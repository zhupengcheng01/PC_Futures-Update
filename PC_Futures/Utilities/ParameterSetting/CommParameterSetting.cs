using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class CommParameterSetting
    {
        /// <summary>
        /// 止盈止损设置
        /// </summary>
        public static StopLossModel StopLossModel = new StopLossModel();

        /// <summary>
        /// 自动止盈止损设置
        /// </summary>
        public static List<AutoStopLossModel> AutoStopLossModel = new List<AutoStopLossModel>();

        /// <summary>
        /// 下单面板设置
        /// </summary>
        public static OrderVersionModel OrderVersion = new OrderVersionModel();

        /// <summary>
        /// 快捷键设置
        /// </summary>
        public static ShortcutKeyModel ShortcutKey = new ShortcutKeyModel();

        /// <summary>
        /// 消息提示
        /// </summary>
        public static MessageAlert MessageAlert = new MessageAlert();

   



    }
}
