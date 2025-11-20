using EZSocketNc.Configs;
using EZSocketNc.Mqtts;

namespace HmcCncServices.Configs
{
    public class CncServiceConfig
    {
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Workcell { get; set; }
        public string Bay { get; set; }
        public bool? Enable { get; set; } = true;
        public int HeartBeat { get; set; } = 1;
        public MqttConfig Mqtt { get; set; }
        public CncDeviceConfig[] CncDevices { get; set; }

    }
}
