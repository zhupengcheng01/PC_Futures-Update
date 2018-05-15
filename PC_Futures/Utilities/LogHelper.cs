using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace Utilities
{
    public sealed class LogHelper
    {
        /// <summary>
        /// 声明私有的构造函数
        /// </summary>
        private LogHelper()
        {

        }
        //用于记录信息的log
        private static ILog _log;

        /// <summary>
        /// 用于Trace的log
        /// </summary>
        private static ILog Log
        {
            get
            {

                if (_log == null)
                {
                    log4net.Config.XmlConfigurator.Configure();
                    _log = LogManager.GetLogger("logerror");
                }

                return _log;
            }
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg">错误消息</param>
        /// <param name="ex">Exception</param>
        public static void Error(String msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static void Error(String msg)
        {
            Log.Error(msg);
        }

        /// <summary>
        /// 记录一般信息
        /// </summary>
        /// <param name="msg">一般信息</param>
        public static void Info(String msg)
        {
            Log.Info(msg);
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg">调试信息</param>
        public static void Debug(String msg)
        {
            Log.Debug(msg);
        }


        /// <summary>
        /// Log开始
        /// </summary>
        /// <param name="method">方法名</param> 
        public static void beginMethod(MethodBase method)
        {
            Debug(method.DeclaringType + "." + method.Name + "开始");
        }

        /// <summary>
        /// Log结束
        /// </summary>
        /// <param name="method">方法名</param> 
        public static void endMethod(MethodBase method)
        {
            Debug(method.DeclaringType + "." + method.Name + "结束");
        }

        /// <summary>
        /// Log执行 数据库操作
        /// </summary>
        /// <param name="method">方法名</param> 
        public static void BLLMethod(MethodBase method)
        {
            Debug("执行数据库操作" + method.DeclaringType + "." + method.Name);
        }
    }
}
