using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 链接错误类型
    /// 网络相关错误代码
    /// </summary>
    public enum EZeNetErrorType:uint
    {
        /// <summary>
        /// 正常结束
        /// </summary>
        [Description("正常结束")]
        OK = 0,// 正常结束
        ///// <summary>
        ///// 错误标志
        ///// </summary>
        //[Description("错误标志")]
        //ME_ERR_FLG = 0x80000000,
        /// <summary>
        /// 网络已打开
        /// </summary>
        [Description("网络已打开")]
        EZNC_ENET_ALREADYOPEN = 0x82020001,                  // 网络已打开
        /// <summary>
        /// 网络未打开
        /// </summary>
        [Description("网络未打开")]
        EZNC_ENET_NOTOPEN = 0x82020002,                          // 网络未打开
        /// <summary>
        /// 网络卡不存在
        /// </summary>
        [Description("网络未打开")]
        EZNC_ENET_CARDNOTEXIST = 0x82020004,                // 网络卡不存在
        /// <summary>
        /// 通道错误
        /// </summary>
        [Description("通道错误")]
        EZNC_ENET_BADCHANNEL = 0x82020006,                    // 通道错误
        /// <summary>
        /// 文件描述符错误
        /// </summary>
        [Description("文件描述符错误")]
        EZNC_ENET_BADFD = 0x82020007,                              // 文件描述符错误
        /// <summary>
        /// 未连接
        /// </summary>
        [Description("未连接")]
        EZNC_ENET_NOTCONNECT = 0x8202000A,                    // 未连接
        /// <summary>
        /// 无法关闭
        /// </summary>
        [Description("无法关闭")]
        EZNC_ENET_NOTCLOSE = 0x8202000B,                        // 无法关闭
        /// <summary>
        /// 超时
        /// </summary>
        [Description("超时")]
        EZNC_ENET_TIMEOUT = 0x82020014,                          // 超时
        /// <summary>
        /// 数据错误
        /// </summary>
        [Description("数据错误")]
        EZNC_ENET_DATAERR = 0x82020015,                          // 数据错误
        /// <summary>
        /// 操作已取消
        /// </summary>
        [Description("操作已取消")]
        EZNC_ENET_CANCELED = 0x82020016,                        // 操作已取消
        /// <summary>
        /// 非法大小
        /// </summary>
        [Description("非法大小")]
        EZNC_ENET_ILLEGALSIZE = 0x82020017,                  // 非法大小
        /// <summary>
        /// 任务退出
        /// </summary>
        [Description("任务退出")]
        EZNC_ENET_TASKQUIT = 0x82020018,                        // 任务退出
        /// <summary>
        /// 未知函数
        /// </summary>
        [Description("未知函数")]
        EZNC_ENET_UNKNOWNFUNC = 0x82020032,                  // 未知函数
        /// <summary>
        /// 设置数据错误
        /// </summary>
        [Description("设置数据错误")]
        EZNC_ENET_SETDATAERR = 0x82020033,                    // 设置数据错误
    }

}
