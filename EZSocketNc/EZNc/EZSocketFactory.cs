using EZSocketNc.Configs;
using EZSocketNc.Interface;
using EZSocketNc.Siemens;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 单个通讯信息
    /// </summary>
    public class EZSocketFactory
    {
        private static ConcurrentDictionary<string, IEZSocket> EZSockeDicts = new ConcurrentDictionary<string, IEZSocket>();
        private static ConcurrentBag<int> _MachineNoBag;
        public static ConcurrentBag<int> MachineNoBag
        {
            get
            {
                if (_MachineNoBag == null)
                {
                    _MachineNoBag = new ConcurrentBag<int>();
                    for (var i = 1; i <= 255; i++)
                        _MachineNoBag.Add(i);
                }
                return _MachineNoBag;
            }
        }

        public static dynamic CreateEZNcCom(EZSocketConfig config)
        {
            //EZSocket对象的声明 
            Type objClassType = Type.GetTypeFromProgID("EZNcAut.DispEZNcCommunication");
            //EZSocket对象的生成 
            object objEZNC = Activator.CreateInstance(objClassType);
            return objEZNC;
        }


        public static IEZSocket CreateEZSocket(CncDeviceConfig config)
        {
            IEZSocket ezsocket = null;
            if (config == null && config.Socket == null) return ezsocket;
            if (!EZSockeDicts.ContainsKey(config.Socket.Key))
            {
                if (MachineNoBag.TryTake(out int no)) config.Socket.MachineNo = no;
                else
                {
                    throw new Exception("255个Machine No已经分配完，如需添加新的设备请先关闭其他的。");
                }
                if (config.Protocol == "melsec")
                    ezsocket = new EZSocket(config);
                else if (config.Protocol == "simenes")
                    ezsocket = new SiemensSocket(config);
                EZSockeDicts.TryAdd(config.Socket.Key, ezsocket);
            }
            else if (EZSockeDicts.TryGetValue(config.Socket.Key, out ezsocket))
            {
                return ezsocket;
            }
            return ezsocket;
        }
        //private static object lockObj = new object();
        public static IEZSocket CreateEZSocket(EZSocketConfig config)
        {
            IEZSocket ezsocket = null;
            if (config == null) return ezsocket;
            if (!EZSockeDicts.ContainsKey(config.Key))
            {
                if (MachineNoBag.TryTake(out int no)) config.MachineNo = no;
                else
                {
                    throw new Exception("255个Machine No已经分配完，如需添加新的设备请先关闭其他的。");
                }
                if (config.SystemType == EZSocketNc.EZNc.EZSystemType.Siemens)
                    ezsocket = new SiemensSocket(config);
                else ezsocket = new EZSocket(config);
                EZSockeDicts.TryAdd(config.Key, ezsocket);
            }
            else if (EZSockeDicts.TryGetValue(config.Key, out ezsocket))
            {
                return ezsocket;
            }
            return ezsocket;
        }
    }

}
