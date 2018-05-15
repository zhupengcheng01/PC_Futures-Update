using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ResultTradeLoginModel
    {
        public int cmdcode { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public TradeLoginModel content { get; set; }
    }
    public class TradeLoginModel
    {
        public string login_name { get; set; }
        public string password { get; set; }
        public string account_name { get; set; }
        public string institud_id { get; set; }
        public string user_id { get; set; }
        public int acc_prop { get; set; }
    }
}
