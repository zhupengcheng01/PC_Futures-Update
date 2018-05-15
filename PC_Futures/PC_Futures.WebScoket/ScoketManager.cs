using PC_Futures.Models;
using System;
using System.Collections.Generic;

namespace PC_Futures.WebScoket
{
    public class ScoketManager
    {
        private TradeServerWSClient tradeWS;
        private QuotesServerWSClient quotesWS;
        private static ScoketManager _instance;
       public SiteTemp _titetemp = null;
        WebScoketHelper _WebScoketHelper = WebScoketHelper.GetInstance();
        public static ScoketManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ScoketManager();
            }
            return _instance;
        }
        public ScoketManager()
        {
            tradeWS = TradeServerWSClient.Instance();
        }



        public bool StartTradeSocket(bool IsFirstConnection)
        {
            if (_titetemp == null) return true;
            if (tradeWS == null)
            {
                ConstMothed.Init();
                tradeWS = new TradeServerWSClient();
                tradeWS.socketUrl = "ws://" + _titetemp.IP + ":" + _titetemp.IPPort;
                return tradeWS.startServer(IsFirstConnection);
            }
            return true;
        }
        public void StopTradeTimer()
        {
            if (tradeWS != null)
            {
                tradeWS.StopTimer();
            }
        }

        public void Dispose()
        {
            if (tradeWS != null)
            {
                tradeWS.destroy();
                ConstMothed.ClearData();
                tradeWS = null;
            }

        }

        public bool StartQuotesSocket(string socketUrl)
        {
            quotesWS = new QuotesServerWSClient();
            quotesWS.socketUrl = socketUrl;
            return quotesWS.startServer(true);
         
        }
        /// <summary>
        /// bll调用
        /// </summary>
        /// <param name="mess"></param>
        public void SendTradeWSInfo(string mess)
        {
            try
            {
                string sendMsg = "<HX>" + mess + "<END>";
                tradeWS.sendMessage(sendMsg);
            }
            catch (Exception ex)
            {
                _WebScoketHelper.LogMsg(string.Format("发送命令错误:{0}", ex.ToString()));
            }
        }

        //public void UpdateModelInfo(FuturesViewModel futuresViewModel, QuotationEntity responseModel)
        //{
        //    quotesWS.UpdateModelInfo(futuresViewModel,responseModel);
        //}

        /// <summary>
        /// 发送单个订阅和反订阅
        /// </summary>
        /// <param name="type">7订阅 8反订阅</param>
        /// <param name="stockCode"></param>
        public void SendSubscribeInfo(int type, string stockCode)
        {
            //if (string.IsNullOrEmpty(stockCode))
            //    return;
            //RequestDataEntity rqde = new RequestDataEntity();
            //rqde.type = type.ToString();
            //rqde.data = stockCode;
            //string mess = JsonConvert.SerializeObject(rqde);
            ////LogHelper.Instance.Write("请求股票行情" + mess.ToString());
            //quotesWS.sendMessage(mess);
        }
        /// <summary>
        /// 发送集合订阅和反订阅
        /// </summary>
        /// <param name="type">7订阅 8反订阅</param>
        /// <param name="stockList"></param>
        public void SendSubscribeInfo(int type, List<string> stockList)
        {
            //if (stockList.Count == 0)
            //    return;
            //RequestDataEntity rqde = new RequestDataEntity();
            //string strstockTemp = "";
            //rqde.type = type.ToString();
            //for (int i = 0; i < stockList.Count; i++)
            //{
            //    if (string.IsNullOrEmpty(stockList[i]))
            //    {
            //        continue;
            //    }
            //    strstockTemp += stockList[i] + ",";
            //}
            //strstockTemp = strstockTemp.Substring(0, strstockTemp.Length - 1);
            //if (string.IsNullOrEmpty(strstockTemp))
            //    return;
            //rqde.data = strstockTemp;
            //string mess = JsonConvert.SerializeObject(rqde);
            ////LogHelper.Instance.Write("请求行情" + mess.ToString());
            //quotesWS.sendMessage(mess);
            //if(type==8)
            //{
            //    foreach (var item in stockList)
            //    {
            //        var futuresModel = QuotesTabControlViewModel.GetInstance(null).FuturesViewModelList.FirstOrDefault(o =>!string.IsNullOrEmpty(o.ExchangeDisplay) && string.Equals(o.ContractCode, item));
            //        if (futuresModel != null)
            //        {
            //            if (futuresModel.Tick.EachDealList.Count == 0)
            //                continue;
            //            if (Application.Current != null) //判断不为空
            //            {
            //                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(
            //                () =>
            //                {
            //                    futuresModel.Tick.EachDealList.Clear();
            //                }));
            //            }

            //        }
            //    }
            //}
        }
    }
}
