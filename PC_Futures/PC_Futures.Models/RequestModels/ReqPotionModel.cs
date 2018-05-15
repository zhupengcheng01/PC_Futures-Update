using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
  public  class ReqPotion
    {
        public int cmdcode { get; set; }

        public ReqLoginName content { get; set; }

     
    }

    public class ReqLoginName
    {
        public string user_id { get; set; }


    }

  
}
