using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.WebScoket
{
    public class ConstMothed
    {
        public static Dictionary<string, string> ClassCode = new Dictionary<string, string>();
        public static Dictionary<string, string> MothedCode = new Dictionary<string, string>();
        public static List<string> SussCodes = new List<string>();
        public static void Init()
        {
            ClassCode.Add("3066", "ChildAccountAuditViewModelHelper");
            MothedCode.Add("3066", "RevcSetAutoAuditChildAccountData");
            SussCodes.Add("3066");

            //ClassCode.Add("5004", "ConditionBillViewModelHelper");
            //MothedCode.Add("5004", "RevcAddData");
            //SussCodes.Add("5004");
            //ClassCode.Add("5006", "ConditionBillViewModelHelper");
            //MothedCode.Add("5006", "RevcUpData");
            //SussCodes.Add("5006");
            ClassCode.Add("2210", "PositionViewModelHelper");
            MothedCode.Add("2210", "RevcPositionData");
            SussCodes.Add("2210");

            ClassCode.Add("2212", "PositionViewModelHelper");
            MothedCode.Add("2212", "RevcPositionDetialData");
            SussCodes.Add("2212");
            ClassCode.Add("2214", "DelegationModelViewModelHelper");
            MothedCode.Add("2214", "RevcDelegationData");
            SussCodes.Add("2214");

            ClassCode.Add("2268", "FundsViewModelHelper");
            MothedCode.Add("2268", "RevcFunds");
            SussCodes.Add("2268");

            ClassCode.Add("2218", "TodayTraderViewModelsHelper");
            MothedCode.Add("2218", "RevcTradeData");
            SussCodes.Add("2218");

            ClassCode.Add("5002", "ConditionBillViewModelHelper");
            MothedCode.Add("5002", "RevcSelectData");
            SussCodes.Add("5002");

            ClassCode.Add("5004", "ConditionBillViewModelHelper");
            MothedCode.Add("5004", "RevcAddData");
            SussCodes.Add("5004");

            ClassCode.Add("5006", "ConditionBillViewModelHelper");
            MothedCode.Add("5006", "ExecuteUpData");
            SussCodes.Add("5006");

            ClassCode.Add("5008", "ConditionBillViewModelHelper");
            MothedCode.Add("5008", "RevcDeleteData");
            SussCodes.Add("5008");

            ClassCode.Add("5023", "ConditionBillViewModelHelper");
            MothedCode.Add("5023", "RevcSendDeleteData");
            SussCodes.Add("5023");

            ClassCode.Add("5024", "CheckFullStopViewModelHelper");
            MothedCode.Add("5024", "RevcDeleteData");
            SussCodes.Add("5024");

            ClassCode.Add("5022", "CheckFullStopViewModelHelper");
            MothedCode.Add("5022", "RevcDeleteData1");
            SussCodes.Add("5022");

            ClassCode.Add("5020", "CheckFullStopViewModelHelper");
            MothedCode.Add("5020", "RevcUpData");
            SussCodes.Add("5020");

            ClassCode.Add("4002", "TransactionViewModelHelper");
            MothedCode.Add("4002", "RevcPlaceOrderData");
            SussCodes.Add("4002");

            ClassCode.Add("4003", "TransactionViewModelHelper");
            MothedCode.Add("4003", "RevcSendPlaceOrderData");
            SussCodes.Add("4003");

            ClassCode.Add("40031", "TransactionViewModelHelper");
            MothedCode.Add("40031", "RevcSendPlaceOrderData1");
            SussCodes.Add("40031");

            ClassCode.Add("4006", "TransactionViewModelHelper");
            MothedCode.Add("4006", "RevcTransactionInfoData");
            SussCodes.Add("4006");
            ClassCode.Add("4005", "DelegationModelViewModelHelper");
            MothedCode.Add("4005", "RevcCannelData");
            SussCodes.Add("4005");
            ClassCode.Add("4007", "TransactionViewModelHelper");
            MothedCode.Add("4007", "RevcPositionInfoData");
            SussCodes.Add("4007");
            ClassCode.Add("4008", "FundsViewModelHelper");
            MothedCode.Add("4008", "RevcFundInfoData");
            SussCodes.Add("4008");

            ClassCode.Add("5010", "CheckFullStopViewModelHelper");
            MothedCode.Add("5010", "RevcSelectData");
            SussCodes.Add("5010");

            ClassCode.Add("5018", "CheckFullStopViewModelHelper");
            MothedCode.Add("5018", "RevcAddData");
            SussCodes.Add("5018");

            ClassCode.Add("5014", "CheckFullStopViewModelHelper");
            MothedCode.Add("5014", "RevcUpData");
            SussCodes.Add("5014");
            ClassCode.Add("5016", "CheckFullStopViewModelHelper");
            MothedCode.Add("5016", "RevcDeleteData");
            SussCodes.Add("5016");


            ClassCode.Add("2236", "TradeLoginViewModelHelper");
            MothedCode.Add("2236", "RevcLoginData");
            SussCodes.Add("2236");

            ClassCode.Add("2241", "TradeLoginViewModelHelper");
            MothedCode.Add("2241", "RevVarietyData");
            SussCodes.Add("2241");
            ClassCode.Add("2192", "TradeLoginViewModelHelper");
            MothedCode.Add("2192", "RevParitiesData");
            SussCodes.Add("2192");

            ClassCode.Add("2245", "MainViewModelHelper");
            MothedCode.Add("2245", "RevcOptionalData");
            SussCodes.Add("2245");

            ClassCode.Add("2247", "MainViewModelHelper");
            MothedCode.Add("2247", "RevcAddOptionalData");
            SussCodes.Add("2247");

            ClassCode.Add("2249", "MainViewModelHelper");
            MothedCode.Add("2249", "RevcDelOptionalData");
            SussCodes.Add("2249");

            ClassCode.Add("2208", "MainViewModelHelper");
            MothedCode.Add("2208", "RevcModifyPwd");
            SussCodes.Add("2208");

            ClassCode.Add("2226", "DescriptViewModelHelper");
            MothedCode.Add("2226", "RevcData");
            SussCodes.Add("2226");

            ClassCode.Add("2198", "TradeLoginViewModelHelper");
            MothedCode.Add("2198", "RevFeeTypeData");
            SussCodes.Add("2198");

            ClassCode.Add("2261", "TradeLoginViewModelHelper");
            MothedCode.Add("2261", "RevFeeMarginData");
            SussCodes.Add("2261");
            ClassCode.Add("2262", "TradeLoginViewModelHelper");
            MothedCode.Add("2262", "RevSedFeeMarginData");
            SussCodes.Add("2262");
        }

        public static void ClearData()
        {
            ClassCode.Clear();
            MothedCode.Clear();
            SussCodes.Clear(); ;
        }

    }
}
