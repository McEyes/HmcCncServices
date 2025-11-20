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
using System.IO;
using Newtonsoft.Json;

using EZSocketNc.Commons;
using EZSocketNc.Configs;
using EZSocketNc.Extensions;
using EZSocketNc.Mqtts.Dtos;
using EZSocketNc.EZNc;
using EZSocketNc.Interface;

using Microsoft.CSharp.RuntimeBinder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ComTypes;

namespace EZSocketNc.Siemens
{
    /// <summary>
    /// 单个通讯信息
    /// </summary>
    public class SiemensSocket : IEZSocket, IDisposable
    {
        public string DeviceType => "siemens";
        public string Name { get { return DeviceConfig.Name + "_" + SocketConfig.Key; } }
        /// <summary>
        /// 开头
        /// </summary>
        public static readonly byte[] stx = new byte[] { 2 };//ASCII值：2	控制值：STX
        public static readonly byte[] stxs = new byte[] { 0x02, 0x7B };//ASCII值：2	控制值：STX
        /// <summary>
        /// 结尾
        /// </summary>
        public static readonly byte[] etx = new byte[] { 3 };//ASCII值：3	控制值：ETX
        public static readonly byte[] tab = new byte[] { 9 };//ASCII值：9	控制值：HT，也就是tab
        public static readonly byte[] cr = new byte[] { 13 };//ASCII值：13	控制值：CR

        public static readonly byte[] etxs = new byte[] { 0x7D, 0x03 };//ASCII值：3	控制值：ETX

        private CancellationTokenSource _cts;
        private TcpClient _client;
        private NetworkStream _stream;
        private StreamReader _reader;
        private Thread _receiveThread;
        private volatile bool __running;
        private bool _running
        {
            get { return __running; }
            set
            {
                if (__running != value)
                {
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]连接{(__running ? "成功" : "断开")}");
                    __running = value;
                }
            }
        }

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

        public bool IsInit => _client == null;

        private bool _isOpen = false;
        public bool IsOpen
        {
            get { return _running; }
            set
            {
                _running = value;
            }
        }

        private object lockObj = new object();


        public ConcurrentBag<string> errMsg { get; private set; }

        #endregion 属性

        public SiemensSocket(CncDeviceConfig config)
        {
            _cts = new CancellationTokenSource();
            DeviceConfig = config;
            SocketConfig = config.Socket;
            EquipmentInfo = new EZCncInfo(config);
            EquipmentInfo.Name = $"{DeviceConfig.HostName}[{SocketConfig.Ip}]";
            errMsg = new ConcurrentBag<string>();
        }
        public SiemensSocket(EZSocketConfig config)
        {
            _cts = new CancellationTokenSource();
            SocketConfig = config;
            EquipmentInfo = new EZCncInfo(null);
            if (string.IsNullOrWhiteSpace(EquipmentInfo.Name))
                EquipmentInfo.Name = SocketConfig.Ip;
            errMsg = new ConcurrentBag<string>();
        }



