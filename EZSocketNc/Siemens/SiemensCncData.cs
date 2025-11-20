using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZSocketNc.Siemens
{
    public class SiemensCncData
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 设备ip
        /// </summary>
        public string device_id { get; set; }

        public RuntimeData Data {get;set;}

        /// <summary>
        /// 消息数据
        /// </summary>
        public List<KeyValueData> DataList { get; set; }

        public List<Alarm> AlarmList { get; set; }

    }

    public class KeyValueData{
        public string Key { get; set;}
        public string Value { get; set; }
    }
}