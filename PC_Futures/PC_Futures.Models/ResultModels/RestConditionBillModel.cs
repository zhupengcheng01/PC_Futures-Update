using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
  public  class RestConditionBillModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public ConditionBillModel content { get; set; }
        public int errcode { get; set; }
    }
}
