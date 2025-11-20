using EZSocketNc.Commons;
using EZSocketNc.Extensions;
using EZSocketNc.Mqtts;
using EZSocketNc.Mqtts.Dtos;
using EZSocketNc.Interface;
using EZSocketNc.Siemens;

using HmcCncServices.Configs;

using JAgentServiceProtocol;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using EZSocketNc.Configs;
using EZSocketNc.EZNc;
using System.Collections.Generic;
using MQTTnet;
using MQTTnet.Protocol;
using System.ComponentModel;
using System.Xml.Linq;
using EZSocketNc;
using EZSocketNc.Utils;
using System.Net.Sockets;
using System.IO;
using EZSocketNc.Db;

namespace AoiAdapter.Handlers
{
    public class HmcCncHandler
    {
        private readonly CncServiceConfig config;
        //private readonly Iloger Log;

        private MqttClients mqttClient;
        private CancellationTokenSource _cts;
        public List<IEZSocket> EzCncList;
        public ICmdStorage _cmdStorage;

        public HmcCncHandler(CncServiceConfig config)//, Iloger loger
        {
            //Log = loger;
            this.config = config;
            EzCncList = new List<IEZSocket>();
            _cmdStorage = new CmdStorage();
        }

        public void Start()
        {
            InitMqtt();
            StartSocket();
        }

        public void Stop()
        {
            StopSocket();
            if (mqttClient != null)
            {
                mqttClient.Stop();
            }
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
        }


        private void InitMqtt()
        {
            try
            {
                _cts = new CancellationTokenSource();
                mqttClient = new MqttClients(config.Mqtt, config.CncDevices);//, Log
                mqttClient.MessageReceived += MqttmqttClient_MessageReceived;
                mqttClient.Start();
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Error($"[mqtt] InitMqtt error:{ex.Message},\r\n {ex.StackTrace}");
            }
        }


        private void StartSocket()
        {
            foreach (var item in config.CncDevices)
            {
                if (item.Enable == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        item.Mqtt = config.Mqtt.Clone();
                        item.Mqtt.Id = item.Id;
                        item.Mqtt.Kind = item.Kind;
                        item.Mqtt.HostName = item.HostName;
                        var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(item);
                        socket.EquipmentInfo.PropertyChanged += EquipmentInfo_PropertyChanged;
                        EzCncList.Add(socket);
                        socket.StartMonitor();
                    }, TaskCreationOptions.LongRunning);
                }
            }
            Task.Factory.StartNew(() =>
            {
                Task.Run(() => { ReportCncInfo(); }).Wait();
            }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(() =>
            {
                Task.Run(() => { SyncFileToCnc(); }).Wait();
            }, TaskCreationOptions.LongRunning);
        }

        private async void ReportCncInfo()
        {
            CancellationToken cts = _cts.Token;
            while (!cts.IsCancellationRequested)
            {
                if (mqttClient != null)// && mqttClient.IsConnected
                {
                    try
                    {
                        foreach (var item in config.CncDevices)
                        {
                            if (item.Enable==false) continue;
                            await Task.Factory.StartNew(async () =>
                            {
                                var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(item);
                                socket.ReadEquipmentState();
                                var informations = socket.GetChangeInfo();
                                await SendEquipmentImfoReport(item, informations);
                            }, cts);
                        }
                    }
                    catch (Exception ex)
                    {
                        EZSocketNc.Utils.LogHelper.Error($"[mqtt] send Report CncInfo error:{ex.Message},\r\n {ex.StackTrace}");
                    }
                    await Task.Delay(config.HeartBeat * 1000);
                }
            }
        }

        private async void SyncFileToCnc()
        {
            CancellationToken cts = _cts.Token;
            while (!cts.IsCancellationRequested)
            {
                if (_cmdStorage != null)
                {
                    try
                    {
                        var cmdList = _cmdStorage.All();
                        foreach (var item in cmdList)
                        {
                            await Task.Factory.StartNew(() =>
                            {
                                SendFileToDevice(item);
                            }, cts);
                        }
                    }
                    catch (Exception ex)
                    {
                        EZSocketNc.Utils.LogHelper.Error($"[FileToCnc]  SyncFileToCnc error:{ex.Message},\r\n {ex.StackTrace}");
                    }
                    await Task.Delay(config.HeartBeat * 1000);
                }
            }
        }



