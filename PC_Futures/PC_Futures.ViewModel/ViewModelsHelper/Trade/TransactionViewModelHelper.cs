using Futures.Enum;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Utilities;
using Utilities.CommonClass;
using Utility;

namespace PC_Futures.ViewModel
{
    public class TransactionViewModelHelper
    {
        /// <summary>
        /// 请求返回的委托4002
        /// </summary>
        /// <param name="para"></param>
        public void RevcPlaceOrderData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecutePlaceOrderData, para);
        }
        public void ExecutePlaceOrderData(object para)
        {
            try
            {

                DelegationModel rtm = para as DelegationModel;
                //添加持仓集合              
                if (!rtm.bLast)
                {
                    VarietyModel vm = null;
                    string[] values = rtm.contract_id.Split(' ');
                    if (values.Length == 3)
                    {
                        string varietie = values[1];
                        if (ContractVariety.Varieties.ContainsKey(varietie))
                        {
                            vm = ContractVariety.Varieties[varietie];
                        }
                        if (vm != null)
                        {
                            rtm.precision = vm.precision;
                        }
                    }
                  
                }
                if (OrderCancelViewModel.Instance().Delegations.FirstOrDefault(x => x.OrderId == rtm.order_id) == null)
                {
                    OrderCancelViewModel.Instance().Delegations.Add(new DelegationModelViewModel(rtm));
                    LogHelper.Info("4002 增加" + rtm.contract_code + ":" + rtm.direction + ":" + rtm.open_offset + ":" + rtm.order_status);
                }

                if (CommParameterSetting.MessageAlert.OrderAlert == "窗口提示")
                {
                    MessageBox.Show(rtm.contract_id + " 委托创建成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (CommParameterSetting.MessageAlert.OrderAlert == "声音提示")
                {
                    SoundPlayerHelper.Play();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.ToString());
            }
        }

        /// <summary>
        /// 推送的委托4003
        /// </summary>
        /// <param name="para"></param>
        public void RevcSendPlaceOrderData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteSendPlaceOrderData, para);
        }
        public void ExecuteSendPlaceOrderData(object para)
        {
            try
            {
                DelegationModel rtm = para as DelegationModel;
                VarietyModel vm = null;
                string[] values = rtm.contract_code.Split(' ');
                if (values.Length == 3)
                {
                    string varietie = values[1];
                    if (ContractVariety.Varieties.ContainsKey(varietie))
                    {
                        vm = ContractVariety.Varieties[varietie];
                    }
                    if (vm != null)
                    {
                        rtm.precision = vm.precision;
                    }
                }

                DelegationModelViewModel dmvm = OrderCancelViewModel.Instance().Delegations.FirstOrDefault(x => x.OrderId == rtm.order_id);
                if (dmvm == null)
                {
                    return;
                    //OrderCancelViewModel.Instance().Delegations.Add(new DelegationModelViewModel(rtm));
                }
                //DelegationModelViewModel dmvmisHave = OrderCancelViewModel.Instance().Delegations.FirstOrDefault(x => x.OrderId == rtm.order_id && x.OrderStatus == (int)DeleteType.CreateSuccess);
                ////if (dmvmisHave != null)
                //{
                //    return;
                //    //OrderCancelViewModel.Instance().Delegations.Add(new DelegationModelViewModel(rtm));
                //}
                if (rtm.order_status == (int)DeleteType.CreateSuccess || rtm.order_status == (int)DeleteType.PortionTakeEffect)
                {
                    DelegationModelViewModel dmvm1 = OrderCancelViewModel.Instance().KCDelegations.FirstOrDefault(x => x.OrderId == rtm.order_id && rtm.user_id == UserInfoHelper.UserId);
                    if (dmvm1 == null)
                    {
                        OrderCancelViewModel.Instance().KCDelegations.Add(new DelegationModelViewModel(rtm));
                        LogHelper.Info("4003  增加 可撤" + rtm.contract_code + ":" + rtm.direction + ":" + rtm.open_offset + ":" + rtm.order_status);
                    }

                    dmvm.OrderStatus = rtm.order_status;
                    //详细状态
                    dmvm.FailMsg = rtm.fail_msg;
                }
                else
                {
                    dmvm.LeftVolume = dmvm.OrderVolume - rtm.trade_volume;
                    dmvm.TradeVolume = rtm.trade_volume;
                    dmvm.OrderStatus = rtm.order_status;
                    dmvm.LeftVolume = rtm.left_volume;

                    //详细状态
                    dmvm.FailMsg = rtm.fail_msg;
                    DelegationModelViewModel dmvm1 = OrderCancelViewModel.Instance().KCDelegations.FirstOrDefault(x => x.OrderId == rtm.order_id && rtm.user_id == UserInfoHelper.UserId);

                    if (dmvm1 != null)
                    {
                        OrderCancelViewModel.Instance().KCDelegations.Remove(dmvm1);
                        LogHelper.Info("4003  移除 可撤" + rtm.contract_code + ":" + rtm.direction + ":" + rtm.open_offset + ":" + rtm.order_status);
                    }
                }

                DelegationModelViewModel tem1 = OrderCancelViewModel.Instance().KCDelegations.FirstOrDefault(x => x.OrderId == rtm.order_id && rtm.user_id == UserInfoHelper.UserId);
                if (tem1 != null)
                {
                    tem1.LeftVolume = tem1.OrderVolume - rtm.trade_volume;
                    tem1.TradeVolume = rtm.trade_volume;
                    tem1.OrderStatus = rtm.order_status;
                    //详细状态
                    dmvm.FailMsg = rtm.fail_msg;
                }
                if (rtm.order_status == (int)DeleteType.UnTakeEffecUserCannel)
                {
                    if (CommParameterSetting.MessageAlert.RevokeAlert == "窗口提示")
                    {
                        MessageBox.Show(rtm.contract_id + " 委托撤单成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (CommParameterSetting.MessageAlert.RevokeAlert == "声音提示")
                    {
                        SoundPlayerHelper.Play();
                    }
                }
                TransactionViewModel.Instance().SelectNumExecuteChanged();
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }
        /// <summary>
        /// 推送的委托4003
        /// </summary>
        /// <param name="para"></param>
        public void RevcSendPlaceOrderData1(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteSendPlaceOrderData1, para);
        }
        public void ExecuteSendPlaceOrderData1(object para)
        {
            try
            {
                DelegationModel rtm = para as DelegationModel;

                DelegationModelViewModel dmvm = OrderCancelViewModel.Instance().Delegations.FirstOrDefault(x => x.OrderId == rtm.order_id && rtm.user_id == UserInfoHelper.UserId);
                if (dmvm == null)
                {
                    OrderCancelViewModel.Instance().Delegations.Add(new DelegationModelViewModel(rtm));
                }
                else
                {
                    dmvm.OrderStatus = rtm.order_status;
                    //详细状态
                    dmvm.FailMsg = rtm.fail_msg;
                }
                DelegationModelViewModel dmvm1 = OrderCancelViewModel.Instance().KCDelegations.FirstOrDefault(x => x.OrderId == rtm.order_id && rtm.user_id == UserInfoHelper.UserId);
                if (dmvm1 != null)
                {

                    OrderCancelViewModel.Instance().KCDelegations.Remove(dmvm1);
                }

            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }
        /// <summary>
        /// 推送的成交信息4006
        /// </summary>
        /// <param name="para"></param>
        public void RevcTransactionInfoData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteTransactionInfoData, para);
        }
        public void ExecuteTransactionInfoData(object para)
        {
            try
            {

                TransactionInfoModel tim = para as TransactionInfoModel;
                if (tim == null) return;
                if (TodayTraderViewModels.Instance().TodayTraderList.FirstOrDefault(x => x.ShadowTradeID == tim.shadow_tradeID) != null) return;
                TodayTraderModel ttm = new TodayTraderModel();
                ttm.contract_code = tim.contract_code;
                ttm.direction = tim.direction;
                ttm.open_offset = tim.open_offset;
                ttm.order_id = tim.order_id;
                ttm.price_type = tim.price_type;
                ttm.shadow_orderID = tim.shadow_orderID;
                ttm.trade_id = tim.trade_number;
                ttm.contract_id = tim.contract_id;
                ttm.shadow_tradeID = tim.shadow_tradeID;
                ttm.trade_date = tim.trade_date;
                ttm.trade_id = tim.trade_id;
                ttm.trade_price = tim.trade_price;
                ttm.trade_time = tim.trade_time;
                ttm.trade_volume = tim.trade_volume;
                VarietyModel vm = null;
                string[] values = ttm.contract_code.Split(' ');
                if (values.Length == 3)
                {
                    string varietie = values[1];
                    if (ContractVariety.Varieties.ContainsKey(varietie))
                    {
                        vm = ContractVariety.Varieties[varietie];
                    }
                    if (vm != null)
                    {
                        ttm.precision = vm.precision;
                    }
                }
                TodayTraderViewModels.Instance().TodayTraderList.Add(new TodayTraderModelViewModel(ttm));
                Dictionary<string, TodayTraderModelViewModel> dicTTVM = new Dictionary<string, TodayTraderModelViewModel>();
                foreach (TodayTraderModelViewModel item in TodayTraderViewModels.Instance().TodayTraderList)
                {

                    if (!dicTTVM.ContainsKey(item.ContractCode + item.Direction + item.OpenOffset))
                    {
                        TodayTraderModelViewModel ttmvm = item.Clone(item);
                        ttmvm.AllPrice = item.TradePrice * item.TradeVolume;
                        dicTTVM.Add(item.ContractCode + item.Direction + item.OpenOffset, ttmvm);
                    }
                    else
                    {
                        dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].AllPrice = dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].AllPrice + item.TradePrice * item.TradeVolume;
                        dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].TradeVolume = dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].TradeVolume + item.TradeVolume;
                        dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].TradePrice = dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].AllPrice / dicTTVM[item.ContractCode + item.Direction + item.OpenOffset].TradeVolume;
                    }

                }
                if (dicTTVM != null && dicTTVM.Values.Count > 0)
                {
                    TodayTraderViewModels.Instance().TodayTraderListALL.Clear();

                    foreach (TodayTraderModelViewModel item in dicTTVM.Values)
                    {
                        TodayTraderViewModels.Instance().TodayTraderListALL.Add(item);
                    }
                }
                if (CommParameterSetting.MessageAlert.TradeAlert == "窗口提示")
                {
                    // string msg = "\t合约已成交\t" + "\n\n" + ttm.contract_id + "\n\t" + "方向:" + (ttm.direction == "B" ? "买" : "卖") + "\n\t开平:" + (ttm.open_offset == 1 ? "开仓" : "平仓") + "\n\t成交数量:" + ttm.trade_volume + "\n\t成交价格" + ttm.trade_price;
                    string msg = "     " + ttm.contract_id + "\t" + "    方向:" + (ttm.direction == "B" ? "买" : "卖") + "\t\n     开平:" + (ttm.open_offset == 1 ? "开仓" : "平仓") + "\t    成交数量:" + ttm.trade_volume + "\t\n     成交价格" + ttm.trade_price + "\t" + "    已成交";
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogHelper.Info(msg + "成交时间：");
                }
                else if (CommParameterSetting.MessageAlert.TradeAlert == "声音提示")
                {
                    SoundPlayerHelper.Play();
                }
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }

        /// <summary>
        /// 推送的持仓信息4007
        /// </summary>
        /// <param name="para"></param>
        public void RevcPositionInfoData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecutePositionInfoData, para);
        }
        public void ExecutePositionInfoData(object para)
        {
            try
            {
                PotionModel rtm = para as PotionModel;
                PositionAllViewModel pavm = PositionAllViewModel.Instance();
                PositionViewModel pvm = PositionViewModel.Instance();
                PotionDetailModelViewModel pdmvmtemp = pavm.DetPMList.FirstOrDefault(x => x.PsitionId == rtm.position_id);

                if (rtm.position_volume == 0)
                {

                    if (pdmvmtemp != null)
                    {
                        pavm.DetPMList.Remove(pdmvmtemp);
                        LogHelper.Info("4007  移除 " + rtm.contract_code + ":" + rtm.direction);
                        pavm.PMList.Clear();
                        pvm.PMList.Clear();
                    }

                    int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "delete from AutoStopLoss where ContractID='" + rtm.contract_id + "' and PostionID='" + rtm.position_id + "';");

                }
                else
                {

                    if (pdmvmtemp != null)
                    {
                        pdmvmtemp.PositionVolume = rtm.position_volume;
                        pdmvmtemp.AbleVolume = rtm.position_volume;
                        pdmvmtemp.UseMargin = rtm.use_margin;
                        pdmvmtemp.SysUseMargin = rtm.sys_margin;
                    }
                    else
                    {
                        PotionDetailModel pdm = new PotionDetailModel();
                        pdm.able_volume = rtm.position_volume;
                        pdm.contract_code = rtm.contract_code;
                        pdm.contract_id = rtm.contract_id;
                        pdm.direction = rtm.direction;
                        pdm.open_date = rtm.position_date;
                        pdm.open_price = rtm.position_price;
                        pdm.open_time = rtm.position_time;
                        pdm.position_volume = rtm.position_volume;
                        pdm.position_id = rtm.position_id;
                        pdm.shadow_orderID = rtm.shadow_orderID;
                        pdm.shadow_positionid = rtm.shadow_positionid;
                        pdm.shadow_tradeid = rtm.shadow_tradeid;

                        pdm.trade_id = "";
                        pdm.user_id = rtm.user_id;
                        pdm.use_margin = rtm.use_margin;
                        pdm.sys_margin = rtm.sys_margin;
                        VarietyModel vm = null;
                        string[] values = pdm.contract_id.Split(' ');
                        if (values.Length == 3)
                        {
                            string varietie = values[1];
                            if (ContractVariety.Varieties.ContainsKey(varietie))
                            {
                                vm = ContractVariety.Varieties[varietie];
                            }
                            if (vm != null)
                            {
                                pdm.precision = vm.precision;
                            }
                        }

                        pdmvmtemp = new PotionDetailModelViewModel(pdm);
                        pavm.DetPMList.Add(pdmvmtemp);

                    }
                }
                if (ContractVariety.ContracPostionID.ContainsKey(rtm.contract_id))
                {
                    AutoStopLossModel aslm = CommParameterSetting.AutoStopLossModel.FirstOrDefault(x => x.Direction == rtm.direction && x.Agreement == rtm.contract_id);
                    if (aslm != null)
                    {
                        if (!ContractVariety.PostionPrice.ContainsKey(rtm.position_id))
                        {
                            ContractVariety.PostionPrice.Add(rtm.position_id, new AutoLossPrice());
                        }
                        string[] VarietiesKey = rtm.contract_id.Split(' ');
                        if (VarietiesKey.Length == 3)
                        {
                            string varietie = VarietiesKey[1];
                            VarietyModel vm = null;
                            if (ContractVariety.Varieties.ContainsKey(varietie))
                            {
                                vm = ContractVariety.Varieties[varietie];
                            }
                            if (vm != null)
                            {
                                if (rtm.direction == "B")
                                    ContractVariety.PostionPrice[rtm.position_id].LossPrice = rtm.position_price - aslm.StopLossPotion * vm.tick_size;
                                else
                                    ContractVariety.PostionPrice[rtm.position_id].LossPrice = rtm.position_price + aslm.StopLossPotion * vm.tick_size;
                                ContractVariety.PostionPrice[rtm.position_id].NewPrice = rtm.position_price;
                                if (!ContractVariety.ContracPostionID.ContainsKey(rtm.contract_id))
                                {
                                    ContractVariety.ContracPostionID.Add(rtm.contract_id, new List<string>());
                                }
                                ContractVariety.ContracPostionID[rtm.contract_id].Add(rtm.position_id);
                                int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "insert INTO AutoStopLoss VALUES('" + rtm.position_id + "','" + rtm.contract_id + "','" + UserInfoHelper.UserId + "'," + ContractVariety.PostionPrice[rtm.position_id].LossPrice + "," + ContractVariety.PostionPrice[rtm.position_id].NewPrice + ");");

                            }
                        }
                    }
                }
                PotionDetailModelViewModel[] temp = pavm.DetPMList.ToArray();
                //合约名加方向作为K
                Dictionary<string, PotionDetailModelViewModel> dicod = new Dictionary<string, PotionDetailModelViewModel>();

                foreach (PotionDetailModelViewModel item in temp)
                {

                    PotionDetailModelViewModel pm1 = item.Clone(item);
                    if (!dicod.Keys.Contains(pm1.ContractCode + pm1.Direction))
                    {
                        pm1.AllPrice = pm1.OpenPrice * pm1.PositionVolume;
                        dicod.Add(pm1.ContractCode + pm1.Direction, pm1);
                        //MainViewModel.GetInstance().SubscribedStocks.Add(pm1.ContractCode);
                    }
                    else
                    {
                        dicod[item.ContractCode + item.Direction].AbleVolume = dicod[item.ContractCode + item.Direction].AbleVolume + item.AbleVolume;
                        dicod[item.ContractCode + item.Direction].PositionProfitLoss = dicod[item.ContractCode + item.Direction].PositionProfitLoss + item.PositionProfitLoss;
                        dicod[item.ContractCode + item.Direction].PositionVolume = dicod[item.ContractCode + item.Direction].PositionVolume + item.PositionVolume;
                        dicod[item.ContractCode + item.Direction].UseMargin = dicod[item.ContractCode + item.Direction].UseMargin + item.UseMargin;
                        dicod[item.ContractCode + item.Direction].SysUseMargin = dicod[item.ContractCode + item.Direction].SysUseMargin + item.SysUseMargin;
                        dicod[item.ContractCode + item.Direction].AllPrice += item.PositionVolume * item.OpenPrice;
                        dicod[item.ContractCode + item.Direction].OpenPrice = dicod[item.ContractCode + item.Direction].AllPrice / dicod[item.ContractCode + item.Direction].PositionVolume;
                    }

                }
                if (dicod.Keys.Count > 0)
                {
                    pavm.PMList.Clear();
                    pvm.PMList.Clear();
                    foreach (PotionDetailModelViewModel item in dicod.Values)
                    {
                        pavm.PMList.Add(item);
                        pvm.PMList.Add(item);
                    }
                    pvm.SelectedItem = pvm.PMList.FirstOrDefault(x => x.ContractCode == rtm.contract_code && x.Direction == rtm.direction);
                }
                if (!MainViewModel.GetInstance().SubscribedStocks.Contains(rtm.contract_code))
                {
                    MainViewModel.GetInstance().SubscribedStocks.Add(rtm.contract_code);
                    ScoketManager.GetInstance().SendSubscribeInfo(7, rtm.contract_code);
                }

                foreach (string key in CommHelper.CFSmvmList.Keys)
                {
                    PotionDetailModelViewModel temp1 = PositionViewModel.Instance().PMList.FirstOrDefault(x => (x.ContractId + x.Direction) == key);
                    if (temp1 == null) continue;
                    double maxprice = 99999999999;
                    double minprice = 0;
                    double maxprice1 = 0;
                    double minprice1 = 99999999999;
                    int maxnum = 0;
                    int minnum = 0;
                    foreach (CheckFullStopModelViewModel item in CommHelper.CFSmvmList[key])
                    {
                        if (item == null) continue;
                        if (item.StoplossPrice > 0 || item.StopprofitPrice > 0)
                        {
                            if (item.Direction == "B")
                            {
                                if (item.StopprofitPrice > 0)
                                {
                                    if (item.StopprofitPrice < maxprice)
                                    {
                                        maxprice = item.StopprofitPrice;
                                        maxnum = item.OrderVolume;
                                    }
                                    else if (item.StopprofitPrice == maxprice)
                                    {
                                        maxnum += item.OrderVolume;
                                    }

                                }
                                if (item.StoplossPrice > 0)
                                {
                                    if (item.StoplossPrice > minprice)
                                    {
                                        minprice = item.StoplossPrice;
                                        minnum = item.OrderVolume;
                                    }
                                    else if (item.StoplossPrice == minprice)
                                    {
                                        minnum += item.OrderVolume;
                                    }

                                }
                                if (maxprice < 99999999999 && maxnum > 0)
                                {
                                    temp1.ProfitVolume = maxprice + "/" + maxnum;

                                }
                                if (minprice > 0 && minnum > 0)
                                {
                                    temp1.LossVolume = minprice + "/" + minnum;

                                }
                            }
                            else
                            {
                                if (item.StopprofitPrice > 0)
                                {
                                    if (item.StopprofitPrice > maxprice1)
                                    {
                                        maxprice1 = item.StopprofitPrice;
                                        maxnum = item.OrderVolume;
                                    }
                                    else if (item.StopprofitPrice == maxprice1)
                                    {
                                        maxnum += item.OrderVolume;
                                    }

                                }
                                if (item.StoplossPrice > 0)
                                {
                                    if (item.StoplossPrice < minprice1)
                                    {
                                        minprice1 = item.StoplossPrice;
                                        minnum = item.OrderVolume;
                                    }
                                    else if (item.StoplossPrice == minprice1)
                                    {
                                        minnum += item.OrderVolume;
                                    }

                                }
                                if (maxprice1 > 0 && maxnum > 0)
                                {
                                    temp1.ProfitVolume = maxprice1 + "/" + maxnum;

                                }
                                if (minprice < 99999999999 && minnum > 0)
                                {
                                    temp1.LossVolume = minprice1 + "/" + minnum;

                                }
                            }
                        }
                    }
                }
                TransactionViewModel.Instance().AgreementChangedExecuteChanged();
                if (pavm.PMList.FirstOrDefault(x => x.ContractCode == rtm.contract_code && x.Direction == rtm.direction) == null)
                {
                    if (CommHelper.CFSmvmList.ContainsKey(rtm.contract_code + rtm.direction))
                    {
                        CommHelper.CFSmvmList[rtm.contract_code + rtm.direction].Clear();
                        LogHelper.Info("4007中移除止盈止损" + rtm.contract_id + rtm.direction + "清除数据");
                    }
                }

                //PositionViewModel.Instance().JSAbleVolume();
                TransactionViewModel.Instance().FigureUpNum(TransactionViewModel.Instance()._futures);
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }
    }
}
