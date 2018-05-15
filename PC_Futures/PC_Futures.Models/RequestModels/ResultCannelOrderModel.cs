using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ResultCannelOrderModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public RCannelOrderModel content { get; set; }
        public int errcode { get; set; }
    }
    public class RCannelOrderModel
    {
        public string user_id { get; set; }
        public string order_id { get; set; }
    }
}