        private void StopSocket()
        {
            Task.Factory.StartNew(() =>
              {
                  foreach (var item in config.CncDevices)
                  {
                      var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(item);
                      socket.EquipmentInfo.PropertyChanged -= EquipmentInfo_PropertyChanged;
                      if (socket != null)
                      {
                          if (item.Enable == true) socket.StopMonitor();
                      }
                  }
              }, _cts.Token);
        }



        private void MqttmqttClient_MessageReceived(object sender, string msgBody)
        {
            IResult<DeviceCommandStatus> result = new Result<DeviceCommandStatus>();
            BaseMsg cmdMsg = null;
            EZSocketNc.Utils.LogHelper.Debug($"[mqtt]收到消息：{msgBody}");
            var topic = string.Empty;
            try
            {
                if (sender is MQTTnet.Client.MqttApplicationMessageReceivedEventArgs)
                {
                    var client = sender as MQTTnet.Client.MqttApplicationMessageReceivedEventArgs;
                    topic = client.ApplicationMessage.Topic;
                }

                cmdMsg = msgBody.FromJSON<BaseMsg>();
                //检查消息时效，超过30秒的数据不在处理
                var datetime = DateTime.Now;
                if (!DateTime.TryParse(cmdMsg.TimeStamp, out datetime) || !(datetime > DateTime.Now.AddSeconds(-300) && datetime < DateTime.Now.AddSeconds(300)))
                {
                    //超过时效数据不在执行
                    EZSocketNc.Utils.LogHelper.Warn($"[Mqtt][out/{config.Mqtt.Kind}/{config.Mqtt.Id}] The message is expired:{msgBody}.\r\n");
                    result.SetError(EquipmentErrorCode.CommandExpired);
                    result.Data = DeviceCommandStatus.Failed;
                    return;
                }


                //下载CNC文件
                if ("CmdExecute".Equals(cmdMsg.MessageType, StringComparison.InvariantCultureIgnoreCase))
                {
                    var changeOverMsg = msgBody.FromJSON<BaseMsg<CmdExecute>>();

                    if (string.IsNullOrWhiteSpace(changeOverMsg.Data.Parameter))
                    {
                        EZSocketNc.Utils.LogHelper.Error($"[Mqtt] [{config.Name}]消息命令格式错误，缺少命令信息,当前接收的消息内容为:{msgBody}.\r\n");
                        result.Data = DeviceCommandStatus.Failed;
                        result.SetError(EquipmentErrorCode.UnknownCmdError);
                        result.SetError($"{cmdMsg.MessageType}  消息命令格式错误，缺少命令信息.");
                        return;
                    }
                    //result = AutoChange(changeOverMsg.Data);
                }
                else if ("loadRcp".Equals(cmdMsg.MessageType, StringComparison.InvariantCultureIgnoreCase))
                {
                    var mqttMsg = msgBody.FromJSON<BaseMsg<LoadRcp>>();

                    if (string.IsNullOrWhiteSpace(mqttMsg.Data.FileName) || string.IsNullOrWhiteSpace(mqttMsg.Data.File))
                    {
                        EZSocketNc.Utils.LogHelper.Error($"[Mqtt] [{config.Name}]消息命令格式错误，缺少命令信息,当前接收的消息内容为:{msgBody}.\r\n");
                        result.Data = DeviceCommandStatus.Failed;
                        result.SetError(EquipmentErrorCode.UnknownCmdError);
                        result.SetError($"{cmdMsg.MessageType} 消息命令格式错误，缺少命令信息.");
                        return;
                    }
                    var cncConfig = config.CncDevices.FirstOrDefault(f => f.Mqtt.OutTopic == topic);
                    if (cncConfig != null)
                    {
                        result.Data = DeviceCommandStatus.Executing;
                        CmdRetryEntity entity = CmdHelper.ToEntity(mqttMsg, cncConfig.Socket.Key);
                        result.Success = _cmdStorage.Insert(entity);
                        if (result.Success)
                        {
                            result.Data = DeviceCommandStatus.Success;
                            EZSocketNc.Utils.LogHelper.Debug($"[mqtt] loadRcp命令缓存成功，等待轮训发送");
                        }
                        else
                        {
                            result.Data = DeviceCommandStatus.Failed;
                            EZSocketNc.Utils.LogHelper.Debug($"[mqtt] loadRcp命令缓存失败");
                        }
                        result.AddError(SendFileToDevice(entity));
                        //     var fileStr = mqttMsg.Data.File;
                        //     if (fileStr.StartsWith("data:application/octet-stream;base64,"))
                        //        fileStr = fileStr.Replace("data:application/octet-stream;base64,", "");
                        //     var buffers = EncryptHelper.DecodeBase64(fileStr);
                        //     FileHelper.CreateFile($"{AppDomain.CurrentDomain.BaseDirectory}{cncConfig.Name}\\{mqttMsg.Data.FileName}", buffers);
                        //     var ezSocketNc = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(cncConfig);
                        //     var filepath = mqttMsg.Data.FilePath;
                        //     if (mqttMsg.Data.FilePath.EndsWith("IC1") || mqttMsg.Data.FilePath.EndsWith("IC1\\")) filepath = $"{ mqttMsg.Data.FilePath}\\{mqttMsg.Data.FileName}";
                        //     result.AddError(ezSocketNc.WriteFile(filepath, buffers));
                        //     if (result.Success)
                        //     {
                        // _cmdStorage.Remove(mqttMsg.Id);
                        //        result.Data = DeviceCommandStatus.Success;
                        //     }
                        //     else
                        //     {
                        //        result.Data = DeviceCommandStatus.Failed;
                        //     }
                    }
                }
                else if ("ReConnect".Equals(cmdMsg.MessageType, StringComparison.InvariantCultureIgnoreCase))
                {//重启设备
                 //    cmdMsg.HostName
                    var cncConfig = config.CncDevices.FirstOrDefault(f => f.Mqtt.OutTopic == topic);
                    if (cncConfig != null)
                    {
                        var ezSocketNc = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(cncConfig);
                        ezSocketNc.Close();
                    }
                }
                else
                {
                    EZSocketNc.Utils.LogHelper.Error($"[Mqtt] [{config.Name}]不支持当前指令:{msgBody}.\r\n");
                    result.Data = DeviceCommandStatus.Failed;
                    result.SetError(EquipmentErrorCode.UnknownCmdError);
                }
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Error($"[Mqtt] Message Received 异常,\r\n {ex.Message}\r\n {ex.StackTrace},\r\n msgBody: {msgBody} ");
                result.Data = DeviceCommandStatus.Failed;
                result.SetError(EquipmentErrorCode.UnknownError);
                result.SetError($"Message Received error:{ex.Message}");
            }
            finally
            {
                try
                {
                    if (cmdMsg == null)
                        SendReponseAsync(topic, "CmdResponse", "", result).Wait();
                    else
                        SendReponseAsync(topic, cmdMsg.MessageType, cmdMsg.Id, result).Wait();
                }
                catch (Exception ex)
                {
                    EZSocketNc.Utils.LogHelper.Error($"[mqtt] finally send report Error:{ex.Message},\n{ex.StackTrace}");
                }
            }
        }

