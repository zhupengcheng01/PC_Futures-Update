using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class PositionViewModelHelper
    {
        ///// <summary>
        ///// 请求返回的委托
        ///// </summary>
        ///// <param name="para"></param>
        //public void RevcPositionData(object para)
        //{
        //    //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
        //    AppUtils.GetInstance().getMainLoopHandle().Post(ExecutePositionData, para);
        //}
        //public void ExecutePositionData(object para)
        //{
        //    PotionModel pm = para as PotionModel;
        //    if (pm.blast)
        //    {
        //        //结束推送
        //        return;
        //    }
        //    //PositionModelViewModel pvm = new PositionModelViewModel(pm);
        //    //PositionAllViewModel.Instance().PMList.Add(pvm);
        //    //PositionViewModel.Instance().PMList.Add(pvm);
        //}


        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcPositionDetialData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecutePositionDetialData, para);
        }
        public void ExecutePositionDetialData(object para)
        {
            try
            {
                PotionDetailModel pm = para as PotionDetailModel;
                PositionAllViewModel pavm = PositionAllViewModel.Instance();
                if (pavm.DetPMList.FirstOrDefault(x => x.PsitionId == pm.position_id) != null) return;
                if (pm.bLast)
                {
                    PotionDetailModelViewModel[] temp = pavm.DetPMList.ToArray();
                    //合约名加方向作为K
                    Dictionary<string, PotionDetailModelViewModel> dicod = new Dictionary<string, PotionDetailModelViewModel>();
                    foreach (PotionDetailModelViewModel item in temp)
                    {
                        PotionDetailModelViewModel pm1 = item.Clone(item);
                        if (!dicod.Keys.Contains(pm1.ContractCode + pm1.Direction))
                        {
                            pm1.AllPrice = pm1.OpenPrice*pm1.PositionVolume;
                            dicod.Add(pm1.ContractCode + pm1.Direction, pm1);
                            MainViewModel.GetInstance().SubscribedStocks.Add(pm1.ContractCode);

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
                        foreach (PotionDetailModelViewModel item in dicod.Values)
                        {
                            pavm.PMList.Add(item);
                            PositionViewModel.Instance().PMList.Add(item);
                        }
                    }
                    ScoketManager.GetInstance().SendSubscribeInfo(7, MainViewModel.GetInstance().SubscribedStocks);
                    //发送命令
                    ReqPotion rp = new ReqPotion();
                    rp.cmdcode = RequestCmdCode.SelectStopLoss;
                    rp.content = new ReqLoginName() { user_id = UserInfoHelper.UserId };
                    ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));
                    //PositionViewModel.Instance().JSAbleVolume();
                    TransactionViewModel.Instance().FigureUpNum(TransactionViewModel.Instance()._futures);
                    return;
                }
            
                VarietyModel vm = null;
                string[] values = pm.contract_id.Split(' ');
                if (values.Length == 3)
                {
                    string varietie = values[1];
                    if (ContractVariety.Varieties.ContainsKey(varietie))
                    {
                        vm = ContractVariety.Varieties[varietie];
                    }
                    if (vm != null)
                    {
                        pm.precision = vm.precision;
                    }
                }

                PotionDetailModelViewModel pvm = new PotionDetailModelViewModel(pm);
                pavm.DetPMList.Add(pvm);
               
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }

    }
}
