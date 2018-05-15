using Futures.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Utilities
{
    public class StateToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)DeleteType.ComitServer)
            {
                resut = "已发送到服务器";
            }
            else if (type == (int)DeleteType.CreateSuccess)
            {
                resut = "创建成功";
            }
            else if (type == (int)DeleteType.AllTakeEffect)
            {
                resut = "全部生效";
            }
            else if (type == (int)DeleteType.PortionTakeEffect)
            {
                resut = "部分生效,剩余部分还在委托队列中";
            }
            else if (type == (int)DeleteType.PortionTakeEffectCannel)
            {
                resut = "部分生效,剩余委托被用户撤销";
            }
            else if (type == (int)DeleteType.PortionTakeEffectSystemCannel)
            {
                resut = "部分生效,剩余委托被系统撤销";
            }
            else if (type == (int)DeleteType.UnTakeEffecUserCannel)
            {
                resut = "未生效，用户撤销";
            }
            else if (type == (int)DeleteType.UnTakeEffecSysCannel)
            {
                resut = "未生效，被系统撤销";
            }
            else
            {
                resut = "失败";
            }
            return resut;
        }
        public string ConvertString (object value)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)DeleteType.ComitServer)
            {
                resut = "已发送到服务器";
            }
            else if (type == (int)DeleteType.CreateSuccess)
            {
                resut = "创建成功";
            }
            else if (type == (int)DeleteType.AllTakeEffect)
            {
                resut = "全部生效";
            }
            else if (type == (int)DeleteType.PortionTakeEffect)
            {
                resut = "部分生效,剩余部分还在委托队列中";
            }
            else if (type == (int)DeleteType.PortionTakeEffectCannel)
            {
                resut = "部分生效,剩余委托被用户撤销";
            }
            else if (type == (int)DeleteType.PortionTakeEffectSystemCannel)
            {
                resut = "部分生效,剩余委托被系统撤销";
            }
            else if (type == (int)DeleteType.UnTakeEffecUserCannel)
            {
                resut = "未生效，用户撤销";
            }
            else if (type == (int)DeleteType.UnTakeEffecSysCannel)
            {
                resut = "未生效，被系统撤销";
            }
            else
            {
                resut = "失败";
            }
            return resut;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StateALLToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)DeleteType.ComitServer)
            {
                resut = "已提交";
            }
            else if (type == (int)DeleteType.CreateSuccess)
            {
                resut = "创建成功";
            }
            else if (type == (int)DeleteType.AllTakeEffect)
            {
                resut = "全部生效";
            }
            else if (type == (int)DeleteType.PortionTakeEffect || type == (int)DeleteType.PortionTakeEffectCannel || type == (int)DeleteType.PortionTakeEffectSystemCannel)
            {
                resut = "部分生效";
            }
            else if (type == (int)DeleteType.UnTakeEffecUserCannel || type == (int)DeleteType.UnTakeEffecSysCannel)
            {
                resut = "撤销";
            }

            else
            {
                resut = "失败";
            }
            return resut;
        }

        public string ConvertString (object value)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)DeleteType.ComitServer)
            {
                resut = "已提交";
            }
            else if (type == (int)DeleteType.CreateSuccess)
            {
                resut = "创建成功";
            }
            else if (type == (int)DeleteType.AllTakeEffect)
            {
                resut = "全部生效";
            }
            else if (type == (int)DeleteType.PortionTakeEffect || type == (int)DeleteType.PortionTakeEffectCannel || type == (int)DeleteType.PortionTakeEffectSystemCannel)
            {
                resut = "部分生效";
            }
            else if (type == (int)DeleteType.UnTakeEffecUserCannel || type == (int)DeleteType.UnTakeEffecSysCannel)
            {
                resut = "撤销";
            }

            else
            {
                resut = "失败";
            }
            return resut;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
    public class BillStateToValueConverter : IValueConverter
    {
      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)ConditionStateType.CONDITION_STATE_WORKING)
            {
                resut = "新建";
            }
            else if (type == (int)ConditionStateType.CONDITION_STATE_HADTRRIGER)
            {
                resut = "已触发";
            }
            else if (type == (int)ConditionStateType.CONDITION_STATE_FAIL)
            {
                resut = "失败";
            }
            else if (type == (int)ConditionStateType.CONDITION_STATE_DELETE)
            {
                resut = "删除";
            }
           
            else
            {
                resut = "未知";
            }
            return resut;
        }
        public string ConvertString(object value)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)ConditionStateType.CONDITION_STATE_WORKING)
            {
                resut = "新建";
            }
            else if (type == (int)ConditionStateType.CONDITION_STATE_HADTRRIGER)
            {
                resut = "已触发";
            }
            else if (type == (int)ConditionStateType.CONDITION_STATE_FAIL)
            {
                resut = "失败";
            }
            else if (type == (int)ConditionStateType.CONDITION_STATE_DELETE)
            {
                resut = "删除";
            }

            else
            {
                resut = "未知";
            }
            return resut;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

}
