using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ResultFuturesDataModel
    {
        public string cmdcode { get; set; }
        public FuturesDataModel content { get; set; }
    }
    public class FuturesDataModel
    {
        /// <summary>
        /// 股票代码
        /// </summary>
        public string StockCode { get; set; }
        /// <summary>
        /// 股票名称
        /// </summary>
        public string StockName { get; set; }
        /// <summary>
        /// 股票前缀
        /// </summary>
        public string ExchangeCD { get; set; }
    }
}
