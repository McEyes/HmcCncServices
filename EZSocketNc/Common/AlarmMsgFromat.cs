using EZSocketNc.Mqtts.Dtos;

using System.Collections.Generic;

namespace EZSocketNc.Commons
{
    public class AlarmMsgFromat
    {
        public static List<EquipmentAlarmData> FormatAlarmsg(string[] alarmsgs)
        {
            var list = new List<EquipmentAlarmData>();
            if (alarmsgs == null) return list;
            alarmsgs.ForEach(item =>
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    list.Add(FormatAlarmsg(item));
                }
            });
            return list;
        }

        public static EquipmentAlarmData FormatAlarmsg(string alarmsg)
        {
            if (string.IsNullOrWhiteSpace(alarmsg)) return null;
            var msgs = alarmsg.Split(new char[] { '\t', '.', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            // EZSocketNc.Utils.LogHelper.Info($"alarmsg:{msgs.Length}\r\n{alarmsg}");
            if (msgs.Length > 2)
            {
                if (int.TryParse(msgs[1], out _))
                    return new EquipmentAlarmData() { AlarmInstanceId = msgs[0], AlarmCode = msgs[1], AlarmMsg = msgs[2], AlarmType = msgs[0] };
                else
                    return new EquipmentAlarmData() { AlarmInstanceId = msgs[0], AlarmCode = msgs[2], AlarmMsg = msgs[1], AlarmType = msgs[0] };
            }
            else if (msgs.Length > 1)
                return new EquipmentAlarmData() { AlarmInstanceId = msgs[0], AlarmCode = msgs[0], AlarmMsg = alarmsg, AlarmType = msgs[0] };
            return new EquipmentAlarmData() { AlarmInstanceId = msgs[0], AlarmCode = msgs[0], AlarmMsg = alarmsg, AlarmType = msgs[0] };
        }
    }
}
