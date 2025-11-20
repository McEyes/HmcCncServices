using EZSocketNc.Commons;

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
    public class EZNcVersion
    {
        /// <summary>
        /// NC系统号
        /// </summary>
        public string NcSystemNo { get; set; }
        /// <summary>
        ///NC系统名称
        /// </summary>
        public string NcSystemName { get; set; }
        /// <summary>
        /// tPLC系统号
        /// </summary>
        public string PlcSystemNo { get; set; }

        public void FormatStr(string ncVer)
        {
            if (string.IsNullOrWhiteSpace(ncVer)) return;
            var datas = ncVer.Split(new char[] { '\t', '\0', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (datas.Length >= 3)
            {
                NcSystemNo = datas[0];
                NcSystemName = datas[1];
                PlcSystemNo = datas[2];
            }
            else if (datas.Length == 2)
            {
                NcSystemNo = datas[0];
                NcSystemName = datas[1];
            }
            else if (datas.Length == 1)
            {
                NcSystemNo = datas[0];
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(NcSystemNo))
                return $"{NcSystemNo}\r\n{NcSystemName}\r\n{PlcSystemNo}";
            return string.Empty;
        }
    }
}
