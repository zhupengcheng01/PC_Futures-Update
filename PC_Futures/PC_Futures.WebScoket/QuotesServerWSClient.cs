using System;
using System.Threading;
using WebSocketSharp;
using System.Timers;
using Newtonsoft.Json;
using PC_Futures.Models;

namespace PC_Futures.WebScoket
{
    public class QuotesServerWSClient
    {
     
        public string socketUrl { get; set; }
        public WebSocket webSocket;
        private System.Timers.Timer sendTimer;
        private System.Timers.Timer receiveTimer;
        private int connectionTimes = 0;
        private int receiveTimes = 0;
        private int errorTimes = 0;
        private bool _isConnection = false;
        private object obj = new object();
        WebScoketHelper _WebScoketHelper = WebScoketHelper.GetInstance();

        public QuotesServerWSClient()
        {
           
            sendTimer = new System.Timers.Timer(5000);
            sendTimer.Elapsed += sendTimer_Elapsed;

            receiveTimer = new System.Timers.Timer(15000);
            receiveTimer.Elapsed += receiveTimer_Elapsed;
        }

        public bool startServer(bool IsFirstConnection)
        {
            try
            {
                if (webSocket != null && (webSocket.ReadyState == WebSocketState.Connecting || webSocket.ReadyState == WebSocketState.Open))
                {
                    webSocket.Close();
                }
                webSocket = new WebSocket(socketUrl);

                webSocket.OnOpen += (sender, e) => onOpen(sender, e);
                webSocket.OnMessage += (sender, e) => onMessage(sender, e);
                webSocket.OnError += (sender, e) => onError(sender, e);
                //去掉OnClose，因为如果服务器一直重连不上，经过多次连接之后，会报堆栈溢出，是由于陷入死循环了
                //webSocket.OnClose += (sender, e) => OnClose(sender, e); 


                webSocket.Connect();
                if (IsFirstConnection && webSocket.ReadyState != WebSocketState.Connecting && webSocket.ReadyState != WebSocketState.Open)
                {
                    return false;
                }
                if (sendTimer != null)
                {
                    sendTimer.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                _WebScoketHelper.LogMsg("QuotesServerWSClient连接异常:" + ex.ToString());
                _WebScoketHelper.GetMessageBoxQuotesShow("交易服务连接异常！");
                return false;
            }
        }

        public void onOpen(object sender, object e)
        {
            _WebScoketHelper.GetQuotesServerState("已连接");
            connectionTimes++;
            if (connectionTimes > 1)
            {
                _WebScoketHelper.QuotesIsReattached(true);
            }
        }

        public void onError(object sender, object e)
        {
            _WebScoketHelper.LogMsg(string.Format("onError:{0}", e.ToString()));
            _WebScoketHelper.GetQuotesServerState("已报错");
            if (sendTimer != null)
            {
                sendTimer.Stop();
            }
            destroy();
            Thread.Sleep(5000);
            startServer(false);
        }

        private void sendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sendTimer != null)
            {
                sendTimer.Stop();
            }
            _isConnection = false;
            string msg = "ping";
            _WebScoketHelper.LogMsg("发送心跳");
            sendMessage(msg);
            if (receiveTimer != null)
            {
                receiveTimer.Start();
            }
        }
        private void receiveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (receiveTimer != null)
            {
                receiveTimer.Stop();
            }
            if (!_isConnection)
            {
                _WebScoketHelper.LogMsg("行情心跳接受不到数据，然后重连");
                _WebScoketHelper.GetQuotesServerState("已断开");
                destroy();
                Thread.Sleep(5000);
                startServer(false);
                return;
            }
            if (sendTimer != null)
            {
                sendTimer.Start();
            }
        }
        public void sendMessage(String msg)
        {
            if (webSocket != null && webSocket.ReadyState == WebSocketState.Open)
            {
                webSocket.Send(msg);
                _WebScoketHelper.LogMsg(string.Format("发送消息:{0}", msg));
            }
        }
        public void onMessage(object sender, MessageEventArgs e)
        {
            try
            {
                _isConnection = true;
                if (e.Data == "pong")
                {
                    _WebScoketHelper.LogMsg("接收心跳");
                    return;
                }
                _WebScoketHelper.LogMsg("行情接收");
                ResultQuotationEntity resultResponseModel = JsonConvert.DeserializeObject<ResultQuotationEntity>(e.Data);
                if (resultResponseModel != null)
                {
                    _WebScoketHelper.QuotesUpdate(resultResponseModel);
                }
            }
            catch (Exception ex)
            {
                _WebScoketHelper.LogMsg(string.Format("行情推送的错误信息:{0}", e.Data));
                _WebScoketHelper.LogMsg(string.Format("Error:{0}", ex));
            }
        }
        public void destroy()
        {
            if (webSocket != null && (webSocket.ReadyState == WebSocketState.Connecting || webSocket.ReadyState == WebSocketState.Open))
            {
                webSocket.Close();
                webSocket = null;
            }
        }
        //public void UpdateQutesInfo(QuotationEntity responseModel)
        //{
        //    if (responseModel == null || string.IsNullOrEmpty(responseModel.ed))
        //        return;
        //    //  WebScoketHelper.LogMsg("行情合约:"+responseModel.cd);
        //    lock (TradeInfoHelper.SubscribedContractList)
        //    {
        //        var model = TradeInfoHelper.SubscribedContractList.FirstOrDefault(o => string.Equals(o.cd, responseModel.cd));
        //        if (model != null)
        //        {
        //            TradeInfoHelper.SubscribedContractList.Remove(model);
        //        }
        //        TradeInfoHelper.SubscribedContractList.Add(responseModel);
        //    }
        //    if (UserInfoHelper.IsHaveLogin)
        //    {
        //        #region 更新持仓

        //        _positionAllViewModel = PositionAllViewModel.Instance();
        //        _positionAllViewModel.SendMarket(responseModel);
        //        #endregion

        //        #region 更新自选
        //        var tradeQuotesViewModel = TradeQuotesViewModel.GetInstance(null);
        //        var optionalmodel = tradeQuotesViewModel.OptionalList.FirstOrDefault(o => string.Equals(o.ContractCode, responseModel.cd));
        //        UpdateModelInfo(optionalmodel, responseModel);
        //        #endregion

        //        #region 更新下单面板
        //        FuturesViewModel selectModel = MainViewModel.GetInstance().SelectItemViewModel;
        //        if (selectModel != null && string.Equals(selectModel.ContractCode, responseModel.cd))
        //        {
        //            TransactionViewModel.Instance().SendMarket(responseModel);
        //        }
        //        #endregion
        //    }

        //    #region 更新行情列表
        //    if (_quotesViewModel == null)
        //    {
        //        _quotesViewModel = QuotesTabControlViewModel.GetInstance(null);
        //    }
        //    if (TradeInfoHelper.ContractModelGroupList.ContainsKey(responseModel.ed))
        //    {
        //        var hkexmodel = TradeInfoHelper.ContractModelGroupList[responseModel.ed].FirstOrDefault(o => string.Equals(o.ContractCode, responseModel.cd));
        //        UpdateModelInfo(hkexmodel, responseModel);
        //    }
        //    #endregion

        //}
        //public void UpdateModelInfo(FuturesViewModel futuresViewModel, QuotationEntity responseModel)
        //{
        //    if (futuresViewModel == null)
        //        return;
        //    futuresViewModel.Tick.Precy = responseModel.py ?? 0;
        //    futuresViewModel.Tick.BidP1 = responseModel.bp1;
        //    futuresViewModel.Tick.BidP2 = responseModel.bp2;
        //    futuresViewModel.Tick.BidP3 = responseModel.bp3;
        //    futuresViewModel.Tick.BidP4 = responseModel.bp4;
        //    futuresViewModel.Tick.BidP5 = responseModel.bp5;
        //    futuresViewModel.Tick.AskP1 = responseModel.ap1;
        //    futuresViewModel.Tick.AskP2 = responseModel.ap2;
        //    futuresViewModel.Tick.AskP3 = responseModel.ap3;
        //    futuresViewModel.Tick.AskP4 = responseModel.ap4;
        //    futuresViewModel.Tick.AskP5 = responseModel.ap5;
        //    futuresViewModel.Tick.BidV1 = responseModel.bs1;
        //    futuresViewModel.Tick.BidV2 = responseModel.bs2;
        //    futuresViewModel.Tick.BidV3 = responseModel.bs3;
        //    futuresViewModel.Tick.BidV4 = responseModel.bs4;
        //    futuresViewModel.Tick.BidV5 = responseModel.bs5;
        //    futuresViewModel.Tick.AskV1 = responseModel.as1;
        //    futuresViewModel.Tick.AskV2 = responseModel.as2;
        //    futuresViewModel.Tick.AskV3 = responseModel.as3;
        //    futuresViewModel.Tick.AskV4 = responseModel.as4;
        //    futuresViewModel.Tick.AskV5 = responseModel.as5;
        //    futuresViewModel.Tick.TotalSize = responseModel.ts;
        //    futuresViewModel.Tick.PreClosePrice = responseModel.pclp;
        //    futuresViewModel.Tick.PreSettlePrice = responseModel.pslp;
        //    futuresViewModel.Tick.OpenPrice = responseModel.op;
        //    futuresViewModel.Tick.HighPrice = responseModel.hp;
        //    futuresViewModel.Tick.LowPrice = responseModel.lop;
        //    futuresViewModel.Tick.Time = responseModel.time;
        //    futuresViewModel.Tick.QuotesTime = string.IsNullOrEmpty(responseModel.time) ? "" : Convert.ToDateTime(responseModel.time).ToString("HH:mm:ss");
        //    //futuresViewModel.Tick.QuotesTime = string.IsNullOrEmpty(responseModel.time) ? "" : responseModel.time.Substring(11);

        //    futuresViewModel.Tick.LastDownPrice = responseModel.ldp;
        //    futuresViewModel.Tick.LastUpPrice = responseModel.lup;
        //    futuresViewModel.Tick.PositionSize = responseModel.tr;
        //    futuresViewModel.Tick.LastSize = responseModel.ls;
        //    futuresViewModel.Tick.LastPrice = responseModel.lp;


        //    #region 更新逐笔成交
        //    //if (responseModel.mk == null || futuresViewModel.Tick.EachDealList.Count < TradeInfoHelper.EachDealCount)
        //    //    return;
        //    //var lastmodel = futuresViewModel.Tick.EachDealList.LastOrDefault();
        //    //DateTime thisTime= string.IsNullOrEmpty(responseModel.time) ? new DateTime() : Convert.ToDateTime(responseModel.time);
        //    //if (DateTime.Compare(lastmodel.OrderByTime, thisTime) >= 0)
        //    //    return;
        //    //if (Application.Current != null) //判断不为空
        //    //{
        //    //    Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(
        //    //    () =>
        //    //    {
        //    //        futuresViewModel.Tick.EachDealList.RemoveAt(0);
        //    //        futuresViewModel.Tick.EachDealList.Add(new EachDealViewModel(futuresViewModel.Tick));
        //    //    }));
        //    //}


        //    #endregion

        //}
    }
}