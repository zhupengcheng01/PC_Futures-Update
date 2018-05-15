using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RequestModifyPwd
    {
        public int cmdcode { get; set; }

        public ModifyPwd content { get; set; }
    }
    public class ModifyPwd
    {
        public string user_id { get; set; }
        public string old_password { get; set; }
        public string new_password { get; set; }
        public int resource { get; set; }
        public string operator_id { get; set; }
    }
}
