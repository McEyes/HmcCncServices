using EZSocketNc.Configs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.Mqtts
{
    public class MqttConfig :IMqttConfig
    {
        public string Id { get; set; }
        private string _kind;
        public string Kind { get { return _kind; } set { if (value != null) _kind = value.ToLower(); else _kind = value; } }
        public string HostName { get; set; } 
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Qos { get; set; }
        //public string Parser { get; set; }
        public int Retries { get; set; } = 3;

        public string Topic => $"in/{Kind}/{Id}";

        public string OutTopic => $"out/{Kind}/{Id}";

        public bool CheckOtherSysConnect { get; set; } = true;
        public int HeartBeat { get; set; } = 60;
        public bool WithSubscribe { get; set; } = true;
        public string Source { get; set; }

        public MqttConfig Clone(IDeviceConfig config = null)
        {
            var data =  base.MemberwiseClone() as MqttConfig;
            if (config != null && config.Mqtt != null)
            {
                data.Id = config.Mqtt.Id;
                data.Kind = config.Mqtt.Kind;
            }
            return data;
        }
    }
}
