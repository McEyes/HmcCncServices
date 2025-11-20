using System;
using System.Collections.Generic;

namespace EZSocketNc.Siemens
{
	public class RuntimeData
	{
		
        public List<Alarm> AlarmList = new List<Alarm>();


        /// <summary>
        /// 加工程序名
        /// </summary>
        public string progName { get; set; }

        /// <summary>
        /// 操作状态：0=JOG，1=MDI,2=AUTO
        /// </summary>
        public string opMode { get; set; }

        /// <summary>
        /// 当前程序段
        /// </summary>
        public string block { get; set; }

        /// <summary>
        /// 当前段
        /// </summary>
        public string blockNoStr { get; set; }
        /// <summary>
        /// 当前段号
        /// </summary>
        public string actLineNumber { get; set; }

        /// <summary>
        /// 当前有效的G命令
        /// </summary>
        public string ncFkt { get; set; }

        /// <summary>
        /// 主轴超程：是代表啥的
        /// </summary>
        public string speedovr { get; set; }

        /// <summary>
        /// 进给超程
        /// </summary>
        public string feedRateOvr { get; set; }

        /// <summary>
        /// 主轴实际速度
        /// </summary>
        public string actSpeed { get; set; }
        /// <summary>
        /// 主轴理论速度：设定速度
        /// </summary>
        public string cmdSpeed { get; set; }
        /// <summary>
        /// 主轴负载
        /// </summary>
        public string driveLoad { get; set; }
        /// <summary>
        /// 理论进给速度
        /// </summary>
        public string cmdFeedRate { get; set; }

        /// <summary>
        /// 实际进给速度
        /// </summary>
        public string actFeedRate { get; set; }

        /// <summary>
        /// X坐标轴
        /// </summary>
        public string actProgPosX { get; set; }

        /// <summary>
        /// Y坐标轴
        /// </summary>
        public string actProgPosY { get; set; }

        /// <summary>
        /// Z坐标轴
        /// </summary>
        public string actProgPosZ { get; set; }

        /// <summary>
        /// 当前道具号
        /// </summary>
        public string actTNumber { get; set; }

        /// <summary>
        /// 当前通道号
        /// </summary>
        public string channelNo { get; set; }

        /// <summary>
        /// 主轴电流：大多数设备不支持
        /// </summary>
        public string actualCurrent { get; set; }

        /// <summary>
        /// 设备状态：1中断，2停止，3运行，4等待，5取消
        /// </summary>
        public string progStatus { get; set; }

        /// <summary>
        /// 加工数量
        /// </summary>
        public string actParts { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public string reqParts { get; set; }

		
        /// <summary>
        /// 操作时间
        /// </summary>
        public string opreatingTime { get; set; }
		
        /// <summary>
        /// 循环时间？
        /// </summary>
        public string cycleTime { get; set; }
		
        /// <summary>
        /// 切割时间，加工时间
        /// </summary>
        public string cuttingTime { get; set; }
		
        /// <summary>
        /// 通电时间
        /// </summary>
        public string poweronTime { get; set; }

      
        // // Token: 0x0200000A RID: 10
        // public class Alarm1
        // {

        //     /// <summary>
        //     /// 当前通道号
        //     /// </summary>
        //     public string alarmNo { get; set; }
        //     /// <summary>
        //     /// 
        //     /// </summary>
        //     public string AlarmMsg { get; set; }

        //     /// <summary>
        //     /// 当前通道号
        //     /// </summary>
        //     public string alarmTime { get; set; }
        // }
	}
}
