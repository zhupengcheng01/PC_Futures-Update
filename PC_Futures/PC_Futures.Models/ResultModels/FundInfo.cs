using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestFundInfo
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public FundInfo content { get; set; }

        public int errcode { get; set; }
    }
    public class FundInfo
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string login_name { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string account_name { get; set; }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public double use_margin { get; set; }
        /// <summary>
        /// 上日权益
        /// </summary>
        public double yester_equity { get; set; }
        /// <summary>
        /// 劣后资金
        /// </summary>
        public double inferior_fund { get; set; }
        /// <summary>
        /// 优先资金
        /// </summary>
        public double priority_fund { get; set; }

        /// <summary>
        /// 可用资金
        /// </summary>
        public double able_fund { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public double fee { get; set; }
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double close_profit_loss { get; set; }
        /// <summary>
        /// 当前权益
        /// </summary>
        public double current_equity { get; set; }
        /// <summary>
        /// 出金
        /// </summary>
        public double out_money { get; set; }
        /// <summary>
        /// 入金
        /// </summary>
        public double in_money { get; set; }
        /// <summary>
        /// 风险度
        /// </summary>
        public double risk_levels { get; set; }
    }
}
