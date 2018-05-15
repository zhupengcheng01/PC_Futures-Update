using Futures.Enum;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class CheckFullStopViewModelHelper
    {

        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcSelectData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteSelectData, para);
        }
        public void ExecuteSelectData(object para)
        {
            try
            {
                CheckFullStopModel cfsm = para as CheckFullStopModel;
                if (cfsm.blast)
                {
                    // CheckFullStopViewModel.Instance(null).AddData();
                    foreach (string key in CommHelper.CFSmvmList.Keys)
                    {
                        PotionDetailModelViewModel temp = PositionViewModel.Instance().PMList.FirstOrDefault(x => (x.ContractId + x.Direction) == key);
                        if (temp == null) continue;
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
                                        temp.ProfitVolume = maxprice + "/" + maxnum;

                                    }
                                    if (minprice > 0 && minnum > 0)
                                    {
                                        temp.LossVolume = minprice + "/" + minnum;

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
                                        temp.ProfitVolume = maxprice1 + "/" + maxnum;

                                    }
                                    if (minprice < 99999999999 && minnum > 0)
                                    {
                                        temp.LossVolume = minprice1 + "/" + minnum;

                                    }

                                }
                            }
                        }
                    }
                    //  PositionViewModel.Instance().PMList.Add(item);
                    return;
                }
                if (string.IsNullOrEmpty(cfsm.contract_id + cfsm.direction)) return;
                if (CommHelper.CFSmvmList.ContainsKey(cfsm.contract_id + cfsm.direction))
                {
                    if (CommHelper.CFSmvmList[cfsm.contract_id + cfsm.direction].FirstOrDefault(x => x.StoplossId == cfsm.stoploss_id) == null)
                    {
                        CommHelper.CFSmvmList[cfsm.contract_id + cfsm.direction].Add(new CheckFullStopModelViewModel(cfsm));
                    }
                }
                else
                {
                    CommHelper.CFSmvmList.Add(cfsm.contract_id + cfsm.direction, new List<CheckFullStopModelViewModel>());
                    CommHelper.CFSmvmList[cfsm.contract_id + cfsm.direction].Add(new CheckFullStopModelViewModel(cfsm));
                }

            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
            //CheckFullStopViewModel.Instance(null).CFSMVMList.Add(new CheckFullStopModelViewModel(cfsm));
            //CheckFullStopViewModel.Instance(null).IsHaveData = true;

        }

        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcAddData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteAddData, para);
        }
        public void ExecuteAddData(object para)
        {
            try
            {
                List<CheckFullStopModel> cfsms = para as List<CheckFullStopModel>;
                if (cfsms != null && cfsms.Count > 0)
                {
                    Result(cfsms);
                    if (CheckFullStopViewModel.IsInstance())
                    {
                        CheckFullStopViewModel.Instance(null).Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.ToString());
            }
        }
        public void Result(List<CheckFullStopModel> cfsms)
        {

            List<CheckFullStopModel> temps = cfsms;
            double maxprice = 99999999999;
            double minprice = 0;
            double maxprice1 = 0;
            double minprice1 = 99999999999;
            int maxnum = 0;
            int minnum = 0;

            if (CommHelper.CFSmvmList.ContainsKey(temps[0].contract_id + temps[0].direction))
            {
                CommHelper.CFSmvmList[temps[0].contract_id + temps[0].direction].Clear();
            }
            else
            {
                CommHelper.CFSmvmList.Add(temps[0].contract_id + temps[0].direction, new List<CheckFullStopModelViewModel>());

            }
            CommHelper.CFSmvmList[temps[0].contract_id + temps[0].direction].Clear();

            foreach (CheckFullStopModel item in temps)
            {
                if (item.stoploss_price > 0 || item.stopprofit_price > 0)
                {
                    if (item.direction == "B")
                    {
                        if (item.stopprofit_price > 0)
                        {
                            if (item.stopprofit_price < maxprice)
                            {
                                maxprice = item.stopprofit_price;
                                maxnum = item.order_volume;
                            }
                            else if (item.stopprofit_price == maxprice)
                            {
                                maxnum += item.order_volume;
                            }

                        }
                        if (item.stoploss_price > 0)
                        {
                            if (item.stoploss_price > minprice)
                            {
                                minprice = item.stoploss_price;
                                minnum = item.order_volume;
                            }
                            else if (item.stoploss_price == minprice)
                            {
                                minnum += item.order_volume;
                            }
                        }
                    }
                    else
                    {
                        if (item.stopprofit_price > 0)
                        {
                            if (item.stopprofit_price > maxprice1)
                            {
                                maxprice1 = item.stopprofit_price;
                                maxnum = item.order_volume;
                            }
                            else if (item.stopprofit_price == maxprice1)
                            {
                                maxnum += item.order_volume;
                            }

                        }
                        if (item.stoploss_price > 0)
                        {
                            if (item.stoploss_price < minprice1)
                            {
                                minprice1 = item.stoploss_price;
                                minnum = item.order_volume;
                            }
                            else if (item.stoploss_price == minprice1)
                            {
                                minnum += item.order_volume;
                            }
                        }
                    }
                    CheckFullStopModel cfsm = new CheckFullStopModel();
                    cfsm.user_id = item.user_id;
                    cfsm.contract_id = item.contract_id;
                    cfsm.direction = item.direction;
                    cfsm.open_offset = (int)OffsetType.OFFSET_COVER;
                    cfsm.order_volume = item.order_volume;
                    cfsm.stoploss_price = item.stoploss_price;
                    cfsm.stopprofit_price = item.stopprofit_price;
                    cfsm.float_loss = item.float_loss;
                    cfsm.stoploss_id = item.stoploss_id;
                    cfsm.float_loss_flag = (int)FloatLossFlag.FLF_FloatLoss;
                    if (CommParameterSetting.StopLossModel.StopLossPrice == "最新价")
                    {
                        cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_LASTPRICE;
                    }
                    else
                    {
                        if (item.direction == "S")
                        {
                            cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_BUYONEPRICE;
                            cfsm.order_add_price = CommParameterSetting.StopLossModel.BuyNum;
                        }
                        else
                        {
                            cfsm.order_add_price = CommParameterSetting.StopLossModel.SellNum;
                            cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_SELLONEPRICE;
                        }
                    }
                    if (CommParameterSetting.StopLossModel.StopProfitDelegation == "市价")
                    {
                        cfsm.price_type = "M";
                    }
                    else
                    {
                        cfsm.price_type = "L";
                    }
                    cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                    cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");

                    CheckFullStopModelViewModel cfsmvm = new CheckFullStopModelViewModel(cfsm);
                    if (CommHelper.CFSmvmList[item.contract_id + item.direction].FirstOrDefault(x => x.StoplossId == cfsm.stoploss_id) == null)
                    {
                        CommHelper.CFSmvmList[item.contract_id + item.direction].Add(cfsmvm);
                        LogHelper.Info("止盈止损添加 " + cfsm.contract_code + ":" + cfsm.direction + ";" + cfsm.create_time);
                    }
                }
            }
            PotionDetailModelViewModel _PositionModelViewModel = PositionViewModel.Instance().PMList.FirstOrDefault(x => x.ContractId == temps[0].contract_id && x.Direction == temps[0].direction);
            if (_PositionModelViewModel == null) return;
            if (_PositionModelViewModel.Direction == "B")
            {
                if (maxprice < 99999999999 && maxnum > 0)
                {

                    _PositionModelViewModel.ProfitVolume = maxprice + "/" + maxnum;

                }
                if (minprice > 0 && minnum > 0)
                {
                    _PositionModelViewModel.LossVolume = minprice + "/" + minnum;

                }
            }
            else
            {
                if (maxprice1 > 0 && maxnum > 0)
                {

                    _PositionModelViewModel.ProfitVolume = maxprice1 + "/" + maxnum;

                }
                if (minprice1 < 99999999999 && minnum > 0)
                {
                    _PositionModelViewModel.LossVolume = minprice1 + "/" + minnum;

                }
            }

        }


        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcUpData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteUpData, para);
        }
        public void ExecuteUpData(object para)
        {
            try
            {
                CheckFullStopModel cfsm = para as CheckFullStopModel;
                //添加持仓集合
                if (CommHelper.CFSmvmList.ContainsKey(cfsm.contract_id + cfsm.direction))
                {
                    CheckFullStopModelViewModel temp = CommHelper.CFSmvmList[cfsm.contract_id + cfsm.direction].FirstOrDefault(x => x.StoplossId == cfsm.stoploss_id);
                    if (temp != null)
                    {
                        temp.StoplossPrice = cfsm.stoploss_price;
                        LogHelper.Info("止盈止损修改" + cfsm.contract_code + ":" + cfsm.direction + ";" + cfsm.create_time + "止损价:" + cfsm.stoploss_price);
                    }
                    PositionAllViewModel pavm = PositionAllViewModel.Instance();
                    PotionDetailModelViewModel pdmvm = pavm.PMList.FirstOrDefault(x => x.ContractId == cfsm.contract_id && x.Direction == cfsm.direction);
                    if (pdmvm != null)
                    {
                        string[] values = pdmvm.LossVolume.Split('/');
                        pdmvm.LossVolume = cfsm.stoploss_price + "/" + values[1];
                    }
                }
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }

        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcDeleteData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteDeleteData, para);
        }
        public void ExecuteDeleteData(object para)
        {
            try
            {
                double maxprice = 99999999999;
                double minprice = 0;
                double maxprice1 = 0;
                double minprice1 = 99999999999;
                int maxnum = 0;
                int minnum = 0;
                RStopLossStatus sls = para as RStopLossStatus;
                LogHelper.Info("5204止盈止损" + sls.contract_id + "开始");
                if (CommHelper.CFSmvmList.ContainsKey(sls.contract_id + "B"))
                {
                    CheckFullStopModelViewModel item = CommHelper.CFSmvmList[sls.contract_id + "B"].FirstOrDefault(x => x.StoplossId == sls.stoploss_id);
                    if (item != null)
                    {
                        CommHelper.CFSmvmList[sls.contract_id + "B"].Remove(item);
                        LogHelper.Info("5204止盈止损" + sls.contract_id + "B清除数据");
                    }
                    foreach (CheckFullStopModelViewModel temp in CommHelper.CFSmvmList[sls.contract_id + "B"])
                    {
                        if (temp == null) continue;
                        if (temp.StoplossPrice > 0 || temp.StopprofitPrice > 0)
                        {

                            if (item.StopprofitPrice > 0)
                            {
                                if (temp.StopprofitPrice < maxprice)
                                {

                                    maxprice = temp.StopprofitPrice;
                                    maxnum = temp.OrderVolume;
                                }
                                else if (temp.StopprofitPrice == maxprice)
                                {
                                    maxnum += temp.OrderVolume;
                                }
                            }
                            if (temp.StoplossPrice > 0)
                            {
                                if (temp.StoplossPrice > minprice)
                                {
                                    minprice = temp.StoplossPrice;
                                    minnum = temp.OrderVolume;
                                }
                                else if (temp.StoplossPrice == minprice)
                                {
                                    minnum += temp.OrderVolume;
                                }
                            }


                        }
                    }
                    PotionDetailModelViewModel _PositionModelViewModel1 = PositionViewModel.Instance().PMList.FirstOrDefault(x => x.ContractId == sls.contract_id && x.Direction == "B");
                    if (_PositionModelViewModel1 != null)
                    {

                        if (maxprice < 99999999999 && maxnum > 0)
                        {
                            _PositionModelViewModel1.ProfitVolume = maxprice + "/" + maxnum;

                        }
                        else
                        {
                            _PositionModelViewModel1.ProfitVolume = "";
                        }
                        if (minprice > 0 && minnum > 0)
                        {
                            _PositionModelViewModel1.LossVolume = minprice + "/" + minnum;

                        }
                        else
                        {
                            _PositionModelViewModel1.LossVolume = "";
                        }
                    }
                }
                if (CommHelper.CFSmvmList.ContainsKey(sls.contract_id + "S"))
                {
                    CheckFullStopModelViewModel item = CommHelper.CFSmvmList[sls.contract_id + "S"].FirstOrDefault(x => x.StoplossId == sls.stoploss_id);
                    if (item != null)
                    {
                        CommHelper.CFSmvmList[sls.contract_id + "S"].Remove(item);
                        LogHelper.Info("5204止盈止损" + sls.contract_id + "S清除数据");
                    }
                    foreach (CheckFullStopModelViewModel temp in CommHelper.CFSmvmList[sls.contract_id + "S"])
                    {
                        if (temp == null) continue;
                        if (temp.StoplossPrice > 0 || temp.StopprofitPrice > 0)
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

                        }
                    }
                    PotionDetailModelViewModel _PositionModelViewModel = PositionViewModel.Instance().PMList.FirstOrDefault(x => x.ContractId == sls.contract_id && x.Direction == "S");
                    if (_PositionModelViewModel != null)
                    {

                        if (maxprice1 > 0 && maxnum > 0)
                        {
                            _PositionModelViewModel.ProfitVolume = maxprice1 + "/" + maxnum;

                        }
                        else
                        {
                            _PositionModelViewModel.ProfitVolume = "";
                        }
                        if (minprice1 < 99999999999 && minnum > 0)
                        {
                            _PositionModelViewModel.LossVolume = minprice1 + "/" + minnum;

                        }
                        else
                        {
                            _PositionModelViewModel.LossVolume = "";
                        }

                    }
                }
                if (CheckFullStopViewModel.IsInstance())
                {
                    CheckFullStopViewModel.Instance(null).Close();
                }
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }

        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcDeleteData1(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程


            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteDeleteData1, para);
        }
        public void ExecuteDeleteData1(object para)
        {
            try
            {

                List<RStopLossStatus> slsAll = para as List<RStopLossStatus>;
                foreach (RStopLossStatus sls in slsAll)
                {
                    if (CommHelper.CFSmvmList.ContainsKey(sls.contract_id + "B"))
                    {

                        CommHelper.CFSmvmList[sls.contract_id + "B"].Clear();
                        LogHelper.Info("止盈止损" + sls.contract_id + "B清除数据");
                        PotionDetailModelViewModel temp = PositionViewModel.Instance().PMList.FirstOrDefault(x => x.ContractId == sls.contract_id && x.Direction == "B");
                        if (temp != null)
                        {
                            temp.LossVolume = null;
                            temp.ProfitVolume = null;
                        }
                        break;
                    }
                    if (CommHelper.CFSmvmList.ContainsKey(sls.contract_id + "S"))
                    {

                        CommHelper.CFSmvmList[sls.contract_id + "S"].Clear();
                        LogHelper.Info("止盈止损" + sls.contract_id + "S清除数据");
                        PotionDetailModelViewModel temp = PositionViewModel.Instance().PMList.FirstOrDefault(x => x.ContractId == sls.contract_id && x.Direction == "S");
                        if (temp != null)
                        {
                            temp.LossVolume = null;
                            temp.ProfitVolume = null;
                        }

                    }
                    break;
                }
                if (CheckFullStopViewModel.IsInstance())
                {
                    CheckFullStopViewModel.Instance(null).Close();
                }
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }
    }
}
