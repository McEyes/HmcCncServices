

using EZSocketNc.Configs;
using EZSocketNc.Mqtts.Dtos;

using System;
using System.Net;

namespace EZSocketNc.Mqtts
{
    public static class MqttHelper
    {

        public static BaseMsg<T> CreateMsg<T>(this IMqttConfig mqttConfig, T data = null) where T : class, new()
        {
            var msg = new BaseMsg<T>();
            FillBaseField(msg, mqttConfig);
            msg.Data = data;
            msg.MessageType = typeof(T).Name;
            return msg;
        }
        public static BaseMsg<dynamic> CreateMsg(string messageType, IMqttConfig mqttConfig, dynamic data)
        {
            var msg = new BaseMsg<dynamic>();
            FillBaseField(msg, mqttConfig);
            msg.Data = data;
            msg.MessageType = messageType;
            return msg;
        }

        public static BaseMsg CreateMsg(this IMqttConfig mqttConfig)
        {
            var msg = new BaseMsg();
            FillBaseField(msg, mqttConfig);
            return msg;
        }

        public static T GetMsg<T>(this IMqttConfig mqttConfig, T msg = null) where T : BaseMsg, new()
        {
            if (msg == null) msg = new T();
            msg.MessageType = typeof(T).Name;
            FillBaseField(msg, mqttConfig);
            return msg;
        }

        public static void FillBaseField(this BaseMsg msg, IMqttConfig mqttConfig)
        {
            msg.Id = Guid.NewGuid().ToString("D");
            msg.TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            msg.HostName = mqttConfig.HostName;
            if (string.IsNullOrEmpty(msg.PanelId))
                msg.PanelId = "";
            if (string.IsNullOrWhiteSpace(msg.MessageType))
                msg.MessageType = msg.GetType().Name;
            SetBaseInfo(msg, mqttConfig);
        }

        public static void SetBaseInfo(this BaseMsg msg, IMqttConfig mqttConfig)
        {
            msg.Sender = mqttConfig.Id;
            msg.Kind = mqttConfig.Kind; 
            if (string.IsNullOrWhiteSpace(msg.HostName))
                msg.HostName = Dns.GetHostName();
            if (!string.IsNullOrWhiteSpace(mqttConfig.Source))
                msg.Source = mqttConfig.Source;
        }
    }
}
