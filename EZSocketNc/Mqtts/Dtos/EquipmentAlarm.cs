using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EZSocketNc.Mqtts.Dtos
{
    public class EquipmentAlarm : BaseMsg
    {
        [JsonProperty("data")]
        public EquipmentAlarmData Data { get; set; }
    }

    public class EquipmentAlarmData
    {
        public EquipmentAlarmData()
        {
        }
        public EquipmentAlarmData(string alarmInstanceId, string alarmCode, string alarmMsg) : this()
        {

            AlarmInstanceId = alarmInstanceId;
            AlarmCode = alarmCode;
            AlarmMsg = alarmMsg;
        }
        public EquipmentAlarmData(string alarmInstanceId, string alarmCode,string alarmType, string alarmMsg) : this()
        {

            AlarmInstanceId = alarmInstanceId;
            AlarmCode = alarmCode;
            AlarmType = alarmType;
            AlarmMsg = alarmMsg;
        }
        [JsonProperty("alarmInstanceId")]
        public string AlarmInstanceId { get; set; }
        [JsonProperty("alarmCode")]
        public string AlarmCode { get; set; }
        [JsonProperty("alarmType")]
        public string AlarmType { get; set; }
        [JsonProperty("alarmMsg")]
        public string AlarmMsg { get; set; }
    }
}
