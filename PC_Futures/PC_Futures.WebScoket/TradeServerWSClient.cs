using System;
using System.Linq;
using System.Threading;
using WebSocketSharp;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PC_Futures.Models;


namespace PC_Futures.WebScoket
{
    public class TradeServerWSClient
    {
        public string socketUrl { get; set; }
        public WebSocket webSocket;
        private System.Timers.Timer sendTimer;
        private System.Timers.Timer receiveTimer;   
        private bool _isConnection = false;
        private object obj = new object();
        WebScoketHelper _WebScoketHelper = WebScoketHelper.GetInstance();     
        private static TradeServerWSClient _instance;
        public static TradeServerWSClient Instance()
        {
            if (_instance == null)
            {
                _instance = new TradeServerWSClient();
            }
            return _instance;
        }
        public TradeServerWSClient()
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
                _WebScoketHelper.GetTradeServerState("连接中");
                if (sendTimer != null)
                {
                    sendTimer.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                _WebScoketHelper.LogMsg("TradeServerWSClient连接异常:" + ex.ToString());
                //MessageBox.Show("交易服务连接异常！");
                return false;
            }
        }

        public void onOpen(object sender, object e)
        {
            _WebScoketHelper.GetTradeServerState("已连接");
        }

        public void OnClose(object sender, object e)
        {

        }

