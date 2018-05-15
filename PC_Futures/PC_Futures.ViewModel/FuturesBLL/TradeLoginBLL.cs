using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures.FuturesBLL
{
    public class TradeLoginBLL
    {
        private ScoketManager scoketManager;
        public TradeLoginBLL()
        {
            scoketManager = ScoketManager.GetInstance();
        }

        public void SendLoginInfo(string username, string pwd, string mac, string ip)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string token = MD5Helper.EncryptWithMD5(time);
            string msg = "{\"cmdcode\":2235,\"content\":{\"login_name\":\"" + username + "\",\"password\":\"" + pwd + "\",\"resource\":0,\"mac_address\":\"" + mac + "\",\"ip_address\":\"" + ip + "\",\"time\":\""+time+ "\",\"token\":\"" + time + "\"}}";
            scoketManager.SendTradeWSInfo(msg);
        }
    }
}
