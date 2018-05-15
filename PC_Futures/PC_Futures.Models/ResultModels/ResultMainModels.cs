using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ResultModifyPwdModel
    {
        public int cmdcode { get; set; }
        public int errcode { get; set; }
        public ModifyPwdModel content { get; set; }
    }
    public class ModifyPwdModel
    {
        public string user_id { get; set; }
        public string old_password { get; set; }
        public string new_password { get; set; }
    }
}
