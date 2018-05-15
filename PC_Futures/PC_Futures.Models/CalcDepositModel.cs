using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestCalcDepositModel
    {
        public string errMsg;

        /// <summary>
        /// FeeCalcType
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// MarginCalcType
        /// </summary>
        public CalcDepositModel content { get; set; }
    }
    public class CalcDepositModel
    {
        /// <summary>
        /// FeeCalcType
        /// </summary>
        public int calc_fee_model { get; set; }
        /// <summary>
        /// MarginCalcType
        /// </summary>
        public int calc_deposit_model { get; set; }
    }
}
