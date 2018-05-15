using PC_Futures.Models;
using PC_Futures.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class ConditionBillViewModelHelper
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
                ConditionBillModel rtm = para as ConditionBillModel;
                if (rtm.blast)
                {
                    return;
                }
                if (UCConditionBillViewModel.Instance().ConditionBillList.FirstOrDefault(x => x.ConditionOrderID == rtm.condition_orderID) != null) return;
                //添加持仓集合
                UCConditionBillViewModel.Instance().ConditionBillList.Add(new ConditionBillModelViewModel(rtm));

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
        public void RevcAddData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteAddData, para);
        }
        public void ExecuteAddData(object para)
        {
            try
            {
                ConditionBillModel rtm = para as ConditionBillModel;
                if (rtm==null||rtm.price_type==null)
                {
                    LogHelper.Info(para.ToString());
                    return;
                }
                //添加持仓集合
                if (UCConditionBillViewModel.Instance().ConditionBillList.FirstOrDefault(x => x.ConditionOrderID == rtm.condition_orderID) == null)
                {
              
                    UCConditionBillViewModel.Instance().ConditionBillList.Add(new ConditionBillModelViewModel(rtm));
                }
                ConditionBillViewModel.Intstace(null, 0, null, 0).Close();
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
        public void RevcUpData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteUpData, para);
        }
        public void ExecuteUpData(object para)
        {
            try
            {

                AppUtils.GetInstance().getMainLoopHandle().Post(Closeing, para);

            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }

        public void Closeing(object para)
        {
            try
            {
                ConditionBillModel rtm = para as ConditionBillModel;
                //添加持仓集合
                ConditionBillModelViewModel temp = UCConditionBillViewModel.Instance().ConditionBillList.FirstOrDefault(o => o.ConditionOrderID == rtm.condition_orderID);
                //添加持仓集合
                if (temp != null)
                {
                    temp.ConditionOrderID = rtm.condition_orderID;
                    temp.ConditionType = rtm.condition_type;
                    temp.ContractCode = rtm.contract_code;
                    temp.Direction = rtm.direction;
                    temp.OpenOffset = rtm.open_offset;
                    temp.OrderPrice = rtm.order_price;
                    temp.OrderVolume = rtm.order_volume;
                    temp.PriceType = rtm.price_type;
                    temp.TrrigerPrice = rtm.trriger_price;
                    temp.TrrigerPriceType = rtm.trriger_price_type;
                    temp.TrrigerTime = rtm.trriger_time;
                    temp.TrrigerContime = rtm.trriger_contime;
                    temp.TrrigerCondate = rtm.trriger_condate;
                    temp.TrrigerCondition = rtm.trriger_condition;
                }
                if (ConditionBillViewModel.Intstace(null) != null)
                {
                    ConditionBillViewModel.Intstace(null).Close();
                }
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }        /// <summary>
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
                DeleteModel rtm = para as DeleteModel;
                ConditionBillModelViewModel temp = UCConditionBillViewModel.Instance().ConditionBillList.FirstOrDefault(o => o.ConditionOrderID == rtm.condition_orderID);
                //添加持仓集合
                if (temp != null)
                {
                    UCConditionBillViewModel.Instance().ConditionBillList.Remove(temp);
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
        public void RevcSendDeleteData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteSendDeleteData, para);
        }
        public void ExecuteSendDeleteData(object para)
        {
            try
            {
                ConditionBillModel cmd = para as ConditionBillModel;
                ConditionBillModelViewModel item = UCConditionBillViewModel.Instance().ConditionBillList.FirstOrDefault(x => x.ConditionOrderID == cmd.condition_orderID);
                if (item != null)
                {
                    item.Status = cmd.status;
                    item.TrrigerDate = cmd.trriger_date;
                    item.TrrigerTime = cmd.trriger_time;
                    //UCConditionBillViewModel.Instance().ConditionBillList.Remove(item);
                }
            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
            }
        }

    }
}
