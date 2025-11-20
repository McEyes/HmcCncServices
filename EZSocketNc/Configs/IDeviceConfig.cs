

using EZSocketNc.Mqtts;

namespace EZSocketNc.Configs
{
    /// <summary>
    /// 设备监控：收集设备状态信息，上报mqtt状态，和下发mqtt指令
    /// </summary>
    public interface IDeviceConfig
    {
        string Id { get; set; }
        string Name { get; set; }
        string HostName { get; set; }
        string Kind { get; set; }
        string Workcell { get; set; }
        string Bay { get; set; }
        bool IsDebug { get; set; }
        bool? Enable { get; set; }
        //bool? IsRetry { get; set; }
        int Retries { get; set; }
        IMqttConfig Mqtt { get; set; }

    }
}
