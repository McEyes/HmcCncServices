

using EZSocketNc.Commons;
using EZSocketNc.Configs;
using EZSocketNc.Mqtts.Dtos;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    public delegate void StatusChangedEventHandler(IDeviceConfig equipment, PropertyChangedEventArgs e);

    public delegate void PropertyChangedEventHandler(IDeviceConfig obj, EzPropertyChangedEventArgs e);

    /// <summary>
    /// 设备信息
    /// </summary>
    public class EZCncInfo
    {
        public bool disablePropertyChanged = false;
        public bool AlarmMsgChanged = false;
        public virtual event PropertyChangedEventHandler PropertyChanged;
        protected IDeviceConfig _config;

        private void TeggerHandler(string propertyName, object oldValue, object newValue)
        {
            if (!disablePropertyChanged && PropertyChanged != null)
                PropertyChanged(_config, new EzPropertyChangedEventArgs(propertyName, oldValue, newValue));
        }

        public EZCncInfo(IDeviceConfig config)
        {
            _config = config;
        }


        /// <summary>
        /// 最后一次状态变动时间
        /// </summary>
        public DateTime LastSyncDataTime { get; set; }

        public string Name { get; set; }
        private EquipmentStatus _RunStatus = EquipmentStatus.Disconnect;
        public EquipmentStatus PreRunStatus { get; set; } = EquipmentStatus.Disconnect;
        public EquipmentStatus RunStatus
        {
            get { return _RunStatus; }
            set
            {
                if (value != _RunStatus)
                {
                    PreRunStatus = _RunStatus;
                    _RunStatus = value;
                    AlarmMsg = new string[0];
                    LastSyncDataTime = DateTime.Now;
                    TeggerHandler("RunStatus", PreRunStatus, value);
                }
            }
        }

        private EZRunLight _runLight = EZRunLight.None;
        //[NonSerialized]
        //[Newtonsoft.Json.JsonIgnore]
        //public EZRunLight PreRunLight = EZRunLight.None;
        //private int times = 3;
        //private static object lockObj = new object();
        public EZRunLight RunLight
        {
            get
            {
                if (_runLight == EZRunLight.None)
                {
                    if (RunStatus == EquipmentStatus.Running) return EZRunLight.Green;
                    else if (string.IsNullOrWhiteSpace(AlarmMsgStr)) return _runLight;
                    else if (AlarmMsgStr.Contains("安全门")) return EZRunLight.Yellow;
                    else return EZRunLight.Red;
                }
                else if (!string.IsNullOrWhiteSpace(AlarmMsgStr)) return EZRunLight.Red;
                else if (AlarmMsgStr.Contains("安全门")) return EZRunLight.Yellow;
                else return _runLight;
            }
            //get => _runLight;
            //set
            //{
            //    // Log?.LogInformation($"===========灯状态切换.value:{value},_runStatus:{_runLight},PreRunStatus:{PreRunLight},RunStatusTimes:{times},(int)value < (int)PreRunLight:{(int)value < (int)PreRunLight},(int)PreRunLight:{(int)PreRunLight},(int)value:{(int)value}");
            //    //green 1,none 0
            //    if (_runLight != value)
            //    {
            //        lock (lockObj)
            //        {
            //            if ((int)value < (int)_runLight && times >= 0)
            //            {
            //                times--;

            //                // Log?.LogInformation($"===========灯状态 跳过更新.value:{value},_runStatus:{_runLight},PreRunStatus:{PreRunLight},RunStatusTimes:{times}");
            //                return;
            //            }
            //            times = 3;
            //            PreRunLight = _runLight;
            //            _runLight = value;
            //            LastSyncDataTime = DateTime.Now;
            //            TeggerHandler("RunStatus");
            //            // Log?.LogInformation($"===========灯状态执行更新.value:{value},_runStatus:{_runLight},PreRunStatus:{PreRunLight},RunStatusTimes:{times}");
            //        }
            //    }
            //    else if (value == _runLight)
            //    {
            //        times = 3;
            //        // Log?.LogInformation($"===========灯状态相同不切换.value:{value},_runStatus:{_runLight},PreRunStatus:{PreRunLight},RunStatusTimes:{times}");
            //    }
            //}
        }

        private string _currentProgramName;
        /// <summary>
        /// 程序文件名称，通过Program_GetProgramNumber2获取
        /// </summary>
        public string ProgramName
        {
            get => _currentProgramName;
            set
            {
                if (_currentProgramName != value)
                {
                    var oldValue = _currentProgramName;
                    _currentProgramName = value;
                    LastSyncDataTime = DateTime.Now;
                    TeggerHandler("ProgramName", oldValue, value);
                }
            }
        }

        private string[] _alarmInfo = new string[0];
        /// <summary>
        /// 报警信息
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string[] AlarmMsg
        {
            get => _alarmInfo;
            set
            {
                var oldAlarm = _alarmInfo.Where(item => !value.Contains(item));
                var newAlarm = value.Where(item => !_alarmInfo.Contains(item));
                if (oldAlarm.Count() > 0 || newAlarm.Count() > 0)
                {
                    AlarmMsgChanged = true;
                    TeggerHandler("AlarmInfo", oldAlarm, newAlarm);
                    var list = _alarmInfo.ToList();
                    list.AddRange(newAlarm);
                    foreach (var item in oldAlarm) list.Remove(item);
                    _alarmInfo = list.ToArray();
                }
                else
                {
                    AlarmMsgChanged = false;
                }
            }
        }

        /// <summary>
        /// 报警信息
        /// </summary>
        public string AlarmMsgStr { get { if (AlarmMsg == null) return ""; else return string.Join(";", AlarmMsg); } }
        public string[] Drives { get; set; }
        /// <summary>
        /// 盘符
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string DrivesStr { get { if (Drives == null) return ""; else return string.Join(";", Drives); } }

        /// <summary>
        /// 当前坐标位置
        /// </summary>
        public EZPosition CurrentPosition { get; set; } = new EZPosition();
        /// <summary>
        /// 工作坐标位置
        /// </summary>
        public EZPosition WorkPosition { get; set; } = new EZPosition();
        /// <summary>
        /// 机械位置
        /// </summary>
        public EZPosition MachinePosition { get; set; } = new EZPosition();

        /// <summary>
        /// 获取CNC设备当前日期、时间
        /// </summary>
        public string CurrentDateTime { get; set; }
        /// <summary>
        /// 累计通电时间
        /// 获取累计电源打开时间
        /// 获取从控制装置电源打开到关闭的电源打开时间总时长(小时、分、秒)
        /// </summary>
        public string TotalAliveTime { get; set; }
        /// <summary>
        /// 获取累计自动运行时间
        /// 返回从以内存(纸带)或MDI模式启动自动运行，到通过M02/M30或复位操作结束为止的总加工时间(小时、分、秒)。
        /// </summary>
        public string TotalRunTime { get; set; }

        /// <summary>
        /// 获取累计自动启动时间
        /// 返回从以内存(纸带)或MDI模式启动自动运行，到通过进给保持、程序段停止或复位结束为止的总自动启动时间(小时、分、秒)。
        /// </summary>
        public string TotalStartTime { get; set; }


        private string _runTime;
        /// <summary>
        /// 本次运行时间
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string RunTime
        {
            get => _runTime;
            set
            {
                if (_runTime != value)
                {
                    _runTime = value;
                    // TeggerHandler("RunTime");
                }
            }
        }

        ///// <summary>
        ///// 外部累计时间1
        ///// </summary>
        //public string Estimate1Time { get; set; }

        ///// <summary>
        ///// 外部累计时间2
        ///// </summary>
        //public string Estimate2Time { get; set; }

        /// <summary>
        /// 当前执行的程序块，代码块
        /// 通过Program_CurrentBlockRead获取
        /// </summary>
        public string ProgramBlock { get; set; }
        /// <summary>
        /// 程序编号
        /// </summary>
        public long SequenceNumber { get; set; }

        public string NcSerialNo { get; set; }

        public EZNcVersion NcVersion { get; set; } = new EZNcVersion();
        //public string Position { get;  set; }


        private int _feedSpeed;
        /// <summary>
        /// 进给速度
        /// 手动进给速度：R2504
        /// </summary>
        public int FeedSpeed
        {
            get => _feedSpeed;
            set
            {
                if (_feedSpeed != value)
                {
                    _feedSpeed = value;
                    // TeggerHandler("FeedSpeed");
                }
            }
        }

        private int _feedRate;
        /// <summary>
        /// 进给倍率
        /// 进给倍率：R2500
        /// </summary>       
        public int FeedRate
        {
            get => _feedRate;
            set
            {
                if (_feedRate != value)
                {
                    _feedRate = value;
                    // TeggerHandler("FeedRate");
                }
            }
        }
        private int _spindleSpeed;
        /// <summary>
        /// 主轴转速
        /// </summary>
        public int SpindleSpeed
        {
            get => _spindleSpeed;
            set
            {
                if (_spindleSpeed != value)
                {
                    _spindleSpeed = value;
                    // TeggerHandler("SpindleSpeed");
                }
            }
        }
        private int _spindleRate;
        /// <summary>
        /// 主轴倍率
        /// </summary>
        public int SpindleRate
        {
            get => _spindleRate;
            set
            {
                if (_spindleRate != value)
                {
                    _spindleRate = value;
                    // TeggerHandler("SpindleRate");
                }
            }
        }

        /// <summary>
        ///  F指令进给速度(FA)
        /// </summary>
        public double FeedFA { get; set; }
        /// <summary>
        /// 手动执行进给速度(FM)
        /// </summary>
        public double FeedFM { get; set; }
        /// <summary>
        /// 同步进给速度(FS)
        /// </summary>
        public double FeedFS { get; set; }
        /// <summary>
        /// 自动执行进给速度(FC)
        /// </summary>
        public double FeedFC { get; set; }
        /// <summary>
        /// 螺纹导程(FE)
        /// </summary>
        public double FeedFE { get; set; }
        /// <summary>
        ///  前端速度(TCP)(仅M700/M800系列)
        /// </summary>
        public double FeedTCP { get; set; }

        /// <summary>
        /// EZNC_M M指令(辅助功能M指令值) 
        /// </summary>
        public int CommandM { get; set; }
        /// <summary>
        ///  EZNC_S S指令(主轴转速S指令值)
        /// </summary>
        public int CommandS { get; set; }
        /// <summary>
        ///  EZNC_T T指令(换刀T指令值)
        /// </summary>
        public int CommandT { get; set; }
        /// <summary>
        /// EZNC_B B指令(第二辅助功能指令值(指定分度台位置等))
        /// </summary>
        public int CommandB { get; set; }
        /// <summary>
        /// 操作状态：0=JOG，1=MDI,2=AUTO
        /// </summary>
        public string OpMode { get; set; }
    }
}
