using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 三菱设备型号，
    ///  NC系统类型
    /// </summary>
    public enum EZRunLight
    {
        /// <summary>
        /// 所有灯熄灭
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 绿灯:M8167
        /// </summary>
        [Description("绿灯")]
        Green = 1,
        /// <summary>
        /// 黄灯：M8166
        /// </summary>
        [Description("黄灯")]
        Yellow = 2,
        /// <summary>
        /// 绿灯:M8165
        /// </summary>
        [Description("红灯")]
        Red = 3,
    }

}
