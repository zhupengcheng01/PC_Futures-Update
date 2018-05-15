using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.WebScoket
{

    public class RequestCmdCode
    {
        /// <summary>
        /// 下单
        /// </summary>
        public static int PlaceOrderCode = 4001;
        /// <summary>
        /// 撤单
        /// </summary>
        public static int CannelOrderCode = 4004;

        /// <summary>
        /// 查询汇率
        /// </summary>
        public static int SelectParities = 2191;
        /// <summary>
        /// 查询持仓总汇
        /// </summary>
        public static int SelectPotionCode = 2209;
        /// <summary>
        /// 查询持仓明细
        /// </summary>
        public static int SelectPotionDetialCode = 2211;
        /// <summary>
        /// 当日委托查询
        /// </summary>
        public static int SelectOrderCancel = 2213;
        /// <summary>
        /// 当日成交
        /// </summary>
        public static int ToDayTradeCode = 2217;
        /// <summary>
        /// 查询资金
        /// </summary>
        public static int SelectFundsCode = 2267;
        /// <summary>
        /// 条件单查询
        /// </summary>
        public static int SelectConditionBill = 5001;
        /// <summary>
        /// 条件单增加
        /// </summary>
        public static int AddConditionBill = 5003;
        /// <summary>
        /// 条件单修改
        /// </summary>
        public static int UpConditionBill = 5005;
        /// <summary>
        /// 条件单删除
        /// </summary>
        public static int DeleteConditionBill = 5007;

        /// <summary>
        /// 止盈止损查询
        /// </summary>
        public static int SelectStopLoss = 5009;
        /// <summary>
        /// 止盈止损增加
        /// </summary>
        public static int AddStopLoss = 5017;
        /// <summary>
        /// 止盈止损修改
        /// </summary>
        public static int UPStopLoss = 5013;
        /// <summary>
        /// 止盈止损删除
        /// </summary>
        public static int DeleteStopLoss = 5021;
        /// <summary>
        /// 查询品种信息
        /// </summary>
        public static int SelectVariety = 2240;
        /// <summary>
        /// 请求自选
        /// </summary>
        public static int RequestOptional = 2244;
        /// <summary>
        /// 修改密码
        /// </summary>
        public static int RequestModifyPwd = 2207;

        /// <summary>
        /// 结算单
        /// </summary>
        public static int ReqDescript = 2225;
        /// <summary>
        /// 资金查询
        /// </summary>
        public static int ReqFund = 2267;

        /// <summary>
        /// 请求收费的方式，0比例1固定值
        /// </summary>
        public static int ReqCalcDeposit = 2197;
       /// <summary>
       /// 查询保证金模板
       /// </summary>
        public static int ReqMargin = 2260;
    }
}
