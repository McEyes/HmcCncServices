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
    public enum EZSystemType
    {
        #region 设备型号
        [Description("CNC C64")]
        CNC_C64 = 0,       // MELDASMAGIC Card64
        //EZNC_SYS_MAGICBOARD64 = 1,     // MELDASMAGIC64
        //EZNC_SYS_MELDAS6X5L = 2,       // MELDAS600L(M6x5L)
        //EZNC_SYS_MELDAS6X5M = 3,       // MELDAS600M(M6x5M)
        //EZNC_SYS_MELDASC6C64 = 4,      // MELDASC6C64
        [Description("CNC M700L")]
        CNC_M700L = 5,       // MELDAS700L
        [Description("CNC M700M")]
        CNC_M700M = 6,       // MELDAS700M
        [Description("CNC C70")]
        CNC_C70 = 7,        // MELDASC70
        /// <summary>
        /// CNC M800L
        /// </summary>
        [Description("CNC M800L")]
        CNC_M800L = 8,       // MELDAS800L
        /// <summary>
        /// CNC M800M
        /// </summary>
        [Description("CNC M800M")]
        CNC_M800M = 9,       //EZNC_SYS_MELDAS800M MELDAS800M

        /// <summary>
        /// 使线路支持多线程,
        /// M700/M800系列可通过以下值的组合支持多线程。
        /// CNC_M800M|NC_SYS_MULTI 表示支持多路系统,
        /// eg:lSytemType = EZNC_SYS_MELDAS800M | EZNC_SYS_MULTI
        /// </summary>
        [Description("CNC MULTI")]
        NC_SYS_MULTI = 0x00010000,    // 多系统
        #endregion 设备型号


        [Description("CNC西门子")]
        Siemens = 18    // 多系统
    }


    //#region 设备型号
    //public const int EZNC_SYS_MAGICCARD64 = 0;       // MELDASMAGIC Card64
    //public const int EZNC_SYS_MAGICBOARD64 = 1;      // MELDASMAGIC64
    //public const int EZNC_SYS_MELDAS6X5L = 2;        // MELDAS600L(M6x5L)
    //public const int EZNC_SYS_MELDAS6X5M = 3;        // MELDAS600M(M6x5M)
    //public const int EZNC_SYS_MELDASC6C64 = 4;       // MELDASC6C64
    //public const int EZNC_SYS_MELDAS700L = 5;        // MELDAS700L
    //public const int EZNC_SYS_MELDAS700M = 6;        // MELDAS700M
    //public const int EZNC_SYS_MELDASC70 = 7;         // MELDASC70
    //public const int EZNC_SYS_MELDAS800L = 8;        // MELDAS800L
    //public const int EZNC_SYS_MELDAS800M = 9;        // MELDAS800M

    //#endregion 设备型号
}
