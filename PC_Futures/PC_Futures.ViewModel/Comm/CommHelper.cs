using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModel
{
    public class CommHelper
    {
        /// <summary>
        /// 按页签分组的合约集合
        /// </summary>
        public static Dictionary<string, List<FuturesViewModel>> ContractModelGroupList = new Dictionary<string, List<FuturesViewModel>>();
        /// <summary>
        /// 所有持仓的止盈止损
        /// </summary>
        public static Dictionary<string, List<CheckFullStopModelViewModel>> CFSmvmList = new Dictionary<string, List<CheckFullStopModelViewModel>>();
    }
}
