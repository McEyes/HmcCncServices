using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 通讯基础配置
    /// </summary>
    public class EZPosition
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }
        /// <summary>
        ///Y坐标
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z坐标
        /// </summary>
        public double Z { get; set; }

        public override string ToString()
        {
            return $"X:{X.ToString("0.000")},Y:{Y.ToString("0.000")},Z:{Z.ToString("0.000")}";
        }
    }
}
