using PC_Futures.Models;
using PC_Futures.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class FundsViewModelHelper
    {
        /// <summary>
        /// 资金查询返回2268
        /// </summary>
        /// <param name="para"></param>
        public void RevcFunds(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcFunds, para);
        }
        public void ExecuteRevcFunds(object para)
        {
            RestTodayFundsModel pm = para as RestTodayFundsModel;
            if (pm != null)
            {
                var loginvm = TradeLoginViewModel.GetInstance(null);
                if (!loginvm.LoginBtnIsEnabled)
                {
                    loginvm.LoginStatus = "正在查询资金信息...";
                }
                if (pm.errcode == 0)
                {
                    if (pm.content != null)
                    {
                        TradeInfoHelper.FundsDataModel = pm.content;
                        TradeLoginViewModel.GetInstance(null).HandleFunds();
                        DescriptViewModel.Instance().Funds.Clear();
                        DescriptViewModel.Instance().Funds.Add(new RestTodayFundsViewModel(pm.content));
                    }
                }
            }
        }
        /// <summary>
        /// 资金推送4008
        /// </summary>
        /// <param name="para"></param>
        public void RevcFundInfoData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteFundInfoData, para);
        }
        public void ExecuteFundInfoData(object para)
        {
            RestTodayFundsModel pm = para as RestTodayFundsModel;
            if (pm != null)
            {
                if (pm.errcode == 0)
                {
                    if (pm.content != null)
                    {
                        TradeInfoHelper.FundsDataModel = pm.content;
                        FundsViewModel.GetInstance().HandleViewModelData(pm.content);
                        if (DescriptViewModel.Instance().Funds.Count > 0)
                        {
                            DescriptViewModel.Instance().Funds[0].AbleFund = pm.content.able_fund;
                            DescriptViewModel.Instance().Funds[0].CloseProfitLoss = pm.content.close_profit_loss;
                            DescriptViewModel.Instance().Funds[0].CurrentEquity = pm.content.current_equity;
                            DescriptViewModel.Instance().Funds[0].Fee = pm.content.fee;
                            DescriptViewModel.Instance().Funds[0].FloatProfitLoss = pm.content.float_profit_loss;
                            DescriptViewModel.Instance().Funds[0].InferiorFund = pm.content.inferior_fund;
                            DescriptViewModel.Instance().Funds[0].InMoney = pm.content.in_money;
                            DescriptViewModel.Instance().Funds[0].OutMoney = pm.content.out_money;
                            DescriptViewModel.Instance().Funds[0].PriorityFund = pm.content.priority_fund;
                            DescriptViewModel.Instance().Funds[0].RiskLevels = pm.content.risk_levels;
                            DescriptViewModel.Instance().Funds[0].UseMargin = pm.content.use_margin;
                            DescriptViewModel.Instance().Funds[0].YesterEquity = pm.content.yester_equity;
                        }
                    }
                }
            }

        }
    }
}
