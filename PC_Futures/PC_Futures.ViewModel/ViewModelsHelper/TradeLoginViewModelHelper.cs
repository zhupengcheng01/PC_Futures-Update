using PC_Futures.Models;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class TradeLoginViewModelHelper
    {
        TradeLoginViewModel loginvm = TradeLoginViewModel.GetInstance(null);
        /// <summary>
        /// 登录返回
        /// </summary>
        /// <param name="para"></param>
        public void RevcLoginData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcLoginData, para);
        }
        public void ExecuteRevcLoginData(object para)
        {
            ResultTradeLoginModel pm = para as ResultTradeLoginModel;
            loginvm.HandleRecvLogin(pm);
        }

        /// <summary>
        /// 品种接受
        /// </summary>
        /// <param name="para"></param>
        public void RevVarietyData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcVarietyData, para);
        }
        public void ExecuteRevcVarietyData(object para)
        {
            VarietyModel vm = para as VarietyModel;
            if (!loginvm.LoginBtnIsEnabled)
            {
                loginvm.LoginStatus = "正在查询品种信息...";
            }
            if (vm.bLast)
            {
                loginvm.ReqParities();
                return;
            }
            string key = vm.product_code;
            if (!ContractVariety.Varieties.ContainsKey(key))
            {
                ContractVariety.Varieties.Add(key, vm);
            }
        }

        /// <summary>
        /// 汇率接受
        /// </summary>
        /// <param name="para"></param>
        public void RevParitiesData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcParitiesData, para);
        }
        public void ExecuteRevcParitiesData(object para)
        {

            List<ParitiesModel> temp = para as List<ParitiesModel>;
            if (temp == null || temp.Count == 0)
            {
                loginvm.LoginSuccess();
                return;
            }
            if (!loginvm.LoginBtnIsEnabled)
            {
                loginvm.LoginStatus = "正在查询汇率信息...";
            }
            foreach (ParitiesModel item in temp)
            {
                if (!ContractVariety.Parities.ContainsKey(item.currency))
                {
                    ContractVariety.Parities.Add(item.currency, item);
                }
            }
            loginvm.ReqDetType();
        }
        /// <summary>
        /// 收费方式
        /// </summary>
        /// <param name="para"></param>
        public void RevFeeTypeData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteFeeTypeData, para);
        }
        public void ExecuteFeeTypeData(object para)
        {
            CalcDepositModel cdm = para as CalcDepositModel;
            if (cdm == null) return;
            ContractVariety.Depostion.calc_deposit_model = cdm.calc_deposit_model;
            ContractVariety.Depostion.calc_fee_model = cdm.calc_fee_model;
            loginvm.ReqMargin();
        }
        /// <summary>
        /// 收费方式
        /// </summary>
        /// <param name="para"></param>
        public void RevFeeMarginData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteMarginData, para);
        }
        public void ExecuteMarginData(object para)
        {
            List<MarginModel> trmp = para as List<MarginModel>;
            if (trmp == null || trmp.Count == 0) return;
            ContractVariety.Margins = trmp;
            loginvm.LoginSuccess();
        }

        /// <summary>
        /// 收费方式
        /// </summary>
        /// <param name="para"></param>
        public void RevSedFeeMarginData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteSendMarginData, para);
        }
        public void ExecuteSendMarginData(object para)
        {
            SMarginModel trmp = para as SMarginModel;
            string[] values = trmp.product_ids.Split(',');
            foreach (string item in values)
            {
                MarginModel mm = ContractVariety.Margins.FirstOrDefault(x=>x.product_id==item);
                if (mm != null)
                {
                    mm.sell_value = trmp.sell_value;
                    mm.buy_value = trmp.buy_value;
                }
            }

        }
    }
}