        public void onError(object sender, object e)
        {
            _WebScoketHelper.GetTradeServerState("已报错");
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
                _WebScoketHelper.LogMsg("交易心跳接受不到数据，然后重连");
                _WebScoketHelper.GetTradeServerState("已断开");
                destroy();
                Thread.Sleep(5000);
                startServer(false);
                SendLoginInfo();
                return;
            }
            if (sendTimer != null)
            {
                sendTimer.Start();
            }
        }
        private void SendLoginInfo()
        {
            //string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string token = MD5Helper.EncryptWithMD5(time);
            //string msg = "<HX>{\"cmdcode\":2235,\"content\":{\"login_name\":\"" + UserInfoHelper.LoginName + "\",\"password\":\"" + UserInfoHelper.Password + "\",\"resource\":0,\"mac_address\":\"" + UserInfoHelper.MAC + "\",\"ip_address\":\"" + UserInfoHelper.IP + "\",\"time\":\"" + time + "\",\"token\":\"" + time + "\"}}<END>";
            //sendMessage(msg);
        }
        public void sendMessage(string msg)
        {
            if (webSocket != null && webSocket.ReadyState == WebSocketState.Open)
            {
                //webSocket.Send("§HX§"+msg+ "§END§");
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
                    return;
                string mesage = e.Data;
                mesage = mesage.TrimStart(new char[] { '<', 'H', 'X', '>' });
                mesage = mesage.TrimEnd(new char[] { '<', 'E', 'N', 'D', '>' });
                RecvData(mesage);

            }
            catch (Exception ex)
            {
                _WebScoketHelper.LogMsg(string.Format("行情推送的错误信息:{0}", e.Data));
                _WebScoketHelper.LogMsg(string.Format("Error:{0}", ex));
            }
        }
        /// <summary>
        /// 处理回传字符串
        /// </summary>
        private void RecvData(string message)
        {
            //  WebScoketHelper.LogMsg(message);
            JObject jo = (JObject)JsonConvert.DeserializeObject(message);
            if (jo == null) return;
            string code = jo["cmdcode"].ToString();
            if (!ConstMothed.SussCodes.Contains(code)) return;

            string classname = ConstMothed.ClassCode[code];
            //方法名
            string Mothed = ConstMothed.MothedCode[code];
            object param = null;
            switch (code)
            {
                case "2236"://交易员登录返回
                    param = JsonConvert.DeserializeObject<ResultTradeLoginModel>(message);
                    break;
                case "2074"://所有业务请求返回  
                    //ResultBussnessModel rbm = JsonHelper.stringToObjectBussness(message);
                    //param = rbm.content;
                    break;
                case "2068"://返回角色协议
                    //ResultUserPermissionModel rum = JsonConvert.DeserializeObject<ResultUserPermissionModel>(message);
                    //param = rum.content;
                    break;
                case "4002"://请求委托                       
                    RestDelegationModel rum = JsonConvert.DeserializeObject<RestDelegationModel>(message);
                    if (rum.errcode == 0)
                    {
                        param = rum.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rum.errMsg);
                        return;
                    }
                    break;
                case "4003"://成交返回委托状态的改变

                    RestDelegationModel rum1 = JsonConvert.DeserializeObject<RestDelegationModel>(message);
                    if (rum1.errcode == 0)
                    {
                        param = rum1.content;
                    }
                    else
                    {
                        //MessageBox.Show(rum1.errMsg, "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.ServiceNotification);
                        code = "40031";
                        param = rum1.content;
                        classname = ConstMothed.ClassCode[code];
                        Mothed = ConstMothed.MothedCode[code];
                    }
                    break;
                case "4006"://成交
                    RestTransactionInfoModel rtim = JsonConvert.DeserializeObject<RestTransactionInfoModel>(message);
                    if (rtim.errcode == 0)
                    {
                        param = rtim.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rtim.errMsg);
                        return;
                    }
                    break;
                case "4007"://持仓
                    RestPostion rpin = JsonConvert.DeserializeObject<RestPostion>(message);
                    if (rpin.errcode == 0)
                    {
                        param = rpin.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rpin.errMsg);
                        return;

                    }
                    break;
                case "4008"://资金推送
                    param = JsonConvert.DeserializeObject<RestTodayFundsModel>(message);
                    break;
                case "4005"://撤单返回
                    ResultCannelOrderModel rcom = JsonConvert.DeserializeObject<ResultCannelOrderModel>(message);
                    if (rcom.errcode == 0)
                    {
                        param = rcom.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcom.errMsg);
                        return;
                    }
                    break;
                case "5002"://条件单查询
                    RestConditionBillModel rcbm = JsonConvert.DeserializeObject<RestConditionBillModel>(message);
                    if (rcbm.errcode == 0)
                    {
                        param = rcbm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcbm.errMsg);
                        return;
                    }
                    break;
                case "5004"://条件单增加
                    RestConditionBillModel rcbm1 = JsonConvert.DeserializeObject<RestConditionBillModel>(message);
                    if (rcbm1.errcode == 0)
                    {
                        param = rcbm1.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcbm1.errMsg);
                        return;
                    }
                    break;
                case "5006"://条件单修改
                    RestConditionBillModel rcbm2 = JsonConvert.DeserializeObject<RestConditionBillModel>(message);
                    if (rcbm2.errcode == 0)
                    {
                        param = rcbm2.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcbm2.errMsg);
                        return;
                    }
                    break;
                case "5008"://条件单删除
                    DeleteCondtionModel rcbm3 = JsonConvert.DeserializeObject<DeleteCondtionModel>(message);
                    if (rcbm3.errcode == 0)
                    {
                        param = rcbm3.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcbm3.errMsg);
                        return;
                    }
                    break;
                case "5018"://止盈止损修改推送
                    ListCheckFullStopModel lcfull = JsonConvert.DeserializeObject<ListCheckFullStopModel>(message);
                    if (lcfull.errcode == 0)
                    {
                        param = lcfull.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(lcfull.errMsg);
                        return;
                    }
                    break;
                case "5023"://条件单删除
                    RestConditionBillModel rcbm4 = JsonConvert.DeserializeObject<RestConditionBillModel>(message);
                    if (rcbm4.errcode == 0)
                    {
                        param = rcbm4.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcbm4.errMsg);
                        return;
                    }
                    break;
                case "5022"://止盈止损单删除
                    StopLossStatusList sls1 = JsonConvert.DeserializeObject<StopLossStatusList>(message);
                    if (sls1.errcode == 0)
                    {
                        param = sls1.content;

                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(sls1.errMsg);
                        return;
                    }
                    break;
                case "5020"://止盈止损损
                    RestCheckFullStopModel sls2 = JsonConvert.DeserializeObject<RestCheckFullStopModel>(message);
                    if (sls2.errcode == 0)
                    {
                        param = sls2.content;

                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(sls2.errMsg);
                        return;
                    }
                    break;
                case "5024"://止盈止损单删除
                    StopLossStatus sls = JsonConvert.DeserializeObject<StopLossStatus>(message);
                    if (sls.errcode == 0)
                    {
                        param = sls.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(sls.errMsg);
                        return;
                    }
                    break;
                case "2210"://持仓总汇
                    RestPostion rp = JsonConvert.DeserializeObject<RestPostion>(message);
                    if (rp.errcode == 0)
                    {
                        param = rp.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rp.errMsg);
                        return;

                    }

                    break;
                case "2212"://持仓明细
                    RestPotionDetailModel rpdm = JsonConvert.DeserializeObject<RestPotionDetailModel>(message);
                    if (rpdm.errcode == 0)
                    {
                        param = rpdm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rpdm.errMsg);
                        return;
                    }
                    break;
                case "2214"://委托
                    RestDelegationModel rd = JsonConvert.DeserializeObject<RestDelegationModel>(message);

                    if (rd.errcode == 0)
                    {
                        param = rd.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rd.errMsg);
                        return;
                    }
                    break;
                case "2218"://当日成交
                    RestTodayTraderModel rttm = JsonConvert.DeserializeObject<RestTodayTraderModel>(message);
                    if (rttm.errcode == 0)
                    {
                        param = rttm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rttm.errMsg);
                        return;

                    }
                    break;
                case "5010"://止盈止损查询
                    RestCheckFullStopModel rcfsm = JsonConvert.DeserializeObject<RestCheckFullStopModel>(message);
                    if (rcfsm.errcode == 0)
                    {
                        param = rcfsm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcfsm.errMsg);
                        return;

                    }
                    break;
                case "5012"://止盈止损增加
                    RestCheckFullStopModel rcfsm1 = JsonConvert.DeserializeObject<RestCheckFullStopModel>(message);
                    if (rcfsm1.errcode == 0)
                    {
                        param = rcfsm1.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcfsm1.errMsg);
                        return;
                    }
                    break;
                case "5014"://止盈止损修改
                    RestCheckFullStopModel rcfsm2 = JsonConvert.DeserializeObject<RestCheckFullStopModel>(message);
                    if (rcfsm2.errcode == 0)
                    {
                        param = rcfsm2.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcfsm2.errMsg);
                        return;
                    }
                    break;
                case "5016"://止盈止损删除
                    StopLossStatus rcfsm3 = JsonConvert.DeserializeObject<StopLossStatus>(message);
                    if (rcfsm3.errcode == 0)
                    {
                        param = rcfsm3.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rcfsm3.errMsg);
                        return;
                    }
                    break;
                case "2241"://合约的品种
                    RestVarietyModel rvm = JsonConvert.DeserializeObject<RestVarietyModel>(message);
                    if (rvm.errcode == 0)
                    {
                        param = rvm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rvm.errMsg);
                        return;
                    }
                    break;
                case "2192"://合约的品种
                    ResultParitiesModel rpm = JsonConvert.DeserializeObject<ResultParitiesModel>(message);
                    if (rpm.errcode == 0)
                    {
                        param = rpm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rpm.errMsg);
                        return;
                    }
                    break;
                case "2268"://资金查询
                    param = JsonConvert.DeserializeObject<RestTodayFundsModel>(message);
                    break;
                case "2245"://自选合约
                    param = JsonConvert.DeserializeObject<ResultOptionalContractModel>(message);
                    break;
                case "2247"://新增自选合约
                    param = JsonConvert.DeserializeObject<ResultOptionalContractModel>(message);
                    break;
                case "2249"://删除自选合约
                    param = JsonConvert.DeserializeObject<ResultOptionalContractModel>(message);
                    break;
                case "2208"://修改密码
                    param = JsonConvert.DeserializeObject<ResultModifyPwdModel>(message);
                    break;
                case "2226"://结算单查询
                    RestDescriptModel rdm = JsonConvert.DeserializeObject<RestDescriptModel>(message);
                    if (rdm.errcode == 0)
                    {
                        param = rdm.content;
                    }
                    else
                    {
                        _WebScoketHelper.GetMessageBoxTradeShow(rdm.errMsg);
                        return;
                    }
                    break;
                case "2198"://收费方式
                    RestCalcDepositModel rcdm = JsonConvert.DeserializeObject<RestCalcDepositModel>(message);
                    if (rcdm.errcode == 0)
                    {
                        param = rcdm.content;
                    }
                    else
                    {
                        _WebScoketHelper.LogMsg(rcdm.errMsg);
                        return;
                    }
                    break;
                case "2261"://收费方式
                    RestMarginModel rmm = JsonConvert.DeserializeObject<RestMarginModel>(message);
                    if (rmm.errcode == 0)
                    {
                        param = rmm.content;
                    }
                    else
                    {
                        _WebScoketHelper.LogMsg(rmm.errMsg);
                        return;
                    }
                    break;
                case "2262"://收费方式
                    SendMarginModel smm = JsonConvert.DeserializeObject<SendMarginModel>(message);
                    if (smm.errcode == 0)
                    {
                        param = smm.content;
                    }
                    else
                    {
                        _WebScoketHelper.LogMsg(smm.errMsg);
                        return;
                    }
                    break;
                default:
                    break;
            }
            WebScoketHelper.CreateAaccpMothed(classname, Mothed, param);
        }
        public void destroy()
        {
            if (webSocket != null && (webSocket.ReadyState == WebSocketState.Connecting || webSocket.ReadyState == WebSocketState.Open))
            {
                webSocket.Close();
                webSocket = null;
            }
        }

        public void StopTimer()
        {
            if (sendTimer != null)
            {
                sendTimer.Stop();
            }
            if (receiveTimer != null)
            {
                receiveTimer.Stop();
            }

        }
    }
}