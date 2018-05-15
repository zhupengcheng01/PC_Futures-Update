using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class DeleteCheckFullStop
    {
        public string contract_id { get; set; }
        public string user_id { get; set; }
        public string direction { get; set; }
        public int resource { get; set; }
        public string operator_id { get; set; }
    }
    public class ReqDeleteCheckFullStop
    {
        public int cmdcode { get; set; }
        public DeleteCheckFullStop content { get; set; }
    }
}
