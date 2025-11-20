using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 公共代码（所有命令）
    /// </summary>
    public enum EZArgCmd : uint
    {
        // 公共代码（所有命令）
        #region 公共代码（所有命令）
        ME_ARG1 = 0x81,
        ME_ARG2 = ME_ARG1 + 0x1,
        ME_ARG3 = ME_ARG1 + 0x2,
        ME_ARG4 = ME_ARG1 + 0x3,
        ME_ARG5 = ME_ARG1 + 0x4,
        ME_ARG6 = ME_ARG1 + 0x5,
        ME_ARG7 = ME_ARG1 + 0x6,
        ME_ARG8 = ME_ARG1 + 0x7,
        ME_ARG9 = ME_ARG1 + 0x8,
        ME_ARG10 = ME_ARG1 + 0x9,
        ME_ARG11 = ME_ARG1 + 0xA,
        ME_ARG12 = ME_ARG1 + 0xB,
        ME_ARG13 = ME_ARG1 + 0xC,
        ME_ARG14 = ME_ARG1 + 0xD,
        ME_ARG15 = ME_ARG1 + 0xE,
        #endregion 公共代码（所有命令）

    }
}

