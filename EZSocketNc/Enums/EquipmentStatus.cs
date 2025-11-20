using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc
{
    
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum EquipmentStatus
    {
        /// <summary>
        /// 未知状态,设备初始状态
        /// </summary>
        [Description("未知状态")]
        Unknown = -1,
        /// <summary>
        /// 运行中，加工中
        /// </summary>
        [Description("运行中")]
        Running = 1,
        /// <summary>
        /// 暂停，待机中
        /// </summary>
        [Description("待机中")]
        Pause = 2,
        /// <summary>
        /// 停机
        /// </summary>
        [Description("停机")]
        Shutdown = 3,
        /// <summary>
        /// 异常报警
        /// </summary>
        [Description("报警")]
        Alarm = 4,
        /// <summary>
        /// 打开门报警
        /// </summary>
        [Description("打开门报警")]
        OpenDoorAlarm = 21,

        /// <summary>
        /// 设备离线,ip不通
        /// </summary>
        [Description("设备离线,无法访问")]
        OffLine = 1001,
        /// <summary>
        /// 设备掉线,ip通，端口不通
        /// </summary>
        [Description("设备端口异常，无法正常通信")]
        PortOffLine = 1002,
        /// <summary>
        /// 设备链接断开,tcp链接断开
        /// </summary>
        [Description("设备链接断开")]
        Disconnect = 1003,
        /// <summary>
        /// 设备停止监控,设备一键转啦功能被禁用
        /// </summary>
        [Description("设备停止监控")]
        Disabled = 1004,

    }
}