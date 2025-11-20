using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using System.Xml.XPath;

using EZSocketNc.Commons;
using EZSocketNc.Configs;
using EZSocketNc.Extensions;
using EZSocketNc.Mqtts.Dtos;
using EZSocketNc.EZNc;

namespace EZSocketNc.Interface
{
    public interface IEZSocket
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        string DeviceType { get; }
        string Name { get; }
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        IDeviceConfig DeviceConfig { get; set; }
        /// <summary>
        /// 
        /// </summary>
        EZSocketConfig SocketConfig { get; set; }
        /// <summary>
        /// 
        /// </summary>
        EZCncInfo EquipmentInfo { get; set; }
        /// <summary>
        /// 通讯结果
        /// </summary>
        EZResult EzResult { get; }

        bool IsInit { get; }

        bool IsOpen { get; }

        ConcurrentBag<string> errMsg { get; }

        //protected readonly ILogger Log;
        #endregion 属性


        /// <summary>
        /// 主动采集设备信息
        /// </summary>
        void StartMonitor();

        /// <summary>
        /// 停止采集
        /// </summary>
        void StopMonitor();


        /// <summary>
        /// 设备连接，可以关闭在打开n次
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        IResult Conn(EZSocketConfig config = null);

        /// <summary>
        /// 设备退出关闭
        /// </summary>
        /// <returns></returns>
        IResult Close();

        /// <summary>
        /// 实时刷新设备状态信息
        /// </summary>
        /// <returns></returns>
        EZCncInfo ReadEquipmentState();

        List<EquipmentInformationDataParameter> GetChangeInfo();

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="bstrFileName"></param>
        /// <param name="pbData"></param>
        /// <param name="lMode"></param>
        /// <returns></returns>
        IResult WriteFile(string bstrFileName, byte[] pbData, int lMode = 3);
        void Dispose();
    }
}