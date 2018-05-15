using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{

    public class ResultParitiesModel
    {
        public string errMsg;

        public int cmdcode { get; set; }
        public List<ParitiesModel> content { get; set; }
        public int errcode { get; set; }
    }
    public class ParitiesModel
    {
        public string id { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 美元
        /// </summary>
        public string currency_name { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public double exchange_rate { get; set; }
        public bool Base { get; set; }
    }
}
