using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PC_Futures.WebScoket
{
    public class WebScoketHelper
    {
        private static WebScoketHelper _instance;
        public static WebScoketHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WebScoketHelper();
            }
            return _instance;

        }
        /// <summary>
        /// 带一个string参数委托
        /// </summary>
        /// <param name="Mesg"></param>
        /// <returns></returns>
        public delegate void EventStringHandler(string Msg);

        /// <summary>
        /// 写日志的事件
        /// </summary>
        public  event EventStringHandler MesError;

        /// <summary>
        /// 交易服务器当前状态的事件
        /// </summary>
        public  event EventStringHandler TradeServerServerStatus;

        /// <summary>
        /// 行情服务器当前状态的事件
        /// </summary>
        public  event EventStringHandler QuotesServerStatus;

        /// <summary>
        /// 行情异常弹框提示
        /// </summary>
        public event EventStringHandler MessageBoxQuotesShow;
        /// <summary>
        /// 交易异常弹框提示
        /// </summary>
        public event EventStringHandler MessageBoxTradeShow;

        /// <summary>
        /// bool类型的参数
        /// </summary>
        /// <param name="isTrue"></param>
        /// <returns></returns>
        public delegate void EventBoolHandler(bool isTrue);

        /// <summary>
        /// 交易服务器是否重连的事件
        /// </summary>
        public  event EventBoolHandler TradeServerServerReattached;

        /// <summary>
        /// 行情服务器是否重连事件
        /// </summary>
        public  event EventBoolHandler QuotesServerReattached;

        /// <summary>
        /// 获取新的行情
        /// </summary>
        /// <param name="isTrue"></param>
        /// <returns></returns>
        public delegate void EventRqtHandler(ResultQuotationEntity rqt);

        /// <summary>
        /// 行情更新的事件
        /// </summary>
        public  event EventRqtHandler QuotesModelUpdate;

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public  void LogMsg(string msg)
        {

            MesError?.Invoke(msg);
        }
        /// <summary>
        /// 获取交易服务器状态
        /// </summary>
        /// <param name="msg"></param>
        public  void GetTradeServerState(string msg)
        {

            TradeServerServerStatus?.Invoke(msg);
        }

        /// <summary>
        /// 获取行情服务器状态
        /// </summary>
        /// <param name="msg"></param>
        public  void GetQuotesServerState(string msg)
        {

            QuotesServerStatus?.Invoke(msg);
        }

        /// <summary>
        /// 行情异常弹框提示
        /// </summary>
        /// <param name="msg"></param>
        public void GetMessageBoxQuotesShow(string msg)
        {

            MessageBoxQuotesShow?.Invoke(msg);
        }
        /// <summary>
        /// 行情异常弹框提示
        /// </summary>
        /// <param name="msg"></param>
        public void GetMessageBoxTradeShow(string msg)
        {

            MessageBoxTradeShow?.Invoke(msg);
        }


        /// <summary>
        /// 获取行情服务器是否重连
        /// </summary>
        /// <param name="msg"></param>
        public void TradeIsReattached(bool isTrue)
        {

            TradeServerServerReattached?.Invoke(isTrue);
        }

        /// <summary>
        /// 获取行情服务器是否重连
        /// </summary>
        /// <param name="msg"></param>
        public  void QuotesIsReattached(bool isTrue)
        {

            QuotesServerReattached?.Invoke(isTrue);
        }

        /// <summary>
        /// 获取行情服务器是否重连
        /// </summary>
        /// <param name="msg"></param>
        public  void QuotesUpdate(ResultQuotationEntity rqt)
        {

            QuotesModelUpdate?.Invoke(rqt);
        }

        /// <summary>
        /// 接受数据的反射到类的方法
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="Mothed"></param>
        /// <param name="param"></param>
        public static void CreateAaccpMothed(string classname, string Mothed, object param)
        {
            Assembly assembly = Assembly.Load("PC_Futures");
            Type type = assembly.GetType("PC_Futures.ViewModel." + classname);
            object instance = assembly.CreateInstance("PC_Futures.ViewModel." + classname);
            Object[] params_obj = new Object[1];
            params_obj[0] = param;
            object value = type.GetMethod(Mothed).Invoke(instance, params_obj);
        }

    }
}
