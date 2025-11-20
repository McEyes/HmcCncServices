using EZSocketNc.Commons;

using JAgentServiceProtocol;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace EZSocketNc.Utils
{
    public static class LogHelper
    {
        private static ILog _DefaultLoger, _ExceptionLoger, _ServerLoger, _DebugLoger;

        private static object logLock = new object();

        public static ILog ServerLoger
        {
            get { return _ServerLoger; }
        }

        public static ILog ExceptionLoger
        {
            get { return _ExceptionLoger; }
        }

        public static ILog DefaultLoger
        {
            get { return _DefaultLoger; }
        }

        static LogHelper()
        {
            _DefaultLoger = LogManager.GetLogger("DefaultLoger");
            _ExceptionLoger = LogManager.GetLogger("ExceptionLoger");
            _ServerLoger = LogManager.GetLogger("ServerLoger");
            _DebugLoger = LogManager.GetLogger("DebugLoger");
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        public static void Info(string msg)
        {
            _DefaultLoger.Info(msg.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.Info(msg);
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Info(string format, params object[] args)
        {
            _DefaultLoger.InfoFormat(format.SafeLogMsg(), args.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.InfoFormat(format, args);
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        public static void Warn(string msg)
        {
            _DefaultLoger.Warn(msg.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.Warn(msg);
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Warn(string format, params object[] args)
        {
            _DefaultLoger.WarnFormat(format.SafeLogMsg(), args.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.WarnFormat(format, args);
            }
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        public static void Error(string msg, Exception e)
        {
            _ExceptionLoger.Error(msg.SafeLogMsg(), e);
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.Error(msg, e);
            }
        }

        public static void Error(Exception e)
        {
            _ExceptionLoger.Error(e.Message);
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Error(string format, params object[] args)
        {
            _ExceptionLoger.ErrorFormat(format.SafeLogMsg());//, args.SafeLogMsg()
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.ErrorFormat(format, args);
            }
        }

        /// <summary>
        /// 服务器日志
        /// </summary>
        public static void Server(string msg)
        {
            _ServerLoger.Info(msg.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.InfoFormat(msg);
            }
        }

        /// <summary>
        /// 服务器日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Server(string format, params object[] args)
        {
            _ServerLoger.InfoFormat(format.SafeLogMsg(), args.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.InfoFormat(format, args);
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        public static void Debug(string msg)
        {
            _DebugLoger.Debug(msg.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.Debug(msg);
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Debug(string format, params object[] args)
        {
            _DebugLoger.DebugFormat(format.SafeLogMsg(), args.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.DebugFormat(format, args);
            }
        }

        public static void DebugFormat(string format, params object[] args)
        {
            _DebugLoger.DebugFormat(format.SafeLogMsg(), args.SafeLogMsg());
            if (Generics.Context != null && Generics.Context.Loger != null)
            {
                Generics.Context.Loger.DebugFormat(format, args);
            }
        }

        #region old
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        public static void WriteError(Type t, Exception ex, string msg = null)
        {
            ILog log = LogManager.GetLogger(t);
            if (string.IsNullOrEmpty(msg))
                msg = "Error";

            log.Error(msg.SafeLogMsg(), ex);
        }
        /// <summary>
        /// 用于采集日志的错误信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        public static void WriteWarn(Type t, Exception ex, string msg = null)
        {
            ILog log = LogManager.GetLogger(t);
            if (string.IsNullOrEmpty(msg))
                msg = "Warn";
            /*包含异常轨迹*/

            Exception exception = ex;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            msg += " 错误明细：";
            log.Warn(msg.SafeLogMsg() + exception.Message);
        }
        /// <summary>
        /// 用于采集日志的错误信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteWarn(Type t, string msg)
        {
            ILog log = LogManager.GetLogger(t);
            if (string.IsNullOrEmpty(msg))
                msg = "Warn";

            log.Warn(msg.SafeLogMsg());
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteError(Type t, string msg)
        {
            ILog log = LogManager.GetLogger(t);
            log.Error(msg.SafeLogMsg());
        }
        [Obsolete]
        public static void WriteInfo(Type t, string msg)
        {
            ILog log = LogManager.GetLogger(t);
            log.Info(msg.SafeLogMsg());
        }

        #endregion

        public static void WriteLog(string logText, long logFileLimitSize = 10485760)
        {
            lock (logLock)
            {
                DateTime currentTime = DateTime.Now;

                string currentDateString = currentTime.ToString("yyyy-MM-dd");

                string logDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\" + currentDateString;

                if (!Directory.Exists(logDirectoryPath))
                {
                    Directory.CreateDirectory(logDirectoryPath);
                }

                string logFilePrefix = currentDateString + "#";

                int logFileSequence = 1;

                string logFileExtension = ".txt";

                List<FileInfo> logDirectoryFileList = FileHelper.GetDirectoryFile(logDirectoryPath);

                if (logDirectoryFileList.Count > 0)
                {
                    logDirectoryFileList = logDirectoryFileList.Where(s => s.Name.StartsWith(logFilePrefix) && s.Extension.ToLower() == logFileExtension).ToList();

                    if (logDirectoryFileList.Count > 0)
                    {
                        var logDirectoryFileInformationList = logDirectoryFileList.Select(s =>
                        {
                            string sequenceString = Path.GetFileNameWithoutExtension(s.Name).Replace(logFilePrefix, string.Empty);

                            int sequence;

                            int.TryParse(sequenceString, out sequence);

                            return new
                            {
                                LogFile = s,
                                LogFileSequence = sequence
                            };
                        }).ToList();

                        var logDirectoryFileInformation = logDirectoryFileInformationList.Where(s => s.LogFileSequence > 0).OrderByDescending(s => s.LogFileSequence).FirstOrDefault();

                        if (logDirectoryFileInformation != null)
                        {
                            string logFileFullPath = logDirectoryFileInformation.LogFile.FullName;

                            long logFileSize = FileHelper.GetFileSize(logFileFullPath);

                            logFileSequence = logDirectoryFileInformation.LogFileSequence;

                            if (logFileSize >= logFileLimitSize)
                            {
                                logFileSequence += 1;
                            }
                        }
                    }
                }

                string logFilePath = logDirectoryPath + @"\" + logFilePrefix + logFileSequence + logFileExtension;

                string logFullText = currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ------ " + logText.SafeLogMsg();

                if (File.Exists(logFilePath))
                {
                    using (StreamWriter sw = File.AppendText(logFilePath))
                    {
                        sw.Write(Environment.NewLine + Environment.NewLine + logFullText);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(logFilePath))
                    {
                        sw.Write(logFullText);
                    }
                }
            }
        }

        /// <summary>
        /// 日志信息安全校验
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string SafeLogMsg(this string msg)
        {
            return msg.Replace('\n', '_').Replace('\r', '_').Replace('\t', '_').Replace("{", "{{").Replace("}", "}}");
        }
        /// <summary>
        /// 日志信息安全校验
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string SafeLogMsg(this object msg)
        {
            return msg?.ToString().SafeLogMsg();
        }
        /// <summary>
        /// 日志信息安全校验
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static object[] SafeLogMsg(this object[] msgs)
        {
            var list = new List<object>();
            msgs.ForEach(item => { list.Add(item.SafeLogMsg()); });
            return list.ToArray();
        }
    }
}