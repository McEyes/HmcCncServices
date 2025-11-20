
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Packets;
using System.Net;
using EZSocketNc.Mqtts.Dtos;
using EZSocketNc.Mqtts;
using EZSocketNc.Configs;

namespace EZSocketNc.Mqtts
{
    public interface IMqttClientService
    {

        /// <summary>
        /// 是否监听第三方链接
        /// </summary>
        bool CheckOtherSysConnect { get; set; }


        /// <summary>
        /// 设备配置信息
        /// </summary>
        List<IDeviceConfig> EquipmentList { get; }

        Task InitMachineList();

        int BKDRHash(string str);


        Task StartAsync();

        Task RestartAsync();

        Task StopAsync();

        bool IsConnected { get; }

        BaseMsg<T> CreateMsg<T>(IDeviceConfig equipment, T data = null) where T : class, new();
        BaseMsg<dynamic> CreateMsg(string messageType, IDeviceConfig equipment, dynamic data);
        BaseMsg CreateMsg(IDeviceConfig equipment);

        T GetMsg<T>(IDeviceConfig equipment, T msg = null) where T : BaseMsg, new();
        void SetBaseInfo(BaseMsg msg, IDeviceConfig equipment);
        Task<bool> SendAsync(BaseMsg msg, IDeviceConfig equipment, int tryTimes = 3);
        List<IDeviceConfig> MachineList { get; }
    }

}
