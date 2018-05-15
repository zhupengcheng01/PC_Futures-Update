using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ReqDescriptModel
    {
        public int cmdcode { get; set; }
        public RDescriptModel content { get; set; }
    }
    public class RDescriptModel
    {
        public string user_id { get; set; }
        public int start_date { get; set; }
        public int end_date { get; set; }
        public int settle_type { get; set; }

    }
}
