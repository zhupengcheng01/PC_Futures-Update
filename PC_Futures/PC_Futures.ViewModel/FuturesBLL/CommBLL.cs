using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.FuturesBLL
{
    public class CommBLL
    {
        private ScoketManager _scoketManager;
        public CommBLL()
        {
            _scoketManager = ScoketManager.GetInstance();
        }
        public void ReqTcp(int cmdcode, string userid)
        {
            ReqPotion trm = new ReqPotion();
            trm.cmdcode = cmdcode;
            trm.content = new ReqLoginName();
            trm.content.user_id = userid;
            string msg = JsonConvert.SerializeObject(trm);
            _scoketManager.SendTradeWSInfo(msg);
        }
    }
}
