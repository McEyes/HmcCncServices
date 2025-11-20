using EZSocketNc.EZNc;

namespace EZSocketNc.Configs
{
    public class CncDeviceConfig : DeviceConfig
    {
        public EZSocketConfig Socket { get; set; }
        //public string Ip { get; set; }
        //public int Port { get; set; } = 683;
        //public EZSystemType SystemType { get; set; } = EZSystemType.CNC_M800M | EZSystemType.NC_SYS_MULTI;
        ///// <summary>
        ///// 1~3000 超时值(单位100ms)
        ///// (在M700/M800系列中此值为10以上，发生超时错误时，请增大值。)
        ///// </summary>
        //public int TimeOut { get; set; } = 10;//3秒
        ///// <summary>
        ///// NC系统的主机:特指安装了EZSocket接口的PC电脑
        ///// 指定要连接的NC系统的主机名。也可指定IP地址。
        ///// 要连接本地主机时，请指定字符串”EZNC_LOCALHOST”。
        ///// </summary>
        //public string HostName { get; set; } = "EZNC_LOCALHOST";
        ///// <summary>
        ///// 数据采集频率，单位100毫秒
        ///// </summary>
        //public int DataReadFreq { get; set; } = 5;

    }
}