        private async Task SendReponseAsync(string topic, string cmd, string cmdId, IResult<DeviceCommandStatus> result)
        {
            try
            {
                var cncConfig = config.CncDevices.FirstOrDefault(f => f.Mqtt.OutTopic == topic);
                if (cncConfig == null) EZSocketNc.Utils.LogHelper.Error($"[mqtt] send report Error,设备[{topic}]不存在，或者已经停止服务！");
                var responseMsg = new CmdResponse();
                var cmddata = new CmdResponseData();
                cmddata.cmd = cmd;
                cmddata.cmdID = cmdId;
                cmddata.errCode = result.Code.ToString();
                cmddata.errMsg = result.Msg;
                cmddata.result = result.Data.ToString().ToLower();
                responseMsg.data = cmddata;
                await mqttClient.SendAsync(responseMsg, cncConfig, config.Mqtt.Retries);
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Error($"[mqtt] send report Error:{ex.Message},\n{ex.StackTrace}");
            }
        }


        #region 设备状态报告
        /// <summary>
        /// 设备状态报告
        /// </summary>
        /// <param name="config">设备配置</param>
        /// <param name="stateInfoList">状态信息集合</param>
        public async Task<bool> SendEquipmentImfoReport(IDeviceConfig config, List<EquipmentInformationDataParameter> stateInfoList)
        {
            if (stateInfoList == null && stateInfoList.Count == 0) return true;
            var resportInfo = config.Mqtt.GetMsg<EquipmentInformation>();
            resportInfo.Data = new EquipmentInformationData() { Parameters = new List<EquipmentInformationDataParameter>() };
            resportInfo.Data.Parameters.AddRange(stateInfoList);

            //EZSocketNc.Utils.LogHelper.Debug($"[mqtt][{config.Name}] send Report CncInfo:\r\n{resportInfo.ToJSON()}");
            return await mqttClient.SendAsync(resportInfo, config, config.Retries);
        }

