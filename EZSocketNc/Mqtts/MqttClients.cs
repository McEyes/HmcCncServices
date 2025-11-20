using EZSocketNc.Configs;
using EZSocketNc.Extensions;
using EZSocketNc.Mqtts.Dtos;
using EZSocketNc.Utils;

using JAgentServiceProtocol;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EZSocketNc.Mqtts
{
    /// <summary>
    /// 一个客户端，多设备模式
    /// </summary>
    public class MqttClients
    {
        private IMqttConfig _config;
        private IMqttClient _client;
        //private Iloger _loger = null;
        private CancellationTokenSource _cts;
        public event EventHandler<string> MessageReceived;
        private MqttQualityOfServiceLevel Qos = MqttQualityOfServiceLevel.AtLeastOnce;



        /// <summary>
        /// 设备配置信息
        /// </summary>
        public CncDeviceConfig[] EquipmentList { get; set; }

        public bool IsConnected => _client == null ? false : _client.IsConnected;

        public MqttClients(IMqttConfig config, CncDeviceConfig[] equipmentList)//, Iloger loger
        {
            _config = config;
            //_loger = loger;
            EquipmentList = equipmentList;
        }

        public void Start()
        {
            try
            {
                Qos = (MqttQualityOfServiceLevel)_config.Qos;
                _cts = new CancellationTokenSource();

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(_config.Host, _config.Port)
                    .WithClientId(Guid.NewGuid().ToString())
                    .WithWillQualityOfServiceLevel(Qos)
                    .WithCredentials(_config.User, _config.Password).Build();

                var mqttFactory = new MqttFactory();
                _client = mqttFactory.CreateMqttClient();
                if (_config.WithSubscribe)
                {
                    _client.ApplicationMessageReceivedAsync += e =>
                    {
                        var receivesBuffer = e.ApplicationMessage?.PayloadSegment.Array ?? new byte[0];
                        var mesBody = System.Text.Encoding.UTF8.GetString(receivesBuffer);
                        if (MessageReceived != null)
                            MessageReceived.Invoke(e, mesBody);
                        return Task.CompletedTask;
                    };
                }

                _client.ConnectedAsync += _client_ConnectedAsync;
                _client.DisconnectedAsync += _client_DisconnectedAsync;
                _client.ConnectAsync(options).Wait();
                if (_config.WithSubscribe && EquipmentList != null)
                {
                    foreach (var m in EquipmentList)
                    {
                        if (m.Mqtt == null)
                        {
                            m.Mqtt = ((MqttConfig)_config).Clone();
                            m.Mqtt.Id = m.Id;
                            m.Mqtt.Kind = m.Kind;
                            m.Mqtt.HostName = m.HostName;
                        }
                        if (m.Mqtt != null && m.Mqtt.WithSubscribe)
                        {
                            _client.SubscribeAsync(new MqttTopicFilter { Topic = m.Mqtt.OutTopic }).Wait();
                        }
                    }
                }

                if (_config.HeartBeat > 0)
                {
                    Task.Factory.StartNew(() =>
                   {
                       Task.Run(() => { StartHeartBeat(); }).Wait();
                   }, TaskCreationOptions.LongRunning);
                }
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Error($"[mqtt] start error:{ex.Message},\r\n{ex.StackTrace}");
            }
        }


        private Task _client_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            return Task.Run(() => { EZSocketNc.Utils.LogHelper.Info("[mqtt]MQTT client connect"); });
        }


        private Task _client_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            return Task.Run(() =>
         {
             try
             {
                 EZSocketNc.Utils.LogHelper.Info("[mqtt] MQTT client disconnect");
                 Thread.Sleep(3000);
                 var cts = _cts.Token;
                 if (!cts.IsCancellationRequested)
                     Restart();
             }
             catch (Exception ex)
             {
                 EZSocketNc.Utils.LogHelper.Error($"[mqtt]reconnect mqtt:{ex.Message},\r\n{ex.StackTrace}");
             }
         });
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Stop()
        {
            try
            {
                if (_client != null)
                {
                    _client.ConnectedAsync -= _client_ConnectedAsync;
                    _client.DisconnectedAsync -= _client_DisconnectedAsync;
                    if (_client.IsConnected) _client.DisconnectAsync().Wait();
                    _client.Dispose();
                    _client = null;
                }
                if (_cts != null && !_cts.IsCancellationRequested)
                {
                    _cts.Cancel();
                    //_cts = null;
                }
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Error($"[mqtt]MQTT client stop:{ex.Message},\r\n{ex.StackTrace}");
            }
        }


        private async void StartHeartBeat()
        {
            CancellationToken cts = _cts.Token;
            while (!cts.IsCancellationRequested)
            {
                if (_client != null && _client.IsConnected)
                {
                    if (_config.HeartBeat == 0) continue;
                    try
                    {
                        var msg = new EquipmentHeartbeat();
                        foreach (var m in EquipmentList)
                        {
                            msg.FillBaseField(m.Mqtt);
                            var message = new MqttApplicationMessageBuilder()
                                  .WithTopic(m.Mqtt.Topic)
                                  .WithPayload(msg.ToJSON())
                                  .WithQualityOfServiceLevel(Qos)
                                  .WithRetainFlag().Build();
                            var result = await _client.PublishAsync(message, cts);
                            if (!result.IsSuccess)
                            {
                                EZSocketNc.Utils.LogHelper.Error($"[mqtt][{m.Mqtt.Topic}]发送心跳包失败,ReasonCode:{result.ReasonCode}\r\n{result.ReasonString}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EZSocketNc.Utils.LogHelper.Error($"[mqtt] send heart beat error:{ex.Message},\r\n {ex.StackTrace}");
                    }
                    await Task.Delay(_config.HeartBeat * 1000);
                }
            }
        }

        public async Task<bool> SendAsync(BaseMsg msg, IDeviceConfig config, int tryTimes = 3)
        {
            msg.FillBaseField(config.Mqtt);
            return await SendAsync(config.Mqtt, msg, tryTimes);
        }

        private async Task<bool> SendAsync(IMqttConfig config, BaseMsg msg, int tryTimes = 3)
        {
            var time = (_config.Retries - tryTimes + 1);
            //EZSocketNc.Utils.LogHelper.Debug($"[mqtt][{config.Topic}]send :{msg.ToJSON()}");
            if (!_client.IsConnected)
            {
                //失败等待重连(10秒一次)，重试3次
                EZSocketNc.Utils.LogHelper.Error($"[mqtt][{config.Topic}][{tryTimes}] IsConnected faild .");
                await Task.Delay(5000 * time);
                if (tryTimes >= 0) return await SendAsync(config, msg, --tryTimes);
                EZSocketNc.Utils.LogHelper.Error($"[mqtt][{config.Topic}][{tryTimes}] 链接超时上报命令失败！");
                return false;
            }
            if (msg == null)
            {
                EZSocketNc.Utils.LogHelper.Error($"[mqtt][{config.Topic}][{tryTimes}] 发送消息对象为空.");
                return true;
            }
            //if (tryTimes == _config.Retries) FillBaseField(msg);
            var payload = msg.ToJSON();
            var message = new MqttApplicationMessageBuilder()
                      .WithTopic(config.Topic)
                      .WithPayload(payload)
                      .WithQualityOfServiceLevel(Qos)
                      .WithRetainFlag().Build();

            var response = _client.PublishAsync(message).Result;
            if (!response.IsSuccess)
            {
                EZSocketNc.Utils.LogHelper.Error($"[mqtt][{config.Topic}][{tryTimes}]send faild :{response.ReasonString}");
                //失败重试3次（延迟N次方秒）
                await Task.Delay(1000 * time);
                return await SendAsync(config, msg, --tryTimes);
            }

            return true;
        }

    }
}
