
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Utilities;

namespace PC_Futures.FuturesBLL
{
    public class ContractBLL
    {
        private ScoketManager scoketManager;
        public ContractBLL()
        {
            scoketManager = ScoketManager.GetInstance();
        }
        public void AddOptional(string contractid)
        {
            if (string.IsNullOrEmpty(UserInfoHelper.UserId))
            {
                MessageBox.Show("登录之后才能加入自选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string msg = "{\"cmdcode\":2246,\"content\":{\"user_id\":\"" + UserInfoHelper.UserId + "\",\"contract_id\":\"" + contractid + "\"}}";
            scoketManager.SendTradeWSInfo(msg);
        }
        public void DelOptional(string serialnumber)
        {
            if (string.IsNullOrEmpty(serialnumber))
                return;
            string msg = "{\"cmdcode\":2248,\"content\":{\"serial_number\":\"" + serialnumber + "\"}}";
            scoketManager.SendTradeWSInfo(msg);
        }
    }
}
