using MicroMvvm;
using System.Windows.Media;

namespace Utilities
{
    public class ServerStatusInfoHelper : ObservableObject
    {
        private static ServerStatusInfoHelper _instance;
        public static ServerStatusInfoHelper Instace()
        {
            if (_instance == null)
            {
                _instance = new ServerStatusInfoHelper();
            }
            return _instance;
        }
        private bool _isReattached;
        /// <summary>
        /// 行情是否重连成功
        /// </summary>
        public bool IsReattached
        {
            get
            {
                return _isReattached;
            }
            set
            {
                if (_isReattached == value)
                    return;
                _isReattached = value;
                RaisePropertyChanged(nameof(IsReattached));
            }
        }
        private string _tradeServerStatus;
        public string TradeServerStatus
        {
            get { return _tradeServerStatus; }
            set
            {
                if (_tradeServerStatus == value)
                    return;
                _tradeServerStatus = value;
                RaisePropertyChanged(nameof(TradeServerStatus));
                RaisePropertyChanged(nameof(TradeForeground));
            }
        }
        public SolidColorBrush TradeForeground
        {
            get
            {
                if (string.Equals(TradeServerStatus, "已连接"))
                {
                    return Brushes.Green;
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }
        private string _quotesServerStatus;
        public string QuotesServerStatus
        {
            get { return _quotesServerStatus; }
            set
            {
                if (_quotesServerStatus == value)
                    return;
                _quotesServerStatus = value;
                RaisePropertyChanged(nameof(QuotesServerStatus));
                RaisePropertyChanged(nameof(QuotesForeground));
            }
        }
        public SolidColorBrush QuotesForeground
        {
            get
            {
                if (string.Equals(QuotesServerStatus, "已连接"))
                {
                    return Brushes.Green;
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }
    }
}
