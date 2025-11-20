using EZSocketNc.Configs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 通讯基础配置
    /// 通讯SetTCPIPProtocol返回结果：
    /// S_OK：正常结束
    /// EZNC_COMM_ALREADYOPENED: 正在通信中，无法设定
    /// EZ_ERR_DATA_RANGE：IP地址或端口号不正确
    /// EZ_ERR_NOT_SUPPORT：不支持
    /// Open3返回结果：
    /// EZ_ERR_DATA_TYPE：参数的数据类型不正确
    /// EZ_ERR_DATA_RANGE：参数的数据范围不正确
    /// EZNC_SYSFUNC_IOCTL_ADDR：NC控制单元编号不正确
    /// EZNC_SYSFUNC_IOCTL_NOTOPEN：软元件未打开
    /// EZNC_SYSFUNC_IOCTL_DATA：通信参数数据范围不正确
    /// EZNC_COMM_NOTSETUP_PROTOCOL：TCP/IP通信未设定(仅限M700/M800系列)
    /// EZNC_COMM_NOTMODULE：无下位模块
    /// EZNC_COMM_CREATEPC：无法生成EZSocketPc对象(仅限C70)
    /// EZNC_COMM_CANNOT_OPEN：在自动化调用中，连接本地主机时，未指定主机名
    /// EZNC_LOCALHOST。
    /// </summary>
    public class EZSocketConfig
    {
        public EZSocketConfig()
        {

        }
        //public EZSocketConfig(CncDeviceConfig config)
        //{
        //    Ip = config.Ip;
        //    Port = config.Port;
        //    HostName = config.HostName;
        //    SystemType = config.SystemType;
        //}

        /// <summary>
        /// 指定连接目标M700/M800系列的IP地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 指定连接目标M700/M800系列的端口号。
        /// 关于端口号，请确认NC控制单元侧的设定,
        /// M700/M800系列时，端口号为683。
        /// 通讯端口，默认都是683
        /// </summary>
        public int Port { get; set; } = 683;

        public EZSystemType SystemType { get; set; } = EZSystemType.CNC_M800M | EZSystemType.NC_SYS_MULTI;
        /// <summary>
        /// 指定NC控制单元编号。连接多个NC控制单元时，
        /// 请对各NC控制单元分别指定不同的NC控制单元编号。
        /// 值：1～255，
        /// 每个ip需要独立的MachineNo，同一个ip，MachineNo最好固定
        /// </summary>
        public int MachineNo { get; set; } = 1;
        /// <summary>
        /// 1~3000 超时值(单位100ms)
        /// (在M700/M800系列中此值为10以上，发生超时错误时，请增大值。)
        /// </summary>
        public int TimeOut { get; set; } = 10;//3秒
        /// <summary>
        /// NC系统的主机:特指安装了EZSocket接口的PC电脑
        /// 指定要连接的NC系统的主机名。也可指定IP地址。
        /// 要连接本地主机时，请指定字符串”EZNC_LOCALHOST”。
        /// </summary>
        public string HostName { get; set; } = "EZNC_LOCALHOST";


        public string Key { get { return $"{Ip}:{Port}"; } }

        public bool IsEnable { get; set; } = true;
        public int DataReadFreq { get; set; } = 2;
    }
}
