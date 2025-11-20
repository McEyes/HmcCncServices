using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc
{
    public static class EZNcDef
    {

        public const int S_OK = 0;// 正常结束
        public const int S_FALSE = -1;// 通信失败

        // 常量定义
        /// <summary>
        /// 真，成功
        /// </summary>
        public const int EZ_TRUE = 1;                     // 真
        /// <summary>
        /// 假,十八
        /// </summary>
        public const int EZ_FALSE = 0;                    // 假

        #region 设备型号
        public const int EZNC_SYS_MAGICCARD64 = 0;       // MELDASMAGIC Card64
        public const int EZNC_SYS_MAGICBOARD64 = 1;      // MELDASMAGIC64
        public const int EZNC_SYS_MELDAS6X5L = 2;        // MELDAS600L(M6x5L)
        public const int EZNC_SYS_MELDAS6X5M = 3;        // MELDAS600M(M6x5M)
        public const int EZNC_SYS_MELDASC6C64 = 4;       // MELDASC6C64
        public const int EZNC_SYS_MELDAS700L = 5;        // MELDAS700L
        public const int EZNC_SYS_MELDAS700M = 6;        // MELDAS700M
        public const int EZNC_SYS_MELDASC70 = 7;         // MELDASC70
        public const int EZNC_SYS_MELDAS800L = 8;        // MELDAS800L
        public const int EZNC_SYS_MELDAS800M = 9;        // MELDAS800M

        #endregion 设备型号


        public const int EZNC_SYS_MULTI = 0x00010000;    // 多系统

        public const int EZNC_PLCAXIS = 255;              // PLC轴

        #region IEZNcProgram2 程序类型
        /// <summary>
        /// 主程序
        /// </summary>
        public const int EZNC_MAINPRG = 0;                // 主程序
        /// <summary>
        /// 子程序
        /// </summary>
        public const int EZNC_SUBPRG = 1;                 // 子程序
        #endregion IEZNcProgram2 程序类型

        public const int EZNC_MCODE1 = 0;                 // M代码1
        public const int EZNC_MCODE2 = 10;                // M代码2
        public const int EZNC_MCODE3 = 20;                // M代码3
        public const int EZNC_MCODE4 = 30;                // M代码4
        public const int EZNC_SCODE1 = 1;                 // S代码1
        public const int EZNC_TCODE1 = 2;                 // T代码1
        public const int EZNC_BCODE1 = 3;                 // B代码1

        public const int EZNC_M = 0;                       // M代码
        public const int EZNC_S = 1;                       // S代码
        public const int EZNC_T = 2;                       // T代码
        public const int EZNC_B = 3;                       // B代码

        public const int EZNC_PRG_MAXNUM = 0;             // 最大程序数量
        public const int EZNC_PRG_CURNUM = 1;             // 当前程序数量
        public const int EZNC_PRG_RESTNUM = 2;            // 剩余程序数量
        public const int EZNC_PRG_CHARNUM = 3;            // 字符程序数量
        public const int EZNC_PRG_RESTCHARNUM = 4;        // 剩余字符程序数量

        public const int EZNC_DISK_DIRTYPE = 0x10000;     // 目录类型
        public const int EZNC_DISK_COMMENT = 0x4;         // 注释
        public const int EZNC_DISK_DATE = 0x2;            // 日期
        public const int EZNC_DISK_SIZE = 0x1;            // 大小

        public const int EZNC_COMMACT_TRUE = 0x1;         // 命令动作为真
        public const int EZNC_COMMACT_FALSE = 0x0;        // 命令动作为假

        public const int EZ_T_CHAR = 17;                   // VARIANT类型 (byte)
        public const int EZ_T_SHORT = 2;                   // VARIANT类型 (short)
        public const int EZ_T_LONG = 3;                    // VARIANT类型 (long)
        public const int EZ_T_DOUBLE = 5;                  // VARIANT类型 (double)
        public const int EZ_T_STR = 8;                     // VARIANT类型 (bstr)
        public const int EZ_T_DLONG = 16384;               // VARIANT类型 (void)

        #region IEZNcSystem2::GetAlarm2 获取报警信息
        /// <summary>
        /// 获取所有报警
        /// </summary>
        public const int M_ALM_ALL_ALARM = 0;              // 所有报警
        /// <summary>
        /// 获取NC报警
        /// </summary>
        public const int M_ALM_NC_ALARM = 0x100;           // NC报警
        /// <summary>
        /// 获取停止代码报警
        /// </summary>
        public const int M_ALM_STOP_CODE = 0x200;          // 停止代码报警
        /// <summary>
        /// 获取PLC报警
        /// </summary>
        public const int M_ALM_PLC_ALARM = 0x300;          // PLC报警
        /// <summary>
        /// 获取操作信息报警
        /// </summary>
        public const int M_ALM_OPE_MSG = 0x400;            // 操作信息
        /// <summary>
        /// 获取警告信息
        /// </summary>
        public const int M_ALM_WARNING = 0x500;            // 警告
        #endregion IEZNcSystem2::GetAlarm2 获取报警信息

        public const int M_ALM_NC_SYSTEM = 0x101;          // NC系统报警
        public const int M_ALM_NC_SERVO = 0x102;           // NC伺服报警
        public const int M_ALM_NC_MCP = 0x103;             // NC MCP报警
        public const int M_ALM_NC_BASICPLC = 0x104;        // NC基本PLC报警
        public const int M_ALM_NC_USERPLC = 0x105;         // NC用户PLC报警
        public const int M_ALM_NC_PROGRAM = 0x106;         // NC程序报警
        public const int M_ALM_NC_SERVO_WARNING = 0x107;   // NC伺服警告
        public const int M_ALM_NC_MCP_WARNING = 0x108;     // NC MCP警告
        public const int M_ALM_NC_SYSTEM_WARNING = 0x109;  // NC系统警告
        public const int M_ALM_NC_OPERATION = 0x10A;       // NC操作报警
        public const int M_ALM_OPE_ALARM = 0x10B;          // 操作报警

        public const int EZNC_PLC_1SHOT = 0x10;            // PLC一击
        public const int EZNC_PLC_MODAL = 0x20;             // PLC模态
        public const int EZNC_PLC_BIT_FLG = 0x1;           // PLC位标志
        public const int EZNC_PLC_BYTE_FLG = 0x2;          // PLC字节标志
        public const int EZNC_PLC_WORD_FLG = 0x4;          // PLC字标志
        public const int EZNC_PLC_DWORD_FLG = 0x8;         // PLC双字标志
        public const int EZNC_PLC_BIT = (EZNC_PLC_BIT_FLG | EZNC_PLC_1SHOT);  // PLC位
        public const int EZNC_PLC_BYTE = (EZNC_PLC_BYTE_FLG | EZNC_PLC_1SHOT); // PLC字节
        public const int EZNC_PLC_WORD = (EZNC_PLC_WORD_FLG | EZNC_PLC_1SHOT); // PLC字
        public const int EZNC_PLC_DWORD = (EZNC_PLC_DWORD_FLG | EZNC_PLC_1SHOT); // PLC双字

        #region IEZNcCommunication3::Close2 线路关闭
        /// <summary>
        /// 不进行NC系统的复位
        /// </summary>
        public const int EZNC_RESET_NONE = 0;              // 无重置
        /// <summary>
        /// 对当前打开的NC系统进行复位。
        /// </summary>
        public const int EZNC_RESET_SIMPLE = 1;            // 简单重置
        /// <summary>
        /// 对所有的NC系统进行复位。
        /// </summary>
        public const int EZNC_RESET_ALL = 2;               // 全部重置
        #endregion IEZNcCommunication3::Close2 线路关闭

        public const int EZNC_FILE_INIT = 0;               // 文件初始化
        public const int EZNC_FILE_READ = 1;               // 读取文件
        public const int EZNC_FILE_WRITE = 2;              // 写入文件
        public const int EZNC_FILE_OVERWRITE = 3;          // 覆盖文件

        public const int EZNC_FILE_OPEN = 1;               // 打开文件
        public const int EZNC_FILE_CREATE = 2;             // 创建文件

        public const int EZNC_FILE_MODE_UNNCPROGRAM = 0;   // 打开文件为NC程序模式
        public const int EZNC_FILE_MODE_NCPROGRAM = 1;     // 打开文件为NC程序模式
    }
}
