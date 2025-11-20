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
    public class EZNcFileInfo
    {
        /// <summary>
        /// 驱动器名称/文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        ///日期、
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        ///注释
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 是否目录
        /// </summary>
        public bool IsDir { get; set; }

        public EZNcFileInfo[] Files { get; set; }

        public EZNcFileInfo()
        {
        }
        public EZNcFileInfo(string dataStr)
        {
            FormatStr(dataStr);
        }

        public void FormatStr(string dataStr)
        {
            if (string.IsNullOrWhiteSpace(dataStr))
                return;
            var datas = dataStr.Split(new char[] { '\t' });
            if (datas.Length >= 4)
            {
                Name = datas[0];
                Size = datas[1];
                Date = datas[2];
                Comment = datas[3].TrimEnd('\0');
            }
            else if (datas.Length == 3)
            {
                Name = datas[0];
                Size = datas[1];
                Date = datas[2].TrimEnd('\0');
            }
            else if (datas.Length == 2)
            {
                Name = datas[0];
                Size = datas[1].TrimEnd('\0');
            }
            else if (datas.Length == 1)
            {
                Name = datas[0].TrimEnd('\0');
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
