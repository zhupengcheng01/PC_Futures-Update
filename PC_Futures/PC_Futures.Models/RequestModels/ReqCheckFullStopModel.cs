using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
   public class ReqCheckFullStopModel
    {
        public int cmdcode { get; set; }
        public List<CheckFullStopModel> content { get; set; }    

    }
}
