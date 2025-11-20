using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 通信相关错误代码
    /// </summary>
    public enum EZSetTCPIPProtocolErrorType : uint
    {

        /// <summary>
        /// 正常结束
        /// </summary>
        [Description("正常结束")]
        OK = 0,// 正常结束
        #region 通信相关错误代码

        // 通信相关错误代码
        /// <summary>
        /// 无法打开通信
        /// </summary>
        [Description("无法打开通信")]
        EZNC_COMM_CANNOT_OPEN = 0x80B00301, // 无法打开通信
        /// <summary>
        /// TCP/IP协议未设置
        /// </summary>
        [Description("TCP/IP协议未设置")]
        EZNC_COMM_NOTSETUP_PROTOCOL = 0x80B00302, // TCP/IP协议未设置
        /// <summary>
        /// 通信已打开
        /// </summary>
        [Description("通信已打开")]
        EZNC_COMM_ALREADYOPENED = 0x80B00303, // 通信已打开
        /// <summary>
        /// 模块不存在
        /// </summary>
        [Description("模块不存在")]
        EZNC_COMM_NOTMODULE = 0x80B00304, // 模块不存在
        /// <summary>
        /// 创建EZSocketPC失败
        /// </summary>
        [Description("创建EZSocketPC失败")]
        EZNC_COMM_CREATEPC = 0x80B00305, // 创建EZSocketPC失败

        #endregion 通信相关错误代码
    }

}
