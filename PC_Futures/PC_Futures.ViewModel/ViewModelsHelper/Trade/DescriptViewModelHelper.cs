using PC_Futures.Models;
using PC_Futures.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures.ViewModel
{
   public class DescriptViewModelHelper
    { /// <summary>
      /// 请求返回的委托
      /// </summary>
      /// <param name="para"></param>
        public void RevcData(object para)
        {
            //反射回来的数据不在主线程，现通过SynchronizationContext返回到当前的主线程
            AppUtils.GetInstance().getMainLoopHandle().Post(ExecuteDelegationData, para);
        }
        public void ExecuteDelegationData(object para)
        {
            try
            {
                RtDescriptModel rdm = para as RtDescriptModel;
                DescriptViewModel.Instance().Connet = rdm.descript_text;

            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.ToString());
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
            

        }

    }
}
