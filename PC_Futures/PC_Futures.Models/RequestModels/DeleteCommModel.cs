using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
   public class DeleteCondtionModel
    {
        public string errMsg;

        public int cmdcode { get; set; }     
        public DeleteModel content { get; set; }
        public int errcode { get; set; }
    }
    public class DeleteModel
    {
        public string condition_orderID { get; set; }
        public string user_id { get; set; }
        public int resource { get; set; }
    }
}
