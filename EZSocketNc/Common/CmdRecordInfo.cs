using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.Commons
{
    public class CmdRecordInfo
    {
        public CmdRecordInfo(string recipeName, string machineId)
        {
            RecipeName = recipeName;
            MachineId = machineId;
            this.LastTime = DateTime.Now;
        }

        public string RecipeName { get; set; }

        public string MachineId { get; set; }
        /// <summary>
        /// 最后更新时间，预留
        /// </summary>
        public DateTime LastTime { get; set; }
    }
}
