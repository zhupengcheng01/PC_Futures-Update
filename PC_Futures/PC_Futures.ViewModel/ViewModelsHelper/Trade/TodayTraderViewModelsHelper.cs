using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class TodayTraderViewModelsHelper
    {/// <summary>
     /// 成交明细
     /// </summary>
     /// <param name="para"></param>
        public void RevcTradeData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteTradeData, para);
        }
        public void ExecuteTradeData(object para)
        {
            try
            {

                TodayTraderModel ttm = para as TodayTraderModel;
                if (TodayTraderViewModels.Instance().TodayTraderList.FirstOrDefault(x => x.TradeNumber == ttm.trade_id) != null)
                { return; }
                if (ttm.bLast)
                {
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
                    foreach (TodayTraderModelViewModel item in dicTTVM.Values)
                    {
                        TodayTraderViewModels.Instance().TodayTraderListALL.Add(item);
                    }
                    return;
                }
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
                //添加持仓集合
                TodayTraderViewModels.Instance().TodayTraderList.Add(new TodayTraderModelViewModel(ttm));
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }
    }
}
