using PC_Futures.Models;
using System.Collections.Generic;


namespace Utilities
{
    public class ContractVariety
    {
        /// <summary>
        /// 全局的品种
        /// </summary>
        public static Dictionary<string, VarietyModel> Varieties = new Dictionary<string, VarietyModel>();
        /// <summary>
        /// 汇率
        /// </summary>
        public static Dictionary<string, ParitiesModel> Parities = new Dictionary<string, ParitiesModel>();

        /// <summary>
        /// 是否启动自动止盈止损
        /// </summary>
        public static bool IsAutoCheckFullStop = false;

        /// <summary>
        /// 所有持仓的止盈止损
        /// </summary>
     //   public static Dictionary<string, List<CheckFullStopModelViewModel>> CFSmvmList = new Dictionary<string, List<CheckFullStopModelViewModel>>();
        /// <summary>
        /// 自动止盈止损中合约对应的持仓ID
        /// </summary>
        public static Dictionary<string, List<string>> ContracPostionID = new Dictionary<string, List<string>>();
        /// <summary>
        /// 当前持仓中止损的相关价格
        /// </summary>
        public static Dictionary<string, AutoLossPrice> PostionPrice = new Dictionary<string, AutoLossPrice>();
        /// <summary>
        /// 是否在下单面板
        /// </summary>
        public static bool IsLoginSuccess = false;
        /// <summary>
        /// 收费方式：0，比例，1固定值
        /// </summary>
        public static CalcDepositModel Depostion = new CalcDepositModel();
        /// <summary>
        /// 保证金模板
        /// </summary>
        public static List<MarginModel> Margins { get; set; }
        /// <summary>
        /// 可用资金保留位数
        /// </summary>
        public static double effectivityLenth = 100;
        public static object main = null;

    }
    public class AutoLossPrice
    {
        /// <summary>
        /// 止损价
        /// </summary>
        public double LossPrice { get; set; }
        /// <summary>
        /// 止损最新价
        /// </summary>
        public double NewPrice { get; set; }
    }

}
