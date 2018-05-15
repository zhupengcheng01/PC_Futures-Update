using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class StopLossStatus
    {
        public int cmdcode { get; set; }

        public RStopLossStatus content { get; set; }

        public int errcode { get; set; }
        public string errMsg { get; set; }
    }

    public class StopLossStatusList
    {
        public int cmdcode { get; set; }

        public List<RStopLossStatus> content { get; set; }

        public int errcode { get; set; }
        public string errMsg { get; set; }
    }
    public class RStopLossStatus
    {
        public string stoploss_id { get; set; }
        public string contract_id { get; set; }
    }
}
