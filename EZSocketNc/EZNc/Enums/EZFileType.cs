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
    public enum EZFileType 
    {
        /// <summary>
        /// 读取目录信息
        /// </summary>
        EZNC_DISK_DIRTYPE = 0x10000,     // 目录类型
        /// <summary>
        /// 读取注释信息(仅限NC控制单元本体侧)
        /// </summary>
        EZNC_DISK_COMMENT = 0x4,       // 注释
        /// <summary>
        /// 读取日期信息(仅限计算机侧)
        /// </summary>
        EZNC_DISK_DATE = 0x2,            // 日期
        /// <summary>
        /// 读取大小信息
        /// </summary>
        EZNC_DISK_SIZE = 0x1,            // 大小
    }

}
