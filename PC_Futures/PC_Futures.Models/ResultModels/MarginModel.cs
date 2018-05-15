using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
  public  class MarginModel
    {
        public string margin_id { get; set; }
        /// <summary>
        /// 品种ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 多头值
        /// </summary>
        public double buy_value { get; set; }
        /// <summary>
        ///  空头值
        /// </summary>
        public double sell_value { get; set; }
    }
    public class RestMarginModel
    {
        public int cmdcode { get; set; }

        public List<MarginModel> content { get; set; }

        public int errcode { get; set; }
        public string errMsg { get; set; }
    }
    public class SendMarginModel
    {
        public int cmdcode { get; set; }

        public SMarginModel content { get; set; }

        public int errcode { get; set; }
        public string errMsg { get; set; }
    }
    public class SMarginModel
    {
        public string margin_model_id { get; set; }
        /// <summary>
        /// 品种ID
        /// </summary>
        public string product_ids { get; set; }
        /// <summary>
        /// 多头值
        /// </summary>
        public double buy_value { get; set; }
        /// <summary>
        ///  空头值
        /// </summary>
        public double sell_value { get; set; }
    }

}