        #endregion 设备状态报告


        private async void EquipmentInfo_PropertyChanged(IDeviceConfig obj, EzPropertyChangedEventArgs e)
        {
            if (obj is CncDeviceConfig)
            {
                var cncConfig = obj as CncDeviceConfig;
                // LogHelper.Debug($"[CNC]{cncConfig.Socket.Key}EquipmentInfo_PropertyChanged :{e.ToJSON()}");
                var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(cncConfig);
                if ("RunStatus".Equals(e.PropertyName, StringComparison.CurrentCultureIgnoreCase))
                {
                    var cncData = socket.EquipmentInfo;
                    //停止工作消息
                    if (cncData.RunStatus != EquipmentStatus.Running)
                    {
                        await SendEquipmentRunStatusReport(socket.DeviceConfig, "EquipmentDowntime");
                    }
                    else if (cncData.RunStatus == EquipmentStatus.Running)
                    {
                        //开始工作消息
                        await SendEquipmentRunStatusReport(socket.DeviceConfig, "EquipmentDowntimeResume");
                    }
                }
                else if ("AlarmInfo".Equals(e.PropertyName, StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        //LogHelper.Debug($"[CNC]{cncConfig.Socket.Key}AlarmInfo :{e.ToJSON()}");
                        var cncData = socket.EquipmentInfo;
                        var oldAlarmsgs = e.OldValue as string[];
                        var oldAlarms = AlarmMsgFromat.FormatAlarmsg(oldAlarmsgs);
                        if (cncData.RunStatus == EquipmentStatus.Running)
                        {
                            foreach (var item in oldAlarms)
                            {
                                await SendAlarmDataReport(socket.DeviceConfig, item, "EquipmentWarningCleard");
                            }
                        }
                        else
                        {
                            var newAlarmsgs = e.NewValue as string[];
                            var newAlarms = AlarmMsgFromat.FormatAlarmsg(newAlarmsgs);
                            foreach (var item in newAlarms)
                            {
                                await SendAlarmDataReport(socket.DeviceConfig, item, "EquipmentWarning");
                            }
                            foreach (var item in oldAlarms)
                            {
                                await SendAlarmDataReport(socket.DeviceConfig, item, "EquipmentWarningCleard");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EZSocketNc.Utils.LogHelper.Error($"[mqtt] AlarmInfo send report Error:{ex.Message},\n{ex.StackTrace}");
                    }
                }
                else
                {
                    var informations = socket.GetChangeInfo();
                    if (informations.Count > 0)
                    {
                        if (!await SendEquipmentImfoReport(socket.DeviceConfig, informations))
                        {
                            LogHelper.Debug($"[CNC]mqtt Client send Heart beat fail topic ={socket.DeviceConfig.Mqtt.Topic}");
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 发送设备异常或清楚设备异常数据报告
        /// </summary>
        protected async Task<bool> SendAlarmDataReport(IDeviceConfig config, EquipmentAlarmData errorData, string messageType)
        {
            var reportData = config.Mqtt.CreateMsg<EquipmentAlarmData>();
            reportData.Data = errorData;
            reportData.MessageType = messageType;
            // EZSocketNc.Utils.LogHelper.Error($"[{config.Name}] [{messageType}]:{errorData.ToJSON()}");
            return await mqttClient.SendAsync(reportData, config, config.Mqtt.Retries);
        }
        /// <summary>
        /// 发送基本消息，工作或停止工作
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messageType">EquipmentDowntimeResume/EquipmentDowntime</param>
        /// <returns></returns> 
        protected async Task<bool> SendEquipmentRunStatusReport(IDeviceConfig config, string messageType = "EquipmentDowntimeResume")
        {
            var reportData = config.Mqtt.CreateMsg();
            reportData.Sender = config.HostName;
            reportData.MessageType = messageType;
            return await mqttClient.SendAsync(reportData, config, config.Mqtt.Retries);
        }

        private IResult SendFileToDevice(CmdRetryEntity entity)
        {
            try
            {
                var cncConfig = config.CncDevices.FirstOrDefault(f => f.Socket.Key == entity.Key);
                if (cncConfig == null)
                {
                    _cmdStorage.Remove(entity.Id);
                    EZSocketNc.Utils.LogHelper.Warn($"[FileToCnc]【{entity.Key}】设备已经不存在，移除同步命令=>{entity.DataJson}");
                }
                var ezSocketNc = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(cncConfig);
                if (ezSocketNc.EquipmentInfo.RunStatus == EquipmentStatus.Running && ezSocketNc.DeviceType == "melsec")
                {
                    EZSocketNc.Utils.LogHelper.Warn($"[FileToCnc]【{cncConfig.Name}({entity.Key})】设备运行中,跳过...");
                    return new Result("设备运行中,跳过...");
                }

                var mqttMsg = entity.DataJson.FromJSON<BaseMsg<LoadRcp>>();
                var fileStr = mqttMsg.Data.File;
                if (fileStr.StartsWith("data:application/octet-stream;base64,")) fileStr = fileStr.Replace("data:application/octet-stream;base64,", "");
                var buffers = EncryptHelper.DecodeBase64(fileStr);
                FileHelper.CreateFile($"{AppDomain.CurrentDomain.BaseDirectory}{cncConfig.Name}\\{mqttMsg.Data.FileName}", buffers);

                var filepath = mqttMsg.Data.FilePath;
                if (mqttMsg.Data.FilePath.EndsWith("IC1") || mqttMsg.Data.FilePath.EndsWith("IC1\\")) filepath = $"{mqttMsg.Data.FilePath}\\{mqttMsg.Data.FileName}";
                EZSocketNc.Utils.LogHelper.Warn($"[FileToCnc]【{cncConfig.Name}({entity.Key})】程序文件[{mqttMsg.Data.FilePath}]开始下发。。。");
                var result = ezSocketNc.WriteFile(filepath, buffers);
                if (result.Success)
                {
                    _cmdStorage.Remove(mqttMsg.Id);
                    EZSocketNc.Utils.LogHelper.Warn($"[FileToCnc]【{cncConfig.Name}({entity.Key})】程序文件[{mqttMsg.Data.FilePath}]同步成功。{result.Msg}");
                }
                else
                {
                    _cmdStorage.Update(entity);
                    if (entity.RetryTimes > 10) EZSocketNc.Utils.LogHelper.Warn($"[FileToCnc]【{cncConfig.Name}({entity.Key})】程序文件[{mqttMsg.Data.FilePath}]第{entity.RetryTimes}次同步失败");
                }
                return result;
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Warn($"[FileToCnc]【{entity.Key}】程序文件同步异常：{ex.Message},\r\n\t{ex.StackTrace}");
            }
            return new Result("发送失败");
        }
    }
}
