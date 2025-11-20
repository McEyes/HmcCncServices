
namespace EZSocketNc.Configs
{
    /// <summary>
    /// 设备监控：收集设备状态信息，上报mqtt状态，和下发mqtt指令
    /// </summary>
    public interface IMqttConfig
    {
        string Id { get; set; }
        string Kind { get; set; }  
        string HostName { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string User { get; set; }
        string Password { get; set; }
        int Qos { get; set; }
        int Retries { get; set; }


        string Topic { get; }
        string OutTopic { get; }
        string Source { get; set; }
        int HeartBeat { get; set; }
        bool WithSubscribe { get; set; }
        bool CheckOtherSysConnect { get; set; }

    }
}
