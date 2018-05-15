using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
  public  class RestDescriptModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public RtDescriptModel content { get; set; }

        public int errcode { get; set; }
    }
    public class RtDescriptModel
    {
        public string user_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string descript_text { get; set; }

    }
}
