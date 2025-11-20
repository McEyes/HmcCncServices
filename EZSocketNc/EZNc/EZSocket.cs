using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using System.Xml.XPath;

using EZSocketNc.Commons;
using EZSocketNc.Configs;
using EZSocketNc.Extensions;
using EZSocketNc.Mqtts.Dtos;
using EZSocketNc.Interface;

using Microsoft.CSharp.RuntimeBinder;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 单个通讯信息
    /// </summary>
    public class EZSocket : IEZSocket, IDisposable
    {
        public string DeviceType => "melsec";
        public string Name { get { return DeviceConfig.Name + "_" + SocketConfig.Key; } }
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public IDeviceConfig DeviceConfig { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EZSocketConfig SocketConfig { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EZCncInfo EquipmentInfo { get; set; }
        /// <summary>
        /// 通讯结果
        /// </summary>
        public EZResult EzResult
        {
            get
            {
                if (lResult >= 0) return (EZResult)(uint)lResult;
                else
                    return EZResult.ME_ERR_FLG;
            }
        }
        /// <summary>
        /// 通讯结果
        /// </summary>
        private int lResult { get; set; }
        //public EZNCAUTLib.DispEZNcCommunication oEZNcAutCom { get; set; } = null;
        public dynamic oEZNcAutCom { get; set; } = null;

        public bool IsInit => oEZNcAutCom == null;

        private bool _isOpen = false;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    if (value)
                        EquipmentInfo.RunStatus = EquipmentStatus.Running;
                    else if (EquipmentInfo.RunStatus != EquipmentStatus.PortOffLine)
                        EquipmentInfo.RunStatus = EquipmentStatus.Disconnect;
                }
            }
        }

        private object lockObj = new object();

        private readonly System.Timers.Timer _timer;
        public bool IsTick = false;



        public ConcurrentBag<string> errMsg { get; private set; }

        //protected readonly ILogger Log;
        #endregion 属性

        public EZSocket(CncDeviceConfig config)
        {
            DeviceConfig = config;
            SocketConfig = config.Socket;
            EquipmentInfo = new EZCncInfo(config);
            EquipmentInfo.Name = $"{DeviceConfig.HostName}[{SocketConfig.Ip}]";
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
            errMsg = new ConcurrentBag<string>();
        }
        public EZSocket(EZSocketConfig config)
        {
            SocketConfig = config;
            EquipmentInfo = new EZCncInfo(null);
            if (string.IsNullOrWhiteSpace(EquipmentInfo.Name))
                EquipmentInfo.Name = SocketConfig.Ip;
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
            errMsg = new ConcurrentBag<string>();
        }



        /// <summary>
        /// 主动采集设备信息
        /// </summary>
        public void StartMonitor()
        {
            try
            {
                Init();
                //_timer.Interval = SocketConfig.DataReadFreq * 1000;//采集心率,默认1秒钟采集一次
                //_timer_Elapsed(null, null);
                //_timer.Start();
                errMsg.Add($"{SocketConfig.Key}监控已启动");
                Utils.LogHelper.Info($"{SocketConfig.Key}监控已启动");
            }
            catch (Exception ex)
            {
                errMsg.Add("StartMonitor Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"StartMonitor Error：{ex.Message}\r\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopMonitor()
        {
            try
            {
                _timer.Enabled = false;
                IsTick = true;
                _timer.Stop();
                errMsg.Add($"{SocketConfig.Key}监控已停止");
                Utils.LogHelper.Info($"{SocketConfig.Key}监控已停止");
            }
            catch (Exception ex)
            {
                errMsg.Add("StopMonitor Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"StopMonitor Error：{ex.Message}\r\n{ex.StackTrace}");
                //Log.LogError("Init Timer Error：" + ex.Message + "\r\n" + ex.StackTrace, ex);
            }
        }


        /// <summary>
        /// 采集设备数据并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsTick)
            {
                IsTick = true;
                try
                {
                    ReadEquipmentState();
                }
                catch (Exception ex)
                {
                    errMsg.Add("_timer_Elapsed Error：" + ex.Message + "\r\n" + ex.StackTrace);
                    Utils.LogHelper.Error($"_timer_Elapsed Error：{ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    IsTick = false;
                }
            }
        }

        /// <summary>
        /// 设备初始化，一个设备只需要初始化一次
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private dynamic Init(EZSocketConfig config = null)
        {
            if (config != null) SocketConfig = config;
            if (oEZNcAutCom == null)
            {
                lock (lockObj)
                {
                    if (oEZNcAutCom == null)
                    {
                        errMsg = new ConcurrentBag<string>();
                        errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]创建CreateEZNcCom");
                        Utils.LogHelper.Info($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]创建CreateEZNcCom");
                        oEZNcAutCom = EZSocketFactory.CreateEZNcCom(SocketConfig);
                        if (oEZNcAutCom != null)
                        {
                            lResult = oEZNcAutCom.SetTCPIPProtocol(SocketConfig.Ip, SocketConfig.Port);//设定后，后续可以多次close和open
                            errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]SetTCPIPProtocol[0x{lResult:x}]{((EZSetTCPIPProtocolErrorType)(uint)lResult).GetDescription()}");
                            errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]oEZNcAutCom创建成功");
                            Utils.LogHelper.Info($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]SetTCPIPProtocol[0x{lResult:x}]{((EZSetTCPIPProtocolErrorType)(uint)lResult).GetDescription()}");
                            Utils.LogHelper.Info($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]oEZNcAutCom创建成功");
                        }
                        else
                        {
                            EquipmentInfo.RunStatus = EquipmentStatus.PortOffLine;
                            errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]oEZNcAutCom创建失败");
                            Utils.LogHelper.Info($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]oEZNcAutCom创建失败");
                        }
                    }
                }
            }
            return oEZNcAutCom;
        }

        /// <summary>
        /// 设备连接，可以关闭在打开n次
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public IResult Conn(EZSocketConfig config = null)
        {
            IResult result = new Result();
            Init(config);
            lock (lockObj)
            {
                if (oEZNcAutCom != null && !IsOpen)
                {
                    lock (lockObj)
                    {
                        lResult = oEZNcAutCom.Open3((int)SocketConfig.SystemType, SocketConfig.MachineNo, SocketConfig.TimeOut, SocketConfig.HostName);
                        errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Open3[0x{lResult:x}]{((EZeNetErrorType)((uint)lResult)).GetDescription()}");
                        Utils.LogHelper.Debug($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Open3[0x{lResult:x}]{((EZeNetErrorType)((uint)lResult)).GetDescription()}");
                        result.SetError(lResult);
                        IsOpen = result.Success;
                        ReadEquipmentInfo();
                    }
                }
                else if (!IsOpen)
                {
                    Utils.LogHelper.Warn($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Open3：[0x{lResult:x}]{((EZeNetErrorType)(uint)lResult).GetDescription()}，CreateEZNcCom失败，请检查配置以及是否安装了EZSocket A2的SDK\r\n");
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Open3：[0x{lResult:x}]{((EZeNetErrorType)(uint)lResult).GetDescription()}，CreateEZNcCom失败，请检查配置以及是否安装了EZSocket A2的SDK\r\n");
                    result.SetError(EZResult.EZNC_COMM_CREATEPC, $"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CreateEZNcCom失败，请检查配置以及是否安装了EZSocket A2的SDK");
                }
            }
            return result;
        }

        /// <summary>
        /// 设备退出关闭
        /// </summary>
        /// <returns></returns>
        public IResult Close()
        {
            IResult result = new Result();
            if (_timer != null)
            {
                _timer.Enabled = false;
                IsTick = true;
                _timer.Stop();
            }
            if (oEZNcAutCom != null)
            {
                lock (lockObj)
                {
                    if (IsOpen)
                    {
                        lResult = oEZNcAutCom.Close2(EZNcDef.EZNC_RESET_SIMPLE);//lReset：指定NC系统的复位方法。无论指定哪一种复位方法，都进行线路关闭处理。
                        result.SetError(lResult);
                        IsOpen = false;
                    }
                    oEZNcAutCom = null;
                }
            }
            return result;
        }


        /// <summary>
        /// 读取设备基础信息，只初始化的时候读取
        /// </summary>
        private void ReadEquipmentInfo()
        {
            if (IsOpen)
            {
                EquipmentInfo.Name = $"{DeviceConfig?.HostName}[{SocketConfig.Ip}]";// SocketConfig.Ip;
                GetNcVersion();
                GetSerialNo();
                GetProgramPosition3();
                GetDriveInformation();
                GetFeedSpeed();
                GetSpindleSpeed();
            }
        }
        int readTimes = 0;
        /// <summary>
        /// 实时刷新设备状态信息
        /// </summary>
        /// <returns></returns>
        public EZCncInfo ReadEquipmentState()
        {
            Conn();
            if (IsOpen)
            {
                GetRunStatus();
                //GetLight();
                GetClockData();
                GetStartTime();
                GetAliveTime();
                GetTotalRunTime();
                if (readTimes++ % 10 == 0)
                {
                    GetPrgData();
                    GetFeedSpeed();
                    GetSpindleSpeed();
                    EquipmentInfo.FeedFA = GetFeedCommand(0).Data;
                    EquipmentInfo.FeedFM = GetFeedCommand(1).Data;
                    EquipmentInfo.FeedFS = GetFeedCommand(2).Data;
                    EquipmentInfo.FeedFC = GetFeedCommand(3).Data;
                    EquipmentInfo.FeedFE = GetFeedCommand(4).Data;
                    EquipmentInfo.FeedTCP = GetFeedCommand(5).Data;
                    EquipmentInfo.CommandM = GetCommand2(0, 1).Data;
                    EquipmentInfo.CommandS = GetCommand2(1, 1).Data;
                    EquipmentInfo.CommandT = GetCommand2(2, 1).Data;
                    EquipmentInfo.CommandB = GetCommand2(3, 1).Data;
                }
                if (EquipmentInfo.RunStatus == EquipmentStatus.Running)
                {
                    GetPosition();
                }
                else
                {
                    GetAlarm();
                }
                if (readTimes > 99999) readTimes = 0;
            }
            return EquipmentInfo;
        }


        public List<EquipmentInformationDataParameter> GetChangeInfo()
        {
            var informations = new List<EquipmentInformationDataParameter>();
            try
            {
                informations.Add(new EquipmentInformationDataParameter() { Key = "RunStatus", Value = EquipmentInfo.RunStatus.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "RunLight", Value = EquipmentInfo.RunLight.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "CurrentProgramName", Value = EquipmentInfo.ProgramName });
                informations.Add(new EquipmentInformationDataParameter() { Key = "DriveName", Value = EquipmentInfo.DrivesStr });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedRate", Value = EquipmentInfo.FeedRate.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedSpeed", Value = EquipmentInfo.FeedSpeed.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "SpindleRate", Value = EquipmentInfo.SpindleRate.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "SpindleSpeed", Value = EquipmentInfo.SpindleSpeed.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "TotalRunTime", Value = EquipmentInfo.TotalRunTime });
                informations.Add(new EquipmentInformationDataParameter() { Key = "TotalStartTime", Value = EquipmentInfo.TotalStartTime });
                informations.Add(new EquipmentInformationDataParameter() { Key = "TotalAliveTime", Value = EquipmentInfo.TotalAliveTime });
                informations.Add(new EquipmentInformationDataParameter() { Key = "CurrentDateTime", Value = EquipmentInfo.CurrentDateTime });
                informations.Add(new EquipmentInformationDataParameter() { Key = "SpindleLocationX", Value = EquipmentInfo.CurrentPosition.X.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "SpindleLocationY", Value = EquipmentInfo.CurrentPosition.Y.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "SpindleLocationZ", Value = EquipmentInfo.CurrentPosition.Z.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "MachinePositionX", Value = EquipmentInfo.MachinePosition.X.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "MachinePositionY", Value = EquipmentInfo.MachinePosition.Y.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "MachinePositionZ", Value = EquipmentInfo.MachinePosition.Z.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "WorkPositionX", Value = EquipmentInfo.WorkPosition.X.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "WorkPositionY", Value = EquipmentInfo.WorkPosition.Y.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "WorkPositionZ", Value = EquipmentInfo.WorkPosition.Z.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "AlarmInfo", Value = EquipmentInfo.AlarmMsgStr.TrimEnd('_') });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedFA", Value = EquipmentInfo.FeedFA.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedFM", Value = EquipmentInfo.FeedFM.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedFS", Value = EquipmentInfo.FeedFS.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedFC", Value = EquipmentInfo.FeedFC.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "FeedTCP", Value = EquipmentInfo.FeedTCP.ToString("0.000") });
                informations.Add(new EquipmentInformationDataParameter() { Key = "CommandM", Value = EquipmentInfo.CommandM.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "CommandS", Value = EquipmentInfo.CommandS.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "CommandT", Value = EquipmentInfo.CommandT.ToString() });
                informations.Add(new EquipmentInformationDataParameter() { Key = "CommandB", Value = EquipmentInfo.CommandB.ToString() });
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Debug($"[Cnc][{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] GetChangeInfo 异常：{ex.Message}\r\n {ex.StackTrace}");
            }
            return informations;
        }


        #region API

        /// <summary>
        /// 报警信息获取处理
        /// </summary>
        /// <returns></returns>
        public IResult<string[]> GetAlarm()
        {
            var result = new Result<string[]>();
            try
            {
                string alarmMsg = string.Empty;

                //lMessageNumber：指定要获取的信息数。值：：1～10(最大)
                int lMessageNumber = 1;
                lResult = oEZNcAutCom.System_GetAlarm2(lMessageNumber, EZNcDef.M_ALM_ALL_ALARM, out alarmMsg);
                //ChekcAlarmMsg(lResult);
                result.SetError(lResult, alarmMsg);
                //Utils.LogHelper.Warn($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Alarm：lResult:{lResult},{alarmMsg}");
                if (result.Success)
                {
                    result.Data = alarmMsg.Split(new char[] { '\r', '\n', '\0' }, StringSplitOptions.RemoveEmptyEntries);
                    //result.Data = result.Data.Where(f => !f.Contains("安全门")).ToArray();
                    if (EquipmentInfo.AlarmMsg == null || EquipmentInfo.AlarmMsg.Length != result.Data.Length
                         || (EquipmentInfo.AlarmMsg.Length > 0 && result.Data.Length > 0 && EquipmentInfo.AlarmMsg[0] != result.Data[0]))
                    {
                        EquipmentInfo.AlarmMsg = result.Data;
                        var i = 1;
                        foreach (var item in result.Data)
                        {
                            errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Alarm{i++}：{item}");
                            //Utils.LogHelper.Warn($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Alarm{i++}：{item}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error：" + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        private string ChekcAlarmMsg(long lResult)
        {
            var msg = string.Empty;
            if (lResult == (long)EZResult.EZNC_OPE_CURRALM_ADDR)
            {
                msg = $"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error：系统、轴指定不正确";
            }
            else if (lResult == (long)EZResult.EZNC_OPE_CURRALM_ALMTYPE)
            {
                msg = $"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error：报警种类不正确";
            }
            else if (lResult == (long)EZResult.EZNC_OPE_CURRALM_DATAERR)
            {
                msg = $"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error：NC与PC之间的通信数据有误";
            }
            else if (lResult == (long)EZResult.EZNC_OPE_CURRALM_DATASIZE)
            {
                msg = $"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error:无法进入应用程序准备的缓冲区";
            }
            else if (lResult == (long)EZResult.EZNC_OPE_CURRALM_NOS)
            {
                msg = $"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAlarm Error：获取信息数不正确";
            }
            if (!string.IsNullOrWhiteSpace(msg))
            {
                errMsg.Add(msg);
                Utils.LogHelper.Error(msg);
            }
            return msg;
        }




        public IResult<string> GetPosition()
        {
            var result = new Result<string>();
            try
            {
                //errMsg.Add("GetPosition ============");
                //获取当前坐标位置 CurentPosition
                EquipmentInfo.CurrentPosition.X = GetPostion("Position_GetCurrentPosition", 1);
                EquipmentInfo.CurrentPosition.Y = GetPostion("Position_GetCurrentPosition", 2);
                EquipmentInfo.CurrentPosition.Z = GetPostion("Position_GetCurrentPosition", 3);


                // 获取工作坐标位置WorkPosition
                EquipmentInfo.WorkPosition.X = GetPostion("Position_GetWorkPosition", 1);
                EquipmentInfo.WorkPosition.Y = GetPostion("Position_GetWorkPosition", 2);
                EquipmentInfo.WorkPosition.Z = GetPostion("Position_GetWorkPosition", 3);


                // 获取机械位置 MachinePosition
                EquipmentInfo.MachinePosition.X = GetPostion("Position_GetMachinePosition", 1);
                EquipmentInfo.MachinePosition.Y = GetPostion("Position_GetMachinePosition", 2);
                EquipmentInfo.MachinePosition.Z = GetPostion("Position_GetMachinePosition", 3);

            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetPosition Error：" + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetPosition Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        private double GetPostion(string method, int lAxisNo)
        {
            var result = new Result<string>();
            double dPosition = 0;
            int lSkipOn = 0;
            if (method == "Position_GetMachinePosition")
            {

                lResult = oEZNcAutCom.Position_GetMachinePosition(lAxisNo, out dPosition, lSkipOn);//lSkipOn As LONG // (I) 跳跃启动标志
            }
            else if (method == "Position_GetWorkPosition")
            {

                lResult = oEZNcAutCom.Position_GetWorkPosition(lAxisNo, out dPosition, lSkipOn);
            }
            else
                lResult = oEZNcAutCom.Position_GetCurrentPosition(lAxisNo, out dPosition);
            //lockObj.GetType().InvokeMember(method, BindingFlags.InvokeMethod, null, oEZNcAutCom, new object[] { lAxisNo, out dPosition });
            result.SetError(lResult);
            if (!result.Success)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
            }
            else
            {
                return dPosition;
            }
            return dPosition;
        }


        /// <summary>
        /// 读出当前模块
        /// </summary>
        /// <returns></returns>
        public IResult<string> GetPrgData()
        {
            var result = new Result<string>();
            try
            {
                List<string> PrgList = new List<string>();
                var CurrentPrg = string.Empty;
                string szPrgData = string.Empty;
                string szPrgNo = string.Empty;
                int lBlockNumber = 10;
                int lCurrentBlkNo = 1;
                int lSequenceNo = 0;

                //自动化调用步骤
                //Program_CurrentBlockRead(
                //lBlockNumber As LONG, // (I)程序段数 指定要获取的程序段数。值：1～10
                //lppwszProgramData As STRING *, // (O)程序保存 ,以UNICODE字符串获取程序段。插入CR,LF代码以分隔程序段。并在末尾插入NULL。
                //plCurrentBlockNo As LONG * // (O)正在执行的程序段号,返回所获取程序段正在执行的程序段号。0 非运行中,1 第1程序段,2 第2程序段
                //) As LONG // (O)错误代码
                // 读取当前程序块的处理//CurrentPrg
                //lResult = oEZNcAutCom.Program_CurrentBlockRead(10, out szPrgData, out lCurrentBlkNo);
                lResult = oEZNcAutCom.Program_CurrentBlockRead(lBlockNumber, out szPrgData, out lCurrentBlkNo);//读出当前模块，获取的是程序块，里面是坐标信息
                /**
X168.982 Y-214.031 Z-2.696
X169.094 Y-213.825
Y-213.734
Y-207.807 Z-2.7
Y-207.716
X168.98 Y-207.508
X168.773 Y-207.395
X168.682 Y-207.394
X159.206 Z-2.707
X159.115
                 * 
                 */
                //坐标快
                result.SetError(lResult);
                if (result.Success)
                {
                    EquipmentInfo.ProgramBlock = szPrgData;
                    // 程序号获取处理 返回当前搜索完成或正在自动运行的程序编号
                    lResult = oEZNcAutCom.Program_GetProgramNumber2(EZNcDef.EZNC_MAINPRG, out szPrgNo);//获取的是程序名
                    result.SetError(lResult);
                    if (!result.Success)
                    {
                        errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                        Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    }
                    EquipmentInfo.ProgramName = szPrgNo;//程序名
                    // 处理序列号获取 ,返回当前搜索完成或正在自动运行的程序的顺序号
                    lResult = oEZNcAutCom.Program_GetSequenceNumber(EZNcDef.EZNC_MAINPRG, out lSequenceNo);
                    result.SetError(lResult);
                    if (!result.Success)
                    {
                        errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                        Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    }
                    EquipmentInfo.SequenceNumber = lSequenceNo;
                }
                else
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetPrgData Error：" + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetPrgData Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取从控制装置电源打开到关闭的电源打开时间总时长(小时、分、秒)。
        /// 在达到最大值时停止累计，并保持最大值。
        /// </summary>
        public IResult GetClockData()
        {
            var result = new Result<string>();
            try
            {
                int plDate = 0;
                int plTime = 0;
                lResult = oEZNcAutCom.Time_GetClockData(out plDate, out plTime);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Close();//如果拿取失败，退出
                    return result;
                }
                // 0- 99995959
                if (plDate >= 20241030)
                {
                    var date = plDate.ToString().PadLeft(8, '0');
                    var time = plTime.ToString().PadLeft(6, '0');
                    result.Data = date.Insert(6, "-").Insert(4, "-") + " " + time.Insert(4, ":").Insert(2, ":");
                    EquipmentInfo.CurrentDateTime = result.Data;
                }
                else
                {
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetClockData 失败关闭连接重连");
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetClockData 失败关闭连接重连");
                    Close();//如果拿取失败，退出
                    return result;
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetClockData Error：" + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetClockData Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        /// <summary>
        /// 获取从控制装置电源打开到关闭的电源打开时间总时长(小时、分、秒)。
        /// 在达到最大值时停止累计，并保持最大值。
        /// </summary>
        public IResult GetAliveTime()
        {
            var result = new Result<string>();
            try
            {
                int plTime = 0;
                lResult = oEZNcAutCom.Time_GetAliveTime(out plTime);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
                // 0- 99995959
                var time = plTime.ToString().PadLeft(8, '0');
                result.Data = time.Insert(time.Length - 4, ":").Insert(time.Length - 1, ":");
                EquipmentInfo.TotalAliveTime = result.Data;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetAliveTime Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        ///获取从以内存(纸带)或MDI模式启动自动运行，到通过M02/M30或复位操作结束为止的总加工时间(小时、分、秒)。
        ///在达到最大值时停止累计，并保持最大值
        ///plTime：返回从以内存(纸带)或MDI模式启动自动运行，
        ///到通过M02/M30或复位操作结束为止的总加工时间(小时、分、秒)。
        ///值：0～99995959
        ///输出示例 9999：59：59 = 99995959
        /// </summary>
        public IResult GetTotalRunTime()
        {
            var result = new Result<string>();
            try
            {
                int plTime = 0;
                lResult = oEZNcAutCom.Time_GetRunTime(out plTime);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
                // 0- 99995959
                var time = plTime.ToString().PadLeft(8, '0');
                result.Data = time.Insert(time.Length - 4, ":").Insert(time.Length - 1, ":");
                EquipmentInfo.TotalRunTime = result.Data;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetRunTime Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        ///获取从以内存(纸带)或MDI模式启动自动运行，到通过进给保持、程序段停止或复位结束为止的总自动启动时间(小时、分、秒)
        ///在达到最大值时停止累计，并保持最大值
        ///plTime：返回从以内存(纸带)或MDI模式启动自动运行，到通过进给保持、程序段停止或复位结束为止的总自动启动时间(小时、分、秒)
        ///值：0～99995959
        ///输出示例 9999：59：59 = 99995959
        /// </summary>
        public IResult GetStartTime()
        {
            var result = new Result<string>();
            try
            {
                int plTime = 0;
                lResult = oEZNcAutCom.Time_GetStartTime(out plTime);
                result.SetError(lResult);
                // 0- 99995959
                var time = plTime.ToString().PadLeft(8, '0');
                result.Data = time.Insert(time.Length - 4, ":").Insert(time.Length - 1, ":");
                EquipmentInfo.TotalStartTime = result.Data;
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetStartTime Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 返回由PLC管理的时间(小时、分、秒)。在用户PLC的软元件ON时开始计数。软元件编号因各机型而异，请通过各机型的PLC接口说明书进行确认
        /// </summary>
        /// <param name="lKind">外部累计时间的种类:0,1;  0根据外部累计时间1 ON进行计数; 1根据外部累计时间2 ON进行计数, 分别位于M700/M800系列：Y704,Y705</param>
        /// <returns></returns>
        public IResult GetEstimateTime(int lKind = 0)
        {
            var result = new Result<string>();
            try
            {
                //long lKind = 0;// (I)外部累计时间的种类:0,1;  0根据外部累计时间1 ON进行计数; 1根据外部累计时间2 ON进行计数
                int plTime = 0;
                //int lKind = 0;
                lResult = oEZNcAutCom.Time_GetEstimateTime(lKind, out plTime);
                result.SetError(lResult);
                // 0- 99995959
                var time = plTime.ToString().PadLeft(8, '0');
                result.Data = time.Insert(time.Length - 4, ":").Insert(time.Length - 1, ":");
                //if (lKind == 0)
                //    EquipmentInfo.Estimate1Time = result.Data;
                //else
                //    EquipmentInfo.Estimate2Time = result.Data;
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetEstimateTime Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        ///获取运行状态。
        ///自动运行暂停中状态仅对M700/M800系列有效
        ///以下信号可表示通过PLC接口进行自动运行的状态：自动运行中、自动运行启动中、自动运行暂停中。这3种信号在各状态下的ON/OFF状态如下
        /// </summary>
        /// <param name="lIndex">
        /// 0 刀长测量。返回 0：非刀长测量中,1：刀长测量中
        /// 1 自动运行中。获取表示正在自动运行的状态。0：非自动运行中 1：自动运行中
        /// 2 自动运行启动。获取表示正在自动运行中执行移动指令或M,S,T,B处理的状态。0：非自动运行启动中 1：自动运行启动中
        /// 3 自动运行暂停中。获取表示在通过自动运行来执行移动指令或辅助功能指令时，自动运行暂停的状态。0：非自动运行暂停中 1：自动运行暂停中
        /// </param>
        /// <returns></returns>
        public IResult GetRunStatus(int lIndex = 1)
        {
            var result = new Result<EquipmentStatus>();
            try
            {
                int plStatus = 0;
                lResult = oEZNcAutCom.Status_GetRunStatus(lIndex, out plStatus);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
                EquipmentInfo.RunStatus = result.Data = plStatus == 1 ? EquipmentStatus.Running : EquipmentStatus.Shutdown;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetRunStatus Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }




        ///// <summary>
        ///// 获取当前设定的M700/M800系列加载的NCAPI模块的路径。在M700/M800系列中打开时，将加载相应路径的NCAPI模块。
        ///// 该路径是Nc A2接口路径(PC上安装的路径)，不需要获取，没意义
        ///// </param>
        ///// <returns></returns>
        //public IResult<string> GetPathLoadModule()
        //{
        //    var result = new Result<string>();
        //    try
        //    {
        //        if (IsOpen)
        //        {
        //            string lppwszPath = string.Empty;
        //            lResult = oEZNcAutCom.GetPathLoadModule(out lppwszPath);
        //            result.SetError(lResult);
        //            if (!result.Success) errMsg.Add(result.Msg);
        //            EquipmentInfo.NcAPIPath = result.Data = lppwszPath;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errMsg.Add("GetPathLoadModule Error：" + ex.Message + "\r\n" + ex.StackTrace);
        //        result.SetError(EZResult.ME_ERR_FLG, ex.Message);
        //    }
        //    return result;
        //}

        /// <summary>
        /// lppwszSerialNo：以UNICODE字符串返回NC序列号(制造号)。
        /// </param>
        /// <returns></returns>
        public IResult<string> GetSerialNo()
        {
            var result = new Result<string>();
            try
            {
                string lppwszSerialNo = string.Empty;
                lResult = oEZNcAutCom.System_GetSerialNo(out lppwszSerialNo);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
                EquipmentInfo.NcSerialNo = result.Data = lppwszSerialNo;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetSerialNo Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 获取程序位置
        /// 轴坐标位置
        /// </param>
        /// <returns></returns>
        public IResult<string> GetProgramPosition3(int lAxisNo = 1)
        {
            var result = new Result<string>();
            try
            {
                //lAxisNo：指定轴。(1轴～ = 1～)
                double pdPosition = 0;//pdPosition：返回程序位置。
                lResult = oEZNcAutCom.Position_GetProgramPosition3(lAxisNo, out pdPosition);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
                //EquipmentInfo.Position =
                result.Data = pdPosition.ToString("0.000");
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetProgramPosition3 Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 以UNICODE字符串获取NC系统的各种版本信息。
        /// 0：获取NC系统的系统号、名称和PLC版本。
        /// 字符串数据的格式如下所示。
        /// NC系统号\tNC系统名称\tPLC系统号\0 
        /// 在NC系统号和NC系统名称之间插入TAB代码。
        /// 数据末尾为NULL代码。
        /// 输出示例“BND-2005W000-A0 MITSUBISHI CNC 830WM”
        /// 若没有项目，则其后为TAB代码。若没有终止项，则TAB代码后为NULL代码。
        /// 1：获取NC系统的控制单元和扩展模块的版本。
        /// 字符串数据的格式如下所示。
        /// 控制单元编号\t扩展模块编号\0 
        /// 在控制单元编号和扩展模块编号之间插入TAB代码。
        /// 数据末尾为NULL代码44,
        /// 2：获取NC系统的RIO模块和终端RIO模块的版本。
        /// 字符串数据的格式如下所示。
        /// RIO模块编号\t终端RIO模块编号\0 
        /// M700/M800系列时为RIO模块1\t RIO模块2\t…\0，最多32个(*)。
        /// *请与机床制造商确认RIO模块数。
        /// 在RIO模块编号和终端RIO模块编号之间插入TAB代码。
        /// 数据末尾为NULL代码。
        /// </summary>
        /// <param name="lAxisNo"></param>
        /// <param name="lIndex"></param>
        /// <returns></returns>
        public IResult<string> GetVersion(int lAxisNo = 1, int lIndex = 0)
        {
            var result = new Result<string>();
            try
            {
                //int lAxisNo = 1;//：指定轴。(1轴～ = 1～) 
                //int lIndex = 0;//：指定参数号。参照下表。0 系统号、名称、PLC版本 取决于系统规格;1 控制单元、扩展模块 取决于系统规格。2 RIO模块、终端RIO模块,取决于系统规格。
                string lppwszBuffer = string.Empty;//lppwszBuffer：以UNICODE字符串返回系统号、名称和控制S/W版本。
                lResult = oEZNcAutCom.System_GetVersion(lAxisNo, lIndex, out lppwszBuffer);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                }
                result.Data = lppwszBuffer;
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetVersion Error：" + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetVersion Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 以UNICODE字符串获取NC系统的各种版本信息。
        /// 0：获取NC系统的系统号、名称和PLC版本。
        /// 字符串数据的格式如下所示。
        /// NC系统号\tNC系统名称\tPLC系统号\0 
        /// 在NC系统号和NC系统名称之间插入TAB代码。
        /// 数据末尾为NULL代码。
        /// 输出示例“BND-2005W000-A0 MITSUBISHI CNC 830WM”
        /// 若没有项目，则其后为TAB代码。若没有终止项，则TAB代码后为NULL代码。
        /// 1：获取NC系统的控制单元和扩展模块的版本。
        /// 字符串数据的格式如下所示。
        /// 控制单元编号\t扩展模块编号\0 
        /// 在控制单元编号和扩展模块编号之间插入TAB代码。
        /// 数据末尾为NULL代码44,
        /// 2：获取NC系统的RIO模块和终端RIO模块的版本。
        /// 字符串数据的格式如下所示。
        /// RIO模块编号\t终端RIO模块编号\0 
        /// M700/M800系列时为RIO模块1\t RIO模块2\t…\0，最多32个(*)。
        /// *请与机床制造商确认RIO模块数。
        /// 在RIO模块编号和终端RIO模块编号之间插入TAB代码。
        /// 数据末尾为NULL代码。
        /// </summary>
        /// <param name="lAxisNo"></param>
        /// <param name="lIndex"></param>
        /// <returns></returns>
        public IResult<EZNcVersion> GetNcVersion()
        {
            var result = new Result<EZNcVersion>();
            var data = GetVersion();
            if (data.Success)
            {
                EquipmentInfo.NcVersion.FormatStr(data.Data);
            }
            return result;
        }




        #region 目录文件获取

        /// <summary>
        /// 读取当前连接的NC控制单元的驱动器信息。
        /// 驱动器信息格式如下所示。
        /// 驱动器名：CRLF驱动器名：CRLF…驱动器名：CRLF\0
        /// 使用CR,LF代码分隔驱动器名。数据末尾为CR,LF、NULL代码。数据末尾为NULL代码。
        /// 无法读取计算机的驱动器信息。
        /// </param>
        /// <returns></returns>
        public IResult<string[]> GetDriveInformation()
        {
            var result = new Result<string[]>();
            try
            {
                string lppwszDriveInfo = string.Empty;
                lResult = oEZNcAutCom.File_GetDriveInformation(out lppwszDriveInfo);
                //EZNC_FILE_DRVLIST_READ：驱动器信息读取错误
                if (lResult >= 0)
                {
                    EquipmentInfo.Drives = result.Data = lppwszDriveInfo.Split(new char[] { '\r', '\n', '\0' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    result.SetError(lResult);
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveInformation[0x{lResult:x}]{(EZFileErrorType)(uint)lResult}:{lppwszDriveInfo},{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveInformation[0x{lResult:x}]{(EZFileErrorType)(uint)lResult}:{lppwszDriveInfo},{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveInformation[0x{lResult:x}]:\r\n Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveInformation[0x{lResult:x}]:\r\n Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        public IResult<EZNcFileInfo[]> GetAllDirs(string lpcwszDirectryName)
        {
            var result = new Result<EZNcFileInfo[]>();
            var list = new List<EZNcFileInfo>();
            ResetDir();
            CloseFile3();
            var data = FindDir2(lpcwszDirectryName, EZFileType.EZNC_DISK_DIRTYPE | EZFileType.EZNC_DISK_SIZE | EZFileType.EZNC_DISK_DATE | EZFileType.EZNC_DISK_COMMENT);
            while (data.Success && data.Data != null)
            {
                list.Add(data.Data);
                data = FindNextDir2();
            }
            result.Data = list.ToArray();
            if (list.Count == 0 && data.Code < 0)
            {
                result.AddError(result);
            }
            return result;
        }

        public IResult<EZNcFileInfo[]> GetAllFiles(string lpcwszDirectryName)
        {
            var result = new Result<EZNcFileInfo[]>();
            var list = new List<EZNcFileInfo>();
            ResetDir();
            CloseFile3();
            var data = FindDir2(lpcwszDirectryName, EZFileType.EZNC_DISK_SIZE | EZFileType.EZNC_DISK_DATE | EZFileType.EZNC_DISK_COMMENT);
            while (data.Success && data.Data != null)
            {
                list.Add(data.Data);
                data = FindNextDir2();
            }
            result.Data = list.ToArray();
            if (list.Count == 0 && data.Code < 0)
            {
                result.AddError(result);
            }
            return result;
        }


        private bool IsFindDir = false;

        /// <summary>
        /// 读取当前连接的NC控制单元的驱动器信息。
        /// 目录指定如下所示。
        /// 驱动器名＋”：”＋\目录名\文件名 …获取指定文件名的信息。(注1)
        /// 驱动器名＋”：”＋\目录名 …获取指定目录名的信息。(注1)
        /// 驱动器名＋”：”＋\目录名\ …获取指定目录下的信息。
        /// 并使用绝对路径进行指定。
        /// EZNC_DISK_DIRTYPE 读取目录信息
        /// EZNC_DISK_COMMENT 读取注释信息(仅限NC控制单元本体侧)
        /// EZNC_DISK_DATE 读取日期信息(仅限计算机侧)
        /// EZNC_DISK_SIZE 读取大小信息
        /// </param>
        /// <returns></returns>
        public IResult<EZNcFileInfo> FindDir2(string lpcwszDirectryName, EZFileType lFileType = EZFileType.EZNC_DISK_SIZE | EZFileType.EZNC_DISK_DATE | EZFileType.EZNC_DISK_COMMENT)
        {
            var result = new Result<EZNcFileInfo>();
            try
            {
                IsFindDir = false;
                //string lpcwszDirectryName = string.Empty;
                //int lFileType = EZNcDef.EZNC_DISK_DIRTYPE;
                string lppwszFileInfo = string.Empty; //文件信息格式如下所示。文件名\t大小\t日期\t注释\0
                lResult = oEZNcAutCom.File_FindDir2(lpcwszDirectryName, (int)lFileType, out lppwszFileInfo);
                if (lResult > 0)
                {
                    result.Data = new EZNcFileInfo(lppwszFileInfo);
                    if ((lFileType & EZFileType.EZNC_DISK_DIRTYPE) == EZFileType.EZNC_DISK_DIRTYPE)
                    {
                        result.Data.IsDir = true;
                        IsFindDir = true;
                    }
                }
                else
                {
                    result.SetError(lResult);
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{lpcwszDirectryName}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n" + result.Msg);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{lpcwszDirectryName}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n" + result.Msg);
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}FindDir2 Error：" + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}FindDir2 Error：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 读取当前连接的NC控制单元的驱动器信息。
        /// 目录指定如下所示。
        /// 驱动器名＋”：”＋\目录名\文件名 …获取指定文件名的信息。(注1)
        /// 驱动器名＋”：”＋\目录名 …获取指定目录名的信息。(注1)
        /// 驱动器名＋”：”＋\目录名\ …获取指定目录下的信息。
        /// 并使用绝对路径进行指定。
        /// EZNC_DISK_DIRTYPE 读取目录信息
        /// EZNC_DISK_COMMENT 读取注释信息(仅限NC控制单元本体侧)
        /// EZNC_DISK_DATE 读取日期信息(仅限计算机侧)
        /// EZNC_DISK_SIZE 读取大小信息
        /// </param>
        /// <returns></returns>
        public IResult<EZNcFileInfo> FindNextDir2()
        {
            var result = new Result<EZNcFileInfo>();
            try
            {
                string lppwszFileInfo = string.Empty; //文件信息格式如下所示。文件名\t大小\t日期\t注释\0
                lResult = oEZNcAutCom.File_FindNextDir2(out lppwszFileInfo);
                if (lResult > 0)
                {
                    result.Data = new EZNcFileInfo(lppwszFileInfo);
                    result.Data.IsDir = IsFindDir;
                }
                else
                {
                    result.SetError(lResult);
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]NextDir Errir[0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]NextDir Errir[0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]FindNextDir2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]FindNextDir2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 读取当前连接的NC控制单元的驱动器信息。
        /// 目录指定如下所示。
        /// 驱动器名＋”：”＋\目录名\文件名 …获取指定文件名的信息。(注1)
        /// 驱动器名＋”：”＋\目录名 …获取指定目录名的信息。(注1)
        /// 驱动器名＋”：”＋\目录名\ …获取指定目录下的信息。
        /// 并使用绝对路径进行指定。
        /// EZNC_DISK_DIRTYPE 读取目录信息
        /// EZNC_DISK_COMMENT 读取注释信息(仅限NC控制单元本体侧)
        /// EZNC_DISK_DATE 读取日期信息(仅限计算机侧)
        /// EZNC_DISK_SIZE 读取大小信息
        /// </param>
        /// <returns></returns>
        public IResult ResetDir()
        {
            var result = new Result();
            try
            {
                IsFindDir = false;
                lResult = oEZNcAutCom.File_ResetDir();
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ResetDir Error[0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ResetDir Error[0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ResetDir Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ResetDir Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 读取当前连接的NC控制单元的驱动器信息。
        /// 驱动器信息格式如下所示。
        /// 驱动器名：CRLF驱动器名：CRLF…驱动器名：CRLF\0
        /// 使用CR,LF代码分隔驱动器名。数据末尾为CR,LF、NULL代码。数据末尾为NULL代码。
        /// 无法读取计算机的驱动器信息。
        /// </param>
        /// <returns></returns>
        public IResult<int> GetDriveSize(string bstrDirectoryName)
        {
            var result = new Result<int>();
            try
            {
                int lppwszDriveInfo = 0;
                lResult = oEZNcAutCom.File_GetDriveSize(bstrDirectoryName, out lppwszDriveInfo);
                result.SetError(lResult);
                if (result.Success)
                    result.Data = lppwszDriveInfo;
                else
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveSize Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveSize Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        /// <summary>
        /// 返回lpcwszDirectryName中所指定目录的可用容量。可用容量的单位为字节
        /// 如果在驱动器名中指定了计算机的驱动器，则忽略目录的指定，并返回该驱动器的可用容量。
        /// 如果在驱动器名中指定了NC控制单元，则返回指定目录的可用容量。如果指定目录中存在子目录，
        /// 则从可用容量计算中去除该子目录中的使用容量。
        /// lpcwszDirectryName：以UNICODE字符串指定目录名。
        /// 目录指定如下所示。
        /// 驱动器名＋“：”＋\目录名\  并使用绝对路径进行指定。
        /// plDriveSize：获取指定目录的可用容量。(单位：字节) 
        /// 自动化参数： 
        /// lpcwszDirectryName：请参照lpcwszDirectryName的说明。
        /// pvDriveSize：以字符串获取指定目录的可用容量
        /// </param>
        /// <returns></returns>
        public IResult<string> GetDriveSize2(string bstrDirectoryName)
        {
            var result = new Result<string>();
            try
            {
                string pvDriveSize = "";
                lResult = oEZNcAutCom.File_GetDriveSize2(bstrDirectoryName, out pvDriveSize);
                result.SetError(lResult);
                if (result.Success)
                    result.Data = pvDriveSize;
                else
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveSize2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetDriveSize2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 创建指定的目录。
        /// 驱动器名可指定为NC卡或计算机
        /// NC卡中只能分别在以下所示位置创建目录。
        /// M800系列：SD卡、U盘.
        /// EZNC_FILE_CREATEDIR_FILEEXIST：目录已存在
        /// EZNC_FILE_CREATEDIR_FILESYSTEM：文件系统存在异常
        /// EZNC_FILE_CREATEDIR_ILLEGALNAME：文件名格式不正确
        /// EZNC_FILE_CREATEDIR_NODIR：目录不存在
        /// EZNC_FILE_CREATEDIR_NOTSUPPORTED：未支持(指定了未支持的目录)
        /// EZNC_FILE_CREATEDIR_NAMELENGTH：路径过长
        /// EZNC_FILE_CREATEDIR_MEMORYOVER：容量超限
        /// EZNC_FILE_CREATEDIR_ALREADYOPENED：已打开其他目录
        /// EZNC_FILE_CREATEDIR_ROOTDIRFULL：超出根目录的最大文件数
        /// EZNC_FILE_CREATEDIR_WRITEERR：无法写入
        /// EZNC_FILE_CREATEDIR_WRITE_PROTECT：写保护错误
        /// EZNC_FS_OPEN_FILE_FILEFULL：超出最大可打开文件数
        /// EZNC_FS_OPEN_FILE_ALREADYOPEN：文件已打开
        /// EZNC_FS_OPEN_FILE_BUSY：文件处于无法打开的状态
        /// EZNC_FS_OPEN_FILE_OPEN：无法打开文件
        /// EZNC_FS_OPEN_FILE_MALLOC：无法确保作业区域
        /// EZNC_FS_OPEN_FILE_NOTSUPPORTED：未支持(未支持CF)
        /// EZNC_FS_OPEN_FILE_NAMELENGTH：文件路径过长
        /// EZNC_FS_OPEN_FILE_SORT：刀具数据排序中，无法打开
        /// EZNC_FS_OPEN_FILE_SAFE_NOPASSWD：在未设置安全密码的状态下进行了写入打开
        /// EZNC_FS_OPEN_FILE_PROTECT：保护数据设定错误
        /// EZNC_FS_OPEN_FILE_WRITE_PROTECT：写保护错误
        /// EZNC_FILE_DIR_NODRIVE：驱动器不存在
        /// </param>
        /// <returns></returns>
        public IResult CreateDir(string lpcwszDirectoryName)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_CreateDir(lpcwszDirectoryName);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CreateDir Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CreateDir Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除是定的目录。
        /// 驱动器名可指定为NC控制单元或计算机。
        /// 如果指定为NC控制单元，则M700/M800系列分别只能在以下所示位置删除目录。
        /// M700系列：CF卡
        /// M800系列：SD卡、U盘
        /// 要删除的目录必须为空。
        /// NC卡端驱动器的路径长度为180个字符，计算机端驱动器的路径长度为250个字符，SD卡和U盘的
        /// 路径长度为128个字符
        /// </param>
        /// <returns></returns>
        public IResult DeleteDir(string lpcwszDirectoryName)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_DeleteDir(lpcwszDirectoryName);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]DeleteDir Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]DeleteDir Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 将lpcwszSrcFileName所指定的文件复制到lpcwszDstFileName。
        /// 文件名使用 驱动器名＋“：”＋\目录名\文件名 和绝对路径进行指定。
        /// lpcwszDstFileName不能是已存在的文件名。且传输目标目录必须是已经存在的。
        /// 在C70中，可将¥PRG¥USER 下属的多个程序文件汇总复制到计算机上的一个文件中。此时请在
        /// lpcwszSrcFileName的文件名中指定“*.*”(例：M01:¥PRG¥USER¥*.*)。
        /// 在lpcwszDstFileName中指定计算机上的任意文件名(例：C:¥PLURAL.PRG)。
        /// 要将一个汇总文件在C70中展开时，请在lpcwszSrcFileName中指定计算机上的任意文件名(例：
        /// C:¥PLURAL.PRG)，并将lpcwszDstFileName的 ¥PRG¥USER 下属文件名指定为“ALL.PRG”(例：
        /// M01:¥PRG¥USER¥ALL.PRG)
        /// 此Method中，不检查指定的目录和文件名是否正确。对于不同种类和用途文件之间的传输(例：用参
        /// 数文件(PARAMET.BIN) 覆盖用户程序(\PRG\USER\～.PRG)上)，或将文件复制到目的不同的目录下
        /// 等的不规则操作，建议检查文件名和目录的适应性。
        /// (注) 在NC控制单元自动运行中，请勿执行写入/覆盖操作。
        /// </summary>
        /// <param name="lpcwszSrcFileName"></param>
        /// <param name="lpcwszDstFileName"></param>
        /// <returns></returns>
        public IResult Copy(string lpcwszSrcFileName, string lpcwszDstFileName)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_Copy2(lpcwszSrcFileName, lpcwszDstFileName);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Copy2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Copy2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除lpcwszFileName所指定的文件。
        /// </summary>
        /// <param name="lpcwszFileName"></param>
        /// <returns></returns>
        public IResult Delete(string lpcwszFileName)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_Delete2(lpcwszFileName);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Delete2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Delete2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 将lpcwszSrcFileName所指定的文件名变更为lpcwszDstFileName所指定的文件名。
        /// 在lpszSrcFileName中使用驱动器名＋“：”＋\目录名\文件名 和绝对路径进行指定。
        /// 在lpcwszDstFileName中仅指定不包含驱动器名和目录名的文件名。
        /// 在lpcwszDstFileName中不可指定已存在的文件名。
        /// M700/M800系列也可对目录名进行变更。
        /// 变更目录名时，lpszSrcFileName使用驱动器名＋“：”＋\目录名\ 和绝对路径进行指定。
        /// lpcwszDstFileName仅指定要变更的目录名。
        /// lpcwszDstFileName不能是已存在的目录名。
        /// </summary>
        /// <param name="lpcwszSrcFileName">指定旧文件名。</param>
        /// <param name="lpcwszDstFileName">指定新文件名。</param>
        /// <returns></returns>
        public IResult Rename(string lpcwszSrcFileName, string lpcwszDstFileName)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_Rename2(lpcwszSrcFileName, lpcwszDstFileName);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Copy2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Copy2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 使用指定模式打开文件。按以下优先顺序创建用于创建临时文件的目录。
        /// ・ 环境变量TMP所指定的目录
        /// ・ 本产品的安装目录
        /// 临时文件名为MELDASn。n为数字。
        /// (注1) 打开的文件请务必使用CloseFile2()(或AbortFile2())来关闭文件。如果不使用CloseFile2()，临时文件会有残留。
        /// (注2) NC控制单元自动运行中，请勿执行写入/覆盖操作。
        /// (注3) 在C70/C80中读取SRAM.BIN(SRAM数据(二进制形式))大约需要20秒。在此期间，不能使用其他Method
        /// </summary>
        /// <param name="bstrFileName">lpcwszFileName：以UNICODE字符串指定含有路径的文件名。</param>
        /// <param name="lMode">
        /// 0 EZNC_FILE_INIT 文件初始化
        /// 1 EZNC_FILE_READ 读取模式
        /// 2 EZNC_FILE_WRITE 写入模式
        /// 3 EZNC_FILE_OVERWRITE 覆盖模式(即使指定文件已存在，也可写入)
        /// </param>
        /// <returns></returns>
        public IResult OpenFile3(string bstrFileName, int lMode = 1)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_OpenFile3(bstrFileName, lMode);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]OpenFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]OpenFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 使用指定模式打开文件。按以下优先顺序创建用于创建临时文件的目录。
        /// ・ 环境变量TMP所指定的目录
        /// ・ 本产品的安装目录
        /// 临时文件名为MELDASn。n为数字。
        /// (注1) 打开的文件请务必使用CloseFile3()(或AbortFile3())来关闭文件。如果不使用CloseFile3()，临时文件会有残留。
        /// (注2) NC控制单元自动运行中，请勿执行写入/覆盖操作。
        /// (注3) 在C70/C80中读取SRAM.BIN(SRAM数据(二进制形式))大约需要20秒。在此期间，不能使用其他Method
        /// (注4) 旧MethodCloseFile2()、AbortFile2()、ReadFile2()不能与此Method组合使用。要操作使用
        /// 此Method打开的文件时，请使用CloseFile3()、AbortFile3()、ReadFile3()。WriteFile()可继续使用。
        /// </summary>
        /// <param name="bstrFileName">lpcwszFileName：以UNICODE字符串指定含有路径的文件名。</param>
        /// <param name="lMode">
        /// 0 EZNC_FILE_INIT 文件初始化
        /// 1 EZNC_FILE_READ 读取模式
        /// 2 EZNC_FILE_WRITE 写入模式
        /// 3 EZNC_FILE_OVERWRITE 覆盖模式(即使指定文件已存在，也可写入)
        /// </param>
        /// <returns></returns>
        public IResult OpenFile4(string bstrFileName, int lMode = 1)
        {
            var result = new Result();
            try
            {
                if (oEZNcAutCom == null)
                {
                    result.SetError("设备还没初始化，请稍后在执行！");
                    return result;
                }
                lResult = oEZNcAutCom.File_OpenFile4(bstrFileName, lMode);
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]OpenFile4 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]OpenFile4 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        /// <summary>
        ///关闭文件。用OpenFile3()打开的文件必须使用CloseFile2()(或AbortFile2())关闭文件。
        ///(注) 执行写入/覆盖操作后，在NC控制单元自动运行期间，请勿执行该操作
        /// </summary>
        /// <returns></returns>
        public IResult CloseFile2()
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_CloseFile2();
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CloseFile2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CloseFile2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        ///关闭文件。用OpenFile4()打开的文件必须使用CloseFile3()(或AbortFile3())关闭文件。
        ///(注) 执行写入/覆盖操作后，在NC控制单元自动运行期间，请勿执行该操作
        ///(注2) 旧Method的OpenFile3()不能与此Method组合使用。
        /// </summary>
        /// <returns></returns>
        public IResult CloseFile3()
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_CloseFile3();
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CloseFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]CloseFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        /// <summary>
        ///强制关闭已打开的文件。用于中止写入。中止写入后，正在写入的文件将被删除。
        ///与CloseFile2()的区别在于它不输出错误。
        ///不支持C80
        /// </summary>
        /// <returns></returns>
        public IResult AbortFile2()
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_AbortFile2();
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]AbortFile2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]AbortFile2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        ///强制关闭已打开的文件。用于中止写入。中止写入后，正在写入的文件将被删除。
        ///与CloseFile3()的不同点在于它不输出错误。
        ///(注1) 旧Method的OpenFile3()不能与此Method组合使用。
        ///不支持C80
        /// </summary>
        /// <returns></returns>
        public IResult AbortFile3()
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_AbortFile3();
                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]AbortFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]AbortFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 从OpenFile3()打开的文件中读取数据。要读取的数据返回字节数据的数组及其字节数。
        /// 当 pdwNumRead 小于dwLength时，将其判断为文件末尾。
        /// 在要读取的数据大小中指定一次读取的数据大小。读取大文件时，可以多次读取。在执行
        /// CloseFile2()之前可依次读取。
        /// </summary>
        /// <param name="lLength">(I)要读取的数据大小</param>
        /// <param name="pvData">(O)已读取的数据
        /// 返回对所读取字节数据数组的指针。在本产品中对读取的数据区域分配了内存，
        /// 因此在客户端请使用CoTaskMemFree()明确地释放其内存
        /// </param>
        /// <returns>返回实际读取的字节数。在自动化调用中，VARIANT数据中包含字节数</returns>
        public IResult<byte[]> ReadFile2(int lLength)
        {
            var result = new Result<byte[]>();
            try
            {
                object pvData = new object();
                lResult = oEZNcAutCom.File_ReadFile2(lLength, out pvData);
                if (lResult >= 0)
                {
                    result.Data = pvData as byte[];
                }
                else
                {
                    result.SetError(lResult);
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ReadFile2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ReadFile2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 从OpenFile4()打开的文件中读取数据。要读取的数据返回字节数据的数组及其字节数。
        /// 当pdwNumRead 小于dwLength时，将其判断为文件末尾。
        /// 在要读取的数据大小中指定一次读取的数据大小。读取大文件时，可以分多次读取。在执行CloseFile3()之前可依次读取。
        /// (注1) 旧Method的OpenFile3()不能与此Method结合使用。
        /// </summary>
        /// <param name="lLength">(I)要读取的数据大小</param>
        /// <param name="pvData">(O)已读取的数据
        /// 返回对所读取字节数据数组的指针。在本产品中对读取的数据区域分配了内存，
        /// 因此在客户端请使用CoTaskMemFree()明确地释放其内存
        /// </param>
        /// <returns>返回实际读取的字节数。在自动化调用中，VARIANT数据中包含字节数</returns>
        public IResult<byte[]> ReadFile3(int lLength)
        {
            var result = new Result<byte[]>();
            try
            {
                object pvData = new byte[lLength];
                lResult = oEZNcAutCom.File_ReadFile3(lLength, out pvData);
                if (lResult >= 0)
                {
                    result.Data = pvData as byte[];
                }
                else
                {
                    result.SetError(lResult);
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ReadFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ReadFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 向OpenFile4()打开的文件中写入数据。写入数据为字节数组数据。
        /// 在写入数据大小中指定一次写入的数据大小。写入大数据时，可以分多次写入。
        /// 在执行CloseFile3()之前可依次写入。
        /// (注1) 请注意，如果更改NC控制单元内除加工程序外的文件，NC控制单元可能无法正常运行。请事
        /// 先进行备份，以便您进行还原
        /// </summary>
        /// <param name="pbData">以字节数组指定写入的数据</param>
        /// <returns></returns>
        public IResult WriteFile(byte[] pbData)
        {
            var result = new Result();
            try
            {
                lResult = oEZNcAutCom.File_WriteFile(pbData);

                result.SetError(lResult);
                if (!result.Success)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                    Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]WriteFile Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]WriteFile Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 向OpenFile4()打开的文件中写入数据。写入数据为字节数组数据。
        /// 在写入数据大小中指定一次写入的数据大小。写入大数据时，可以分多次写入。
        /// 在执行CloseFile3()之前可依次写入。
        /// (注1) 请注意，如果更改NC控制单元内除加工程序外的文件，NC控制单元可能无法正常运行。请事
        /// 先进行备份，以便您进行还原
        /// </summary>
        /// <param name="bstrFileName">lpcwszFileName：以UNICODE字符串指定含有路径的文件名。</param>
        /// <param name="lMode">
        /// 0 EZNC_FILE_INIT 文件初始化
        /// 1 EZNC_FILE_READ 读取模式
        /// 2 EZNC_FILE_WRITE 写入模式
        /// 3 EZNC_FILE_OVERWRITE 覆盖模式(即使指定文件已存在，也可写入)
        /// </param>
        /// <param name="pbData">以字节数组指定写入的数据</param>
        /// <returns></returns>
        public IResult WriteFile(string bstrFileName, byte[] pbData, int lMode = 3)
        {
            var result = OpenFile4(bstrFileName, lMode);
            if (result.Success)
            {
                result.AddError(WriteFile(pbData));
                result.AddError(CloseFile3());
            }
            return result;
        }

        #endregion 目录文件获取


        #region 刀具


        /// <summary>
        /// 获取刀具寿命管理方式。
        /// </summary>
        /// </param>
        /// <returns>plType：返回刀具寿命管理方式
        /// 0 无效
        /// 1 方式Ⅰ
        /// 2 方式Ⅱ
        /// 3 方式Ⅲ(仅限M系M700/M800系列）
        /// </returns>
        public IResult<int> GetToolLifeType2()
        {
            var result = new Result<int>();
            try
            {
                int plType = 0;
                lResult = oEZNcAutCom.Tool_GetToolLifeType2(out plType);
                if (lResult >= 0)
                {
                    result.Data = plType;
                }
                else
                {
                    result.SetError(lResult);
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ReadFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]ReadFile3 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取刀具寿命的组号。
        /// </summary>
        /// <returns>返回组号数组
        /// </returns>
        public IResult<int[]> GetToolLifeGroupList()
        {
            var result = new Result<int[]>();
            try
            {
                object lppdwGroup = new int[10];
                lResult = oEZNcAutCom.Tool_GetToolLifeGroupList(out lppdwGroup);
                if (lResult >= 0)
                {
                    result.Data = lppdwGroup as int[];
                }
                else
                {
                    result.SetError(lResult);
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetToolLifeGroupList Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetToolLifeGroupList Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取指定刀具号的寿命管理数据。请注意，返回寿命管理数据的字符串数组的要素数因各机型而异
        /// *关于刀具寿命管理数据的“方式”，请参照各数控装置的使用说明书
        /// </summary>
        /// <param name="lGroup">设定获取刀具寿命的组号</param>
        /// <param name="lToolNo">设定获取刀具寿命的刀具号</param>
        /// <returns></returns>
        public IResult<string[]> GetToolLifeValue(int lGroup, int lToolNo)
        {
            var result = new Result<string[]>();
            try
            {
                object pvData = new string[10];//以UNICODE字符串数组返回寿命管理数据值
                lResult = oEZNcAutCom.Tool_GetToolLifeValue(lGroup, lToolNo, out pvData);

                result.SetError(lResult);
                if (!result.Success)
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                else result.Data = pvData as string[];
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetToolLifeValue Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetToolLifeValue Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取刀库刀套的总组数
        /// </summary>
        /// <returns>返回刀库刀套的总组数。值：0～360(最大) </returns>
        public IResult<int> GetMGNSize()
        {
            var result = new Result<int>();
            try
            {
                int plSize = 0;//以UNICODE字符串数组返回寿命管理数据值
                lResult = oEZNcAutCom.ATC_GetMGNSize(out plSize);

                result.SetError(lResult);
                if (!result.Success)
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                else result.Data = plSize;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetMGNSize Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取指定刀库的刀套组数。
        /// </summary>
        /// <param name="lMagazineNo">指定刀库号</param>
        /// <returns>返回刀库的刀套组数。。值：0～360(最大) </returns>
        public IResult<int> GetMGNSize2(int lMagazineNo)
        {
            var result = new Result<int>();
            try
            {
                int plSize = 0;//以UNICODE字符串数组返回寿命管理数据值
                lResult = oEZNcAutCom.ATC_GetMGNSize2(lMagazineNo, out plSize);

                result.SetError(lResult);
                if (!result.Success)
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][0x{lResult:x}]:{((EZFileErrorType)(uint)lResult).GetDescription()}\r\n{result.Msg}");
                else result.Data = plSize;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetMGNSize2 Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        #endregion 刀具




        #region PLC 地址读取


        /// <summary>
        /// 批量读取从起始软元件字符串开始的具有连续软元件点数的软元件,
        /// </summary>
        /// <param name="lLength">dwLength：指定要获取的软元件点数。(2~</param>
        /// <param name="bstrDevice">lpcwszDevice：指定获取对象的起始软元件字符串。软元件字符串为UNICODE字符串。
        /// 但是，如果在数据类别中指定了字型(双字型)，请在软元件字符串中以16的倍数(32的倍数)进行指定</param>
        /// <param name="lDataType">EZNC_PLC_BIT 1280个
        /// EZNC_PLC_BYTE 1280个
        /// EZNC_PLC_WORD 640个
        /// EZNC_PLC_DWORD 320个
        /// </param>
        /// <returns></returns>
        public IResult<int[]> DeviceReadBlock(string bstrDevice, int lDataType, int lLength = 2)
        {
            var result = new Result<int[]>();
            try
            {
                object pvValues = new object();//以VARIANT返回软元件值数组
                lResult = oEZNcAutCom.Device_ReadBlock(lLength, bstrDevice, lDataType, out pvValues);
                result.SetError(lResult);
                if (!result.Success) errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{bstrDevice}]:{result.Msg}");
                //else if(pvValues is int[])
                //    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{bstrDevice}]:{string.Join(",", pvValues as int[])}");
                //else if (pvValues is short[])
                //    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{bstrDevice}]:{string.Join(",", pvValues as short[])}");
                //else if (pvValues is byte[])
                //    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{bstrDevice}]:{string.Join(",", pvValues as byte[])}");
                result.Data = pvValues as int[];
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{bstrDevice}]DeviceReadBlock Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        /// <summary>
        /// 读取bool
        /// </summary>
        /// <returns></returns>
        public IResult<bool[]> DeviceReadBool(string bstrDevice, int lLength = 2)
        {
            var result = new Result<bool[]>();
            var dataResult = DeviceReadBlock(bstrDevice, EZNcDef.EZNC_PLC_BIT, lLength);
            if (dataResult.Success)
            {
                var list = new List<bool>();
                foreach (var item in dataResult.Data)
                    list.Add(item == 1 ? true : false);
                result.Data = list.ToArray();
            }
            else result.AddError(result);
            return result;
        }

        /// <summary>
        /// 读取int
        /// </summary>
        /// <returns></returns>
        public IResult<byte[]> DeviceReadByte(string bstrDevice, int lLength = 2)
        {
            var result = new Result<byte[]>();
            var dataResult = DeviceReadBlock(bstrDevice, EZNcDef.EZNC_PLC_BYTE, lLength);//单字节
            if (dataResult.Success)
            {
                //var datas = dataResult.Data as byte[];
                //result.Data = datas;
            }
            else result.AddError(result);
            return result;
        }


        /// <summary>
        /// 读取int
        /// </summary>
        /// <returns></returns>
        public IResult<int[]> DeviceReadInt(string bstrDevice, int lLength = 2)
        {
            return DeviceReadBlock(bstrDevice, EZNcDef.EZNC_PLC_WORD, lLength);//单字节
        }

        /// <summary>
        /// 读取int64
        /// </summary>
        /// <returns></returns>
        public IResult<long[]> DeviceReadInt64(string bstrDevice, int lLength = 2)
        {
            var result = new Result<long[]>();
            var dataResult = DeviceReadBlock(bstrDevice, EZNcDef.EZNC_PLC_DWORD, lLength);//单字节
            if (dataResult.Success)
            {
                //var datas = dataResult.Data as long[];
                //result.Data = datas;
            }
            else result.AddError(result);
            return result;
        }

        /// <summary>
        /// 获取当前灯状态
        /// </summary>
        /// <returns></returns>
        public IResult<bool[]> GetLight()
        {
            var result = DeviceReadBool("M8165", 3);
            if (result.Success)
            {
                CheckLight(result.Data);
            }
            return result;
        }


        /// <summary>
        /// 获取指定系统当前的进给速度指令值
        /// 0 F指令进给速度(FA)
        /// 1 手动执行进给速度(FM)
        /// 2 同步进给速度(FS)
        /// 3 自动执行进给速度(FC)
        /// 4 螺纹导程(FE)
        /// 5 前端速度(TCP)(仅M700/M800系列)
        /// </summary>
        /// <returns></returns>
        public IResult<double> GetFeedCommand(int lType = 3)
        {
            var result = new Result<double>();
            try
            {
                double pdValue = 0;//获取指定系统当前的进给速度指令值 0.000～10,000,000.000［mm/min］总位数由NC机型/选项及机械制造商设定值(参数)决定。
                lResult = oEZNcAutCom.Command_GetFeedCommand(lType, out pdValue);
                result.SetError(lResult);
                if (!result.Success) errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                //EquipmentInfo.Position =
                result.Data = pdValue;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetFeedCommand Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取指定系统当前的M/S/T/B功能的指令模态值
        /// lType   lIndex
        /// M       1～4
        /// S       1～最大主轴数*
        /// T       1
        /// B       1～4 
        /// </summary>
        /// <param name="lType">(I)指令类型：指定要获取的指令值的类型
        /// 0 EZNC_M M指令(辅助功能M指令值) 
        /// 1 EZNC_S S指令(主轴转速S指令值)
        /// 2 EZNC_T T指令(换刀T指令值)
        /// 3 EZNC_B B指令(第二辅助功能指令值(指定分度台位置等))
        /// </param>
        /// <param name="lIndex">// (I)指定编号</param>
        /// <returns>返回指定系统的当前指令值，数据范围：0～99999.999(最大) 
        /// </returns>
        public IResult<int> GetCommand2(int lType = 0, int lIndex = 1)
        {
            var result = new Result<int>();
            try
            {
                int plValue = 0;// (O)指令值
                lResult = oEZNcAutCom.Command_GetCommand2(lType, lIndex, out plValue);
                result.SetError(lResult);
                if (!result.Success) errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]" + result.Msg);
                //EquipmentInfo.Position =
                result.Data = plValue;
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]GetCommand2 Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] Error[0x{lResult:x}]：" + ex.Message + "\r\n" + ex.StackTrace);
                result.SetError(EZResult.ME_ERR_FLG, ex.Message);
            }
            return result;
        }



        private void CheckLight(bool[] runLights)
        {
            if (runLights == null)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]读取当前灯颜色状态异常,数据为空.");
                return;
            }
            else if (runLights.Length < 3)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]读取当前灯颜色状态异常:{string.Join(",", runLights)}");
                return;
            }

            // Logs.LogDebug($"[Cnc][{Config.ToString()}]读取当前灯颜色状态:{string.Join(",", runLights)},time:{CNCData.times}，LastSyncDataTime：{CNCData.LastSyncDataTime:yyyy-MM-dd HH:mm:ss.fff}");
            //if (runLights[0] == true)
            //{
            //    EquipmentInfo.RunLight = EZRunLight.Red;
            //}
            //else if (runLights[1] == true)
            //{
            //    EquipmentInfo.RunLight = EZRunLight.Yellow;
            //}
            //else if (runLights[2] == true)
            //{
            //    EquipmentInfo.RunLight = EZRunLight.Green;
            //}
            //else
            //{
            //    EquipmentInfo.RunLight = EZRunLight.None;
            //}
        }


        /// <summary>
        /// 进给速度
        /// R2504 手动进给速度L
        /// R2505 手动进给速度H
        /// R2500 进给倍率
        /// 第 1 手轮进给 / 增量进给倍率 (R2508, R2509
        /// </summary>
        /// <returns></returns>
        public IResult<int[]> GetFeedSpeed()
        {
            var result = DeviceReadInt("R2500", 6);
            if (result.Success)
            {
                if (result.Data != null && result.Data.Length == 6)
                {
                    EquipmentInfo.FeedSpeed = result.Data[4];
                    EquipmentInfo.SpindleRate = EquipmentInfo.FeedRate = result.Data[0];
                }
            }
            return result;
        }

        /// <summary>
        /// 主轴转速
        /// 1) 主轴指令转速输出 (R7000, R7001)
        /// (2) 主轴指令数据 (R6500, R6501)
        /// (2) 主轴指令最终数据 (R6502, R6503)
        /// 始终设为根据主轴用编码器发出的反馈信号计算的实际主轴转速。
        /// 以主轴转速的 1000 倍保存数据
        /// </summary>
        /// <returns></returns>
        public IResult<int[]> GetSpindleSpeed()
        {
            var result = DeviceReadInt("R7000", 2);
            if (result.Success)
            {
                if (result.Data != null && result.Data.Length == 2)
                {
                    EquipmentInfo.SpindleSpeed = result.Data[0];
                }
            }
            return result;
        }

        #endregion  PLC 地址读取



        #endregion API








        public void Dispose()
        {
            Close();
            EZSocketFactory.MachineNoBag.Add(SocketConfig.MachineNo);
        }
    }
}