        /// <summary>
        /// 主动采集设备信息
        /// </summary>
        public void StartMonitor()
        {
            try
            {
                Conn();
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]监控已启动");
                Utils.LogHelper.Info($"[{SocketConfig.Key}]监控已启动");
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
                Close();
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]监控已停止");
                Utils.LogHelper.Info($"[{SocketConfig.Key}]监控已停止");
            }
            catch (Exception ex)
            {
                errMsg.Add("StopMonitor Error：" + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"StopMonitor Error：{ex.Message}\r\n{ex.StackTrace}");
                //Log.LogError("Init Timer Error：" + ex.Message + "\r\n" + ex.StackTrace, ex);
            }
        }


        /// <summary>
        /// 设备连接，可以关闭在打开n次
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public IResult Conn(EZSocketConfig config = null)
        {
            IResult result = new Result();
            // Init(config);
            if (config != null) SocketConfig = config;
            try
            {
                lock (lockObj)
                {
                    if (_running) return result;
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]开始创建连接");
                    Utils.LogHelper.Info($"[{SocketConfig.Key}]开始创建连接");
                    if (_client != null) Close();
                    _client = new TcpClient();
                    _client.Connect(SocketConfig.Ip, SocketConfig.Port);
                    _running = true;

                    _stream = _client.GetStream();
                    // // _reader = new StreamReader(_stream, Encoding.Unicode);
                    // _reader = new StreamReader(_stream, Encoding.UTF8);

                    _receiveThread = new Thread(ReceiveData);
                    _receiveThread.Start();
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]创建连接完成");
                    Utils.LogHelper.Info($"[{SocketConfig.Key}]创建连接完成");
                }
            }
            catch (Exception ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]连接异常");
                Utils.LogHelper.Info($"{SocketConfig.Key}连接异常");
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
            lock (lockObj)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]开始关闭连接");
                Utils.LogHelper.Info($"{SocketConfig.Key}开始关闭连接");
                _running = false;
                _receiveThread?.Join();
                _receiveThread?.Abort();
                _reader?.Close();
                _stream?.Close();
                _client?.Close();
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]关闭连接成功");
                Utils.LogHelper.Info($"{SocketConfig.Key}关闭连接成功");
            }
            return result;
        }

        private void ReceiveData()
        {
            List<byte> jsonBuilder = new List<byte>();
            byte[] buffer = new byte[4096];

            try
            {
                CancellationToken cts = _cts.Token;
                // errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]开始监听: {!cts.IsCancellationRequested},{_running}");
                while (!cts.IsCancellationRequested && _running)
                {
                    try
                    {
                        buffer = new byte[4096];
                        var bytesRead = _stream.Read(buffer, 0, buffer.Length);
                        // errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]成功接收内容: {bytesRead}");
                        if (bytesRead > 0)
                        {
                            byte[] data = new byte[bytesRead];
                            Array.Copy(buffer, 0, data, 0, data.Length);
                            jsonBuilder.AddRange(data);
                            var result = ReceiveBufferData(jsonBuilder.ToArray());
                            if (result.hasError) jsonBuilder.Clear();
                            else jsonBuilder.AddRange(result.laveData);
                        }
                        else
                        {
                            _running = false;
                            errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]接收数据为0，连接断开");
                        }
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        _running = false;
                        jsonBuilder.Clear();
                        errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Error in receiving data22: " + ex.Message + "\r\n" + ex.StackTrace);
                        Utils.LogHelper.Error($"{SocketConfig.Key}接收内容解析异常：{ex.Message}");
                    }
                }
                _running = false;
            }
            catch (Exception ex)
            {
                jsonBuilder.Clear();
                _running = false;
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Error in receiving data: " + ex.Message + "\r\n" + ex.StackTrace);
                Utils.LogHelper.Error($"{SocketConfig.Key}接收内容解析异常：{ex.ToString()}");
            }
        }

        public (byte[] laveData, bool hasError) ReceiveBufferData(byte[] data)
        {
            var errorFlag = false;
            List<byte> jsonBuilder = new List<byte>();
            var flagIndex = ContainsSubArray(data, etxs);
            // errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]成功解析内容: {bytesRead}<={jsonBuilder.Count()}");
            while (flagIndex >= 0)
            {
                jsonBuilder.AddRange(data.Take(flagIndex + 1));
                // errMsg.Add($"1，[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]解析字符串------");
                if (jsonBuilder.Count > 0)
                {
                    if (jsonBuilder[0] == stx[0]) jsonBuilder = jsonBuilder.Skip(1).ToList();
                    else
                    {
                        var startIndex = ContainsSubArray(data, stxs);
                        jsonBuilder = jsonBuilder.Skip(startIndex).ToList();
                    }
                    var dataStr = Encoding.UTF8.GetString(jsonBuilder.ToArray());
                    if (!ProcessJson(dataStr))
                    {//转换失败
                        errorFlag = true;
                    }
                    jsonBuilder.Clear(); // 清除缓冲区
                }
                data = data.Skip(flagIndex + 2).ToArray();
                flagIndex = ContainsSubArray(data, etxs);
            }
            //jsonBuilder.AddRange(data);
            return (data, errorFlag);
        }


        public int ContainsSubArray(byte[] mainArray, byte[] subArray)
        {
            var index = -1;
            if (mainArray.Length < subArray.Length)
                return index;
            for (int i = 0; i <= mainArray.Length - subArray.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < subArray.Length; j++)
                {
                    if (mainArray[i + j] != subArray[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return i;
            }
            return index;
        }

        private IResult SendFileResult = null;
        private bool ProcessJson(string json)
        {
            try
            {
                // errMsg.Add($"接收字符串: {json}");
                if (json.Contains("\"Success\""))
                {
                    SendFileResult = JsonConvert.DeserializeObject<Result>(json);
                }
                else
                {
                    ConvertToCncInfo(JsonConvert.DeserializeObject<SiemensCncData>(json));
                }
                return true;
            }
            catch (JsonException ex)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Error parsing JSON: " + ex.Message);
                // Utils.LogHelper.Error(json);
                Utils.LogHelper.Error("Error parsing JSON: " + ex.Message);
                return false;
            }
        }

        private void ConvertToCncInfo(SiemensCncData data)
        {
            var startTime = DateTime.Now;
            if (data.DataList == null && data.Data == null) return;
            EquipmentInfo.CurrentDateTime = data.Time.ToString("yyyy-MM-dd HH:mm:ss");
            if (data.Data != null)
            {
                ConvertToCncInfo(data.Data);
                return;
            }

            var keyValue = data.DataList.FirstOrDefault(f => f.Key == "opMode");
            if (keyValue != null)
            {
                EquipmentInfo.OpMode = keyValue.Value;
            }
            var status = data.DataList.FirstOrDefault(f => f.Key == "progStatus");
            if (status != null)
            {
                switch (status.Value)
                {
                    case "3":
                        //自动模式运行中才是真的运行了
                        if (EquipmentInfo.OpMode == "2") EquipmentInfo.RunStatus = EquipmentStatus.Running;
                        else EquipmentInfo.RunStatus = EquipmentStatus.Pause;
                        break;
                    case "1":
                        EquipmentInfo.RunStatus = EquipmentStatus.Pause;
                        break;
                    case "2":
                    default:
                        EquipmentInfo.RunStatus = EquipmentStatus.Shutdown;
                        break;
                }
            }
            var progName = data.DataList.FirstOrDefault(f => f.Key == "progName");
            if (progName != null)
            {
                EquipmentInfo.ProgramName = progName.Value;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "block");
            if (keyValue != null)
            {
                EquipmentInfo.ProgramBlock = keyValue.Value;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "blockNoStr");
            if (keyValue != null)
            {
                if (int.TryParse(keyValue.Value, out int iblockNo))
                    EquipmentInfo.SequenceNumber = iblockNo;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "actLineNumber");
            if (keyValue != null)
            {
                EquipmentInfo.NcSerialNo = keyValue.Value;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "feedRateOvr");
            if (keyValue != null)
            {
                if (int.TryParse(keyValue.Value, out int ifeedRateOvr))
                    EquipmentInfo.FeedRate = ifeedRateOvr;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "cmdSpeed");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out double iactSpeed))
                    EquipmentInfo.FeedSpeed = (int)Math.Round(iactSpeed);
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "speedovr");
            if (keyValue != null)
            {
                if (int.TryParse(keyValue.Value, out int ispeedovr))
                    EquipmentInfo.SpindleRate = ispeedovr;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "actSpeed");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out double iactSpeed))
                    EquipmentInfo.SpindleSpeed = (int)Math.Round(iactSpeed);
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "actProgPosX");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out var iactProgPosX))
                    EquipmentInfo.CurrentPosition.X = iactProgPosX;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "actProgPosY");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out var iactProgPosY))
                    EquipmentInfo.CurrentPosition.Y = iactProgPosY;
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "actProgPosZ");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out var iactProgPosZ))
                    EquipmentInfo.CurrentPosition.Z = iactProgPosZ;
            }
            //切割时间，单位秒
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "cycleTime");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out var icuttingTime))
                {
                    var tspan = TimeSpan.FromSeconds(icuttingTime);
                    EquipmentInfo.TotalRunTime = EquipmentInfo.RunTime = $"{tspan.Hours}:{tspan.Minutes}:{tspan.Seconds}";
                    startTime = startTime.AddMinutes(-icuttingTime);
                }
            }
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "opreatingTime");
            if (keyValue != null)
            {
                if (double.TryParse(keyValue.Value, out var iopreatingTime))
                {
                    var tspan = TimeSpan.FromSeconds(iopreatingTime);
                    EquipmentInfo.TotalStartTime = $"{tspan.Hours}:{tspan.Minutes}:{tspan.Seconds}";
                }
            }
            //启动时间，单位分钟
            keyValue = data.DataList.FirstOrDefault(f => f.Key == "poweronTime");
            if (keyValue != null)
            {
                if (long.TryParse(keyValue.Value, out var ipoweronTime))
                {
                    var tspan = TimeSpan.FromMinutes(ipoweronTime);
                    EquipmentInfo.TotalAliveTime = $"{tspan.Hours}:{tspan.Minutes}:00";
                }
            }

            if (data.AlarmList.Count() > 0)
            {
                EquipmentInfo.AlarmMsg = data.AlarmList.Select(f => f.AlarmMsg).Take(1).ToArray();
                if (EquipmentInfo.AlarmMsgChanged)
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] : {string.Join(";", data.AlarmList.Select(f => f.AlarmMsg))}");
            }
            else EquipmentInfo.AlarmMsg = new string[0];
            EZSocketFactory.CreateEZSocket(this.SocketConfig).EquipmentInfo = EquipmentInfo;
        }


        private void ConvertToCncInfo(RuntimeData data)
        {
            var startTime = DateTime.Now;
            EquipmentInfo.OpMode = data.opMode;
            switch (data.progStatus)
            {
                case "3":
                    //自动模式运行中才是真的运行了
                    if (EquipmentInfo.OpMode == "2") EquipmentInfo.RunStatus = EquipmentStatus.Running;
                    else EquipmentInfo.RunStatus = EquipmentStatus.Pause;
                    break;
                case "1":
                    EquipmentInfo.RunStatus = EquipmentStatus.Pause;
                    break;
                case "2":
                default:
                    EquipmentInfo.RunStatus = EquipmentStatus.Shutdown;
                    break;
            }
            EquipmentInfo.ProgramName = data.progName;
            EquipmentInfo.ProgramBlock = data.block;
            if (int.TryParse(data.blockNoStr, out int iblockNo))
                EquipmentInfo.SequenceNumber = iblockNo;
            EquipmentInfo.NcSerialNo = data.actLineNumber;
            if (int.TryParse(data.feedRateOvr, out int ifeedRateOvr))
                EquipmentInfo.FeedRate = ifeedRateOvr;
            if (double.TryParse(data.cmdSpeed, out double iactSpeed))
                EquipmentInfo.FeedSpeed = (int)iactSpeed;
            if (int.TryParse(data.speedovr, out int ispeedovr))
                EquipmentInfo.SpindleRate = ispeedovr;
            if (double.TryParse(data.actSpeed, out double iactSpeed2))
                EquipmentInfo.SpindleSpeed = (int)Math.Round(iactSpeed);

            if (double.TryParse(data.actProgPosX, out double actProgPosX))
                EquipmentInfo.CurrentPosition.X = actProgPosX;
            if (double.TryParse(data.actProgPosY, out double actProgPosY))
                EquipmentInfo.CurrentPosition.Y = actProgPosY;
            if (double.TryParse(data.actProgPosZ, out double actProgPosZ))
                EquipmentInfo.CurrentPosition.Z = actProgPosZ;
            if (double.TryParse(data.cycleTime, out var icuttingTime))
            {
                var tspan = TimeSpan.FromSeconds(icuttingTime);
                EquipmentInfo.TotalRunTime = EquipmentInfo.RunTime = $"{tspan.Hours}:{tspan.Minutes}:{tspan.Seconds}";
                startTime = startTime.AddMinutes(-icuttingTime);
            }
            if (double.TryParse(data.opreatingTime, out var iopreatingTime))
            {
                var tspan = TimeSpan.FromSeconds(iopreatingTime);
                EquipmentInfo.TotalStartTime = $"{tspan.Hours}:{tspan.Minutes}:{tspan.Seconds}";
            }
            if (long.TryParse(data.poweronTime, out var ipoweronTime))
            {
                var tspan = TimeSpan.FromMinutes(ipoweronTime);
                EquipmentInfo.TotalAliveTime = $"{tspan.Hours}:{tspan.Minutes}:00";
            }

            if (data.AlarmList != null && data.AlarmList.Count() > 0)
            {
                EquipmentInfo.AlarmMsg = data.AlarmList.Select(f => f.AlarmMsg).Take(1).ToArray();
                if (EquipmentInfo.AlarmMsgChanged)
                    errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] : {string.Join(";", data.AlarmList.Select(f => f.AlarmMsg))}");
            }
            else EquipmentInfo.AlarmMsg = new string[0];
            EZSocketFactory.CreateEZSocket(this.SocketConfig).EquipmentInfo = EquipmentInfo;
        }




        /// <summary>
        /// </summary>
        /// <param name="bstrFileName">以UNICODE字符串指定含有路径的文件名。</param>
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
            IResult result = UploadFile(bstrFileName, pbData, lMode);
            //Task.Factory.StartNew(() =>
            //{
            //    result = UploadFile(bstrFileName, pbData, lMode);
            //});
            return result;
        }

        public IResult UploadFile(string filePath, byte[] pbData, int lMode = 3)
        {
            var result = new Result();

            // while (!_running || _cts.IsCancellationRequested)
            //     System.Threading.Thread.Sleep(1000);
            try
            {
                if (filePath.StartsWith("\\IC1")) filePath = "f:\\dh\\spf.dir\\" + filePath;
                Utils.LogHelper.Debug($"[{SocketConfig.Key}][{filePath}]开始发送文件.");
                SendFileResult = null;
                var _client = new TcpClient();
                _client.Connect(SocketConfig.Ip, SocketConfig.Port);
                //_client.SendTimeout = 1500000;//半小时超时

                using (var networkStream = _client.GetStream())
                {
                    using (MemoryStream fileStream = new MemoryStream(pbData))
                    {
                        #region 发送文件名
                        var fileBuffer = new byte[512];
                        var data = System.Text.Encoding.UTF8.GetBytes(filePath + ";" + pbData.Count());
                        Array.Copy(data, 0, fileBuffer, 0, data.Length);
                        networkStream.Write(fileBuffer, 0, fileBuffer.Length);
                        #endregion 发送文件名

                        #region 发送文件流

                        int bytesRead;
                        byte[] buffer = new byte[4096];
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            if (networkStream.CanWrite)
                                networkStream.Write(buffer, 0, bytesRead);
                        }
                        networkStream.Flush();
                        errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]文件发送成功：{SendFileResult?.Msg}.");
                        Utils.LogHelper.Debug($"[{SocketConfig.Key}][{filePath}]文件发送成功：{SendFileResult?.Msg}.");
                        #endregion 发送文件流

                        // 3. 接收服务器的确认信息
                        result.AddError(CheckResultFile(networkStream, filePath, 3));
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Debug($"[{SocketConfig.Key}][{filePath}]文件发送异常：{ex.Message}.\r\n{ex.StackTrace}");
                result.SetError($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Error in uploading file: " + ex.Message + "\r\n" + ex.StackTrace);
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Error in uploading file: " + ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                Utils.LogHelper.Debug($"[{SocketConfig.Key}][{filePath}]文件发送结束。");
                _client.Close();
                _client.Dispose();
            }
            return result;
        }

        private IResult CheckResultFile(NetworkStream networkStream, string filePath, int times = 3)
        {
            var result = new Result();
            byte[] messageBuffer = new byte[4096];
            int messageLength = networkStream.Read(messageBuffer, 0, messageBuffer.Length);
            byte[] byteData = new byte[messageLength];
            Array.Copy(messageBuffer, 0, byteData, 0, messageLength);
            ReceiveBufferData(byteData);
            //string successMessage = Encoding.UTF8.GetString(messageBuffer, 0, messageLength);
            if (SendFileResult != null)
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]文件接收情况：{SendFileResult.Msg}.");
                result.AddError(SendFileResult);
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{filePath}] 文件 {SendFileResult.Msg}.");
            }
            else if (times > 0)
            {
                return CheckResultFile(networkStream, filePath, --times);
            }
            else
            {
                errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{filePath}] 文件上传保存失败.");
                result.SetError($"[{SocketConfig.Key}][{filePath}] 文件上传保存失败.");
                Utils.LogHelper.Error($"[{SocketConfig.Key}][{filePath}] 文件上传保存失败.");
            }
            return result;
        }

        // public void UploadFile(string filePath)
        // {
        //     if (!_client.Connected)
        //     {
        //         errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Client is not connected.");
        //         return;
        //     }

        //     try
        //     {
        //         using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //         using (var networkStream = _client.GetStream())
        //         {
        //             byte[] buffer = new byte[4096];
        //             int bytesRead;
        //             while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
        //             {
        //                 networkStream.Write(buffer, 0, bytesRead);
        //             }
        //             errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]File uploaded successfully.");
        //             Utils.LogHelper.Error($"[{SocketConfig.Key}][{filePath}] File uploaded successfully.");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]Error in uploading file: " + ex.Message);
        //         Utils.LogHelper.Error($"[Cnc][{SocketConfig.Key}] GetChangeInfo 异常：{ex.Message}\r\n {ex.StackTrace}");
        //     }
        // }


        /// <summary>
        /// 读取设备基础信息，只初始化的时候读取
        /// </summary>
        private void ReadEquipmentInfo()
        {
            if (IsOpen)
            {
                EquipmentInfo.Name = $"{DeviceConfig?.HostName}[{SocketConfig.Ip}]";
            }
        }
        int readTimes = 0;
        /// <summary>
        /// 实时刷新设备状态信息
        /// </summary>
        /// <returns></returns>
        public EZCncInfo ReadEquipmentState()
        {
            if (!IsOpen)
            {
                // errMsg.Add($"[{SocketConfig.Key}][{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] ReadEquipmentState");
                Conn();
            }
            GetChangeInfo();
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
                Utils.LogHelper.Error($"[Cnc][{SocketConfig.Key}] GetChangeInfo 异常：{ex.Message}\r\n {ex.StackTrace}");
            }
            return informations;
        }



        public void Dispose()
        {
            _cts?.Cancel();
            Close();
            EZSocketFactory.MachineNoBag.Add(SocketConfig.MachineNo);
        }
    }
}
