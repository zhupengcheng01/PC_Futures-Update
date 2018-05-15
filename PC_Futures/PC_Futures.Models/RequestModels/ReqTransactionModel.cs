using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
   public class ReqTransactionModel
    {
        public int cmdcode { get; set; }

        public TransactionModel content { get; set; }
    }
}
