
using EZSocketNc.Mqtts;

using JAgentServiceProtocol;

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace EZSocketNc.Commons
{
    public class Generics
    {
        public static JAgentContext Context;
        /// <summary>
        /// Mqtt上下文配置
        /// </summary>
        public static MqttConfig MqttConfig;

        /// <summary>
        /// 当前电脑的NTID账号
        /// </summary>
        public static string CurrNTID = Environment.UserName;
        /// <summary>
        /// 当前电脑的网络名称
        /// </summary>
        public static string HostName { get { return Dns.GetHostName(); } }

        private static string hostIp4;
        /// <summary>
        /// 当前电脑的IP地址
        /// </summary>
        public static string HostIp4
        {
            get
            {
                if (string.IsNullOrWhiteSpace(hostIp4))
                {
                    var host = System.Net.Dns.GetHostEntry(HostName);
                    var ip4paddress = host
                        .AddressList
                        .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                    hostIp4 = ip4paddress.ToString();
                }
                return hostIp4;
            }
        }
    }
}
