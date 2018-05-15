using Futures.Enum;
using PC_Futures.Models;
using System;
using System.Linq;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class DelegationModelViewModelHelper
    {
        /// <summary>
        /// 请求返回的委托
        /// </summary>
        /// <param name="para"></param>
        public void RevcDelegationData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteDelegationData, para);
        }
        public void ExecuteDelegationData(object para)
        {
            try
            {
                DelegationModel rtm = para as DelegationModel;
                OrderCancelViewModel ocvm = OrderCancelViewModel.Instance();
                if (ocvm.Delegations.FirstOrDefault(x => x.OrderId == rtm.order_id) != null)
                {
                    return;
                }
                if (rtm.bLast)
                {
                    //PositionViewModel.Instance().JSAbleVolume();
                    TransactionViewModel.Instance().FigureUpNum(TransactionViewModel.Instance()._futures);
                    return;
                }
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


                DelegationModelViewModel dmvm = new DelegationModelViewModel(rtm);

                ocvm.Delegations.Add(dmvm);
                if (rtm.order_status == (int)DeleteType.CreateSuccess || rtm.order_status == (int)DeleteType.PortionTakeEffect)
                {

                    ocvm.KCDelegations.Add(dmvm);
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
        public void RevcCannelData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteCannelData, para);
        }
        public void ExecuteCannelData(object para)
        {
            //RCannelOrderModel rcom = para as RCannelOrderModel;
            //OrderCancelViewModel ocvm = OrderCancelViewModel.Instance();
            //DelegationModelViewModel item = ocvm.Delegations.FirstOrDefault(x => x.OrderOrderref == rcom.order_id);
            //DelegationModelViewModel item1 = ocvm.KCDelegations.FirstOrDefault(x => x.OrderOrderref == rcom.order_id);
            //if (item != null)
            //{
            //    ocvm.Delegations.Remove(item);
            //}
            //if (item1 != null)
            //{
            //    ocvm.KCDelegations.Remove(item1);
            //}
        }
    }
}
