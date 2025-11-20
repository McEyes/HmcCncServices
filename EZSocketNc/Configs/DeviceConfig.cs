using EZSocketNc.Mqtts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.Configs
{
    public class DeviceConfig : IDeviceConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HostName  { get; set; }
        public string Protocol  { get; set; }
        public string Kind { get; set; }
        public string Workcell { get; set; }
        public string Bay { get; set; }

        //public bool? IsRetry { get; set; } = true;
        public bool? Enable { get; set; } = true;
        public int Retries { get; set; } = 3;
        public IMqttConfig Mqtt { get; set; }
        public bool IsDebug { get; set; } = false;
    }
}
