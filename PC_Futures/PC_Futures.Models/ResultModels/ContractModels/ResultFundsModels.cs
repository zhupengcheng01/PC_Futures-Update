using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    #region 资金
    public class RestTodayFundsModel
    {
        public int cmdcode { get; set; }
        public int errcode { get; set; }
        public TodayFundsModel content { get; set; }
    }
    public class TodayFundsModel
    {
        public string user_id { get; set; }
        public string login_name { get; set; }
        public string account_name { get; set; }
        public double use_margin { get; set; }
        public double yester_equity { get; set; }
        public double inferior_fund { get; set; }
        public double priority_fund { get; set; }
        public decimal able_fund { get; set; }
        public double fee { get; set; }
        public double close_profit_loss { get; set; }
        public double float_profit_loss { get; set; }
        public double current_equity { get; set; }
        public double out_money { get; set; }
        public double in_money { get; set; }
        public double risk_levels { get; set; }
    }
    #endregion
}
