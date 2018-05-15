using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ReqCannetOrderModel
    {
        public int cmdcode { get; set; }

        public CannetOrderModel content { get; set; }
    }
    public class CannetOrderModel
    {

        public string user_id { get; set; }
        /// <summary>
        /// order_orderref
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// OperatorTradeType
        /// </summary>
        public int resource { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string operator_id { get; set; }
    }

}
