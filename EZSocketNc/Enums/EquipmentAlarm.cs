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
    /// 设备报警信息
    /// </summary>
    public enum EquipmentAlarm
    {
        /// <summary>
        /// 无报警
        /// </summary>
        [Description("")]
        None = 0,
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