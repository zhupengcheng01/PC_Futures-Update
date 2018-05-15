using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class MainViewModelHelper
    {
        /// <summary>
        /// 自选查询
        /// </summary>
        /// <param name="para"></param>
        public void RevcOptionalData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcOptionalData, para);
        }
        public void ExecuteRevcOptionalData(object para)
        {
            ResultOptionalContractModel pm = para as ResultOptionalContractModel;
            if (pm != null)
            {
                var loginvm = TradeLoginViewModel.GetInstance(null);
                if (!loginvm.LoginBtnIsEnabled)
                {
                    loginvm.LoginStatus = "正在查询自选合约...";
                }
                if (pm.errcode == 0)
                {
                    if (pm.content != null)
                    {
                        if(pm.content.bLast)
                        {
                            TradeLoginViewModel.GetInstance(null).HandleOptional();
                        }
                        else
                        {
                            TradeInfoHelper.OptionalModelList.Add(pm.content);
                        }
                        
                    }
                }
            }
        }

        /// <summary>
        /// 新增自选回传
        /// </summary>
        /// <param name="para"></param>
        public void RevcAddOptionalData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcAddOptionalData, para);
        }
        public void ExecuteRevcAddOptionalData(object para)
        {
            ResultOptionalContractModel pm = para as ResultOptionalContractModel;
            if (pm != null)
            {
                if (pm.errcode == 0)
                {
                    if (pm.content != null)
                    {
                        TradeInfoHelper.OptionalModelList.Add(pm.content);
                        TradeQuotesViewModel.GetInstance(null).AddOptionalData(pm.content);
                    }
                }
            }
        }
        /// <summary>
        /// 删除自选回传
        /// </summary>
        /// <param name="para"></param>
        public void RevcDelOptionalData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcDelOptionalData, para);
        }
        public void ExecuteRevcDelOptionalData(object para)
        {
            ResultOptionalContractModel pm = para as ResultOptionalContractModel;
            if (pm != null)
            {
                if (pm.errcode == 0)
                {
                    if (pm.content != null)
                    {
                        var model= TradeInfoHelper.OptionalModelList.FirstOrDefault(o => string.Equals(o.serial_number, pm.content.serial_number));
                        if(model!=null)
                        {
                            TradeInfoHelper.OptionalModelList.Remove(model);
                            TradeQuotesViewModel.GetInstance(null).DelOptionalData(model);
                        }
                        
                    }
                }
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="para"></param>
        public void RevcModifyPwd(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteRevcModifyPwd, para);
        }
        public void ExecuteRevcModifyPwd(object para)
        {
            ResultModifyPwdModel pm = para as ResultModifyPwdModel;
            ModifyPwdWindowViewModel.GetInstance().HandlePwd(pm);
        }
    }
}
