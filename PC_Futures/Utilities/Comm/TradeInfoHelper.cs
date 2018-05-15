using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class TradeInfoHelper
    {
        /// <summary>
        /// http请求的品种集合
        /// </summary>
        public static List<NewVarietyModel> VarietyModelList = new List<NewVarietyModel>();
        /// <summary>
        /// http请求的合约集合
        /// </summary>
        public static List<ContractModel> ContractModelList = new List<ContractModel>();
     
        /// <summary>
        /// 动态展示页签
        /// </summary>
        public static List<string> ExchangeDisplayList = new List<string>();
        /// <summary>
        /// 已订阅行情的合约
        /// </summary>
       public static List<QuotationEntity> SubscribedContractList = new List<QuotationEntity>();
        /// <summary>
        /// 自选合约
        /// </summary>
        public static List<OptionalContractModel> OptionalModelList = new List<OptionalContractModel>();
        public static TodayFundsModel FundsDataModel = new TodayFundsModel();

        public static string AName = null;

        /// <summary>
        /// 显示最新逐笔条数
        /// </summary>
        public static int EachDealCount = 20;
    }
}
