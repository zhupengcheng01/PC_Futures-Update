using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures
{
   public class RequestDataEntity
    {
        /// <summary>
        /// 订阅或者反订阅（7订阅、8反订阅）
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 订阅股票的代码
        /// </summary>
        public string data { get; set; }
    }
}
