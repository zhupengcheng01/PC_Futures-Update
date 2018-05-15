using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utilities;

namespace PC_Futures
{
    public class FuturesTickViewModel : ObservableObject
    {
        private readonly string _upBrush = "Red";
        private readonly string _downBrush = "#00ff00";
        private HttpRequestContractHelper httpHelper = new HttpRequestContractHelper();
        private ObservableCollection<EachDealViewModel> _EachDealList = new ObservableCollection<EachDealViewModel>();
        private FuturesViewModel _futuresViewModel;
        public FuturesTickViewModel(FuturesViewModel futuresViewModel)
        {
            _futuresViewModel = futuresViewModel;
        }
        public void SetEachDealList()
        {
            if (IsRequestEachDeal) return;
            IsRequestEachDeal = true;
            Func<List<EachDealHistoryModel>> EachDealHandle = EachDealHandleAction;
            IAsyncResult iar = EachDealHandle.BeginInvoke(EachDealHandleBack, EachDealHandle);
        }
        private List<EachDealHistoryModel> EachDealHandleAction()
        {
            return httpHelper.GetEachDealHistory(_futuresViewModel.ContractCode, _futuresViewModel.ProductCode, TradeInfoHelper.EachDealCount).OrderBy(o => (string.IsNullOrEmpty(o.time) ? new DateTime() : Convert.ToDateTime(o.time))).ToList();
        }
        private void EachDealHandleBack(IAsyncResult iar)
        {
            if (iar == null) return;
            Func<List<EachDealHistoryModel>> EachDealHandle = (Func<List<EachDealHistoryModel>>)iar.AsyncState;
            List<EachDealHistoryModel> list = EachDealHandle.EndInvoke(iar);
            foreach (var item in list)
            {
                EachDealViewModel eachViewModel = new EachDealViewModel();
                eachViewModel.EachDealTime = string.IsNullOrEmpty(item.time) ? "" : Convert.ToDateTime(item.time).ToString("HH:mm:ss");
                eachViewModel.EachDealPrice = item.lastPrice;
                eachViewModel.EachDealColor = item.lastPrice >= PreClosePrice ? "Red" : "#00ff00";
                var varietyModel = TradeInfoHelper.VarietyModelList.FirstOrDefault(o => string.Equals(o.productCode, _futuresViewModel.ProductCode));
                if (varietyModel != null)
                {
                    int precy = varietyModel.precy ?? 2;
                    eachViewModel.EachDealKeepDigits = "F" + precy;
                }
                eachViewModel.EachDealSize = item.lastSize;
                eachViewModel.EachDealSizeColor = item.lastPrice <= BidP1 ? "#00ff00" : "Red";
                eachViewModel.OrderByTime = string.IsNullOrEmpty(item.time) ? new DateTime() : Convert.ToDateTime(item.time);
                if (Application.Current != null) //判断不为空
                {
                    Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(
                    () =>
                    {
                        _EachDealList.Add(eachViewModel);
                    }));
                }
            }
            IsRequestEachDeal = false;
        }
        private double _askP2 = double.NaN;
        public FuturesViewModel FuturesViewModel
        {
            get
            {
                return _futuresViewModel;
            }
        }
        public bool IsRequestEachDeal { get; set; }
        /// <summary>
        /// 卖二价
        /// </summary>
        public double AskP2
        {
            get { return _askP2; }
            set
            {
                if (_askP2.Equals(value))
                    return;
                _askP2 = value;
                RaisePropertyChanged(nameof(AskP2));
                RaisePropertyChanged(nameof(AskP2Color));
            }
        }
        private double _askP3 = double.NaN;
        /// <summary>
        /// 卖三价
        /// </summary>
        public double AskP3
        {
            get { return _askP3; }
            set
            {
                if (_askP3.Equals(value))
                    return;
                _askP3 = value;
                RaisePropertyChanged(nameof(AskP3));
                RaisePropertyChanged(nameof(AskP3Color));
            }
        }
        private double _askP4 = double.NaN;
        /// <summary>
        /// 卖四价
        /// </summary>
        public double AskP4
        {
            get { return _askP4; }
            set
            {
                if (_askP4.Equals(value))
                    return;
                _askP4 = value;
                RaisePropertyChanged(nameof(AskP4));
                RaisePropertyChanged(nameof(AskP4Color));
            }
        }
        private double _askP5 = double.NaN;
        /// <summary>
        /// 卖五价
        /// </summary>
        public double AskP5
        {
            get { return _askP5; }
            set
            {
                if (_askP5.Equals(value))
                    return;
                _askP5 = value;
                RaisePropertyChanged(nameof(AskP5));
                RaisePropertyChanged(nameof(AskP5Color));
            }
        }

        private double _bidP2 = double.NaN;
        /// <summary>
        /// 买二价
        /// </summary>
        public double BidP2
        {
            get { return _bidP2; }
            set
            {
                if (_bidP2.Equals(value))
                    return;
                _bidP2 = value;
                RaisePropertyChanged(nameof(BidP2));
                RaisePropertyChanged(nameof(BidP2Color));
            }
        }
        private double _bidP3 = double.NaN;
        /// <summary>
        /// 买三价
        /// </summary>
        public double BidP3
        {
            get { return _bidP3; }
            set
            {
                if (_bidP3.Equals(value))
                    return;
                _bidP3 = value;
                RaisePropertyChanged(nameof(BidP3));
                RaisePropertyChanged(nameof(BidP3Color));
            }
        }
        private double _bidP4 = double.NaN;
        /// <summary>
        /// 买四价
        /// </summary>
        public double BidP4
        {
            get { return _bidP4; }
            set
            {
                if (_bidP4.Equals(value))
                    return;
                _bidP4 = value;
                RaisePropertyChanged(nameof(BidP4));
                RaisePropertyChanged(nameof(BidP4Color));
            }
        }
        private double _bidP5 = double.NaN;
        /// <summary>
        /// 买五价
        /// </summary>
        public double BidP5
        {
            get { return _bidP5; }
            set
            {
                if (_bidP5.Equals(value))
                    return;
                _bidP5 = value;
                RaisePropertyChanged(nameof(BidP5));
         RaisePropertyChanged(nameof(BidP5Color));
            }
        }

        private double _askV2 = double.NaN;
        /// <summary>
        /// 卖二量
        /// </summary>
        public double AskV2
        {
            get { return _askV2; }
            set
            {
                if (_askV2.Equals(value))
                    return;
                _askV2 = value;
                RaisePropertyChanged(nameof(AskV2));
            }
        }
        private double _askV3 = double.NaN;
        /// <summary>
        /// 卖三量
        /// </summary>
        public double AskV3
        {
            get { return _askV3; }
            set
            {
                if (_askV3.Equals(value))
                    return;
                _askV3 = value;
                RaisePropertyChanged(nameof(AskV3));
            }
        }
        private double _askV4 = double.NaN;
        /// <summary>
        /// 卖四量
        /// </summary>
        public double AskV4
        {
            get { return _askV4; }
            set
            {
                if (_askV4.Equals(value))
                    return;
                _askV4 = value;
                RaisePropertyChanged(nameof(AskV4));
            }
        }
        private double _askV5 = double.NaN;
        /// <summary>
        /// 卖五量
        /// </summary>
        public double AskV5
        {
            get { return _askV5; }
            set
            {
                if (_askV5.Equals(value))
                    return;
                _askV5 = value;
                RaisePropertyChanged(nameof(AskV5));
            }
        }

        private double _bidV2 = double.NaN;
        /// <summary>
        /// 买二量
        /// </summary>
        public double BidV2
        {
            get { return _bidV2; }
            set
            {
                if (_bidV2.Equals(value))
                    return;
                _bidV2 = value;
                RaisePropertyChanged(nameof(BidV2));
            }
        }
        private double _bidV3 = double.NaN;
        /// <summary>
        /// 买三量
        /// </summary>
        public double BidV3
        {
            get { return _bidV3; }
            set
            {
                if (_bidV3.Equals(value))
                    return;
                _bidV3 = value;
                RaisePropertyChanged(nameof(BidV3));
            }
        }
        private double _bidV4 = double.NaN;
        /// <summary>
        /// 买四量
        /// </summary>
        public double BidV4
        {
            get { return _bidV4; }
            set
            {
                if (_bidV4.Equals(value))
                    return;
                _bidV4 = value;
                RaisePropertyChanged(nameof(BidV4));
            }
        }
        private double _bidV5 = double.NaN;
        /// <summary>
        /// 买五量
        /// </summary>
        public double BidV5
        {
            get { return _bidV5; }
            set
            {
                if (_bidV5.Equals(value))
                    return;
                _bidV5 = value;
                RaisePropertyChanged(nameof(BidV5));
            }
        }

        private double _lastPrice = double.NaN;
        /// <summary>
        /// 最新价
        /// </summary>
        public double LastPrice
        {
            get
            {
                return _lastPrice;
            }
            set
            {
                //double T = _lastPrice;
                if (_lastPrice.Equals(value))
                {
                    return;
                }
                _lastPrice = value;
                RaisePropertyChanged(nameof(LastPrice));
                RaisePropertyChanged(nameof(Color));
                RaisePropertyChanged(nameof(Change));
                RaisePropertyChanged(nameof(ChangePct));
                RaisePropertyChanged(nameof(LastPriceColor));
                RaisePropertyChanged(nameof(ChangePctColor));
                RaisePropertyChanged(nameof(ChangeColor));
                RaisePropertyChanged(nameof(LastSizeColor));
                //TradeQuotesViewModel.GetInstance(null).CuotesTabControlViewModel.SxKChar(LastPrice);

            }
        }
        private double _lastUpPrice = double.NaN;
        /// <summary>
        /// 涨停价
        /// </summary>
        public double LastUpPrice
        {
            get
            {
                return _lastUpPrice;
            }
            set
            {
                if (_lastUpPrice.Equals(value))
                {
                    return;
                }
                _lastUpPrice = value;
                RaisePropertyChanged(nameof(LastUpPrice));
            }
        }
        private double _lastDownPrice = double.NaN;
        /// <summary>
        /// 跌停价
        /// </summary>
        public double LastDownPrice
        {
            get
            {
                return _lastDownPrice;
            }
            set
            {
                if (_lastDownPrice.Equals(value))
                {
                    return;
                }
                _lastDownPrice = value;
                RaisePropertyChanged(nameof(LastDownPrice));
            }
        }

        /// <summary>
        /// 涨跌
        /// </summary>
        public double Change
        {

            get
            {
                return LastPrice-PreClosePrice;
            }
        }
        /// <summary>
        /// 幅度
        /// </summary>
        public double ChangePct
        {

            get
            {
                
                if(double.IsNaN(PreClosePrice) || PreClosePrice==0)
                {
                    return double.NaN;
                }
                else
                {
                    return (Change / PreClosePrice)*100;
                }
            }
        }

        private double _bidP1 = double.NaN;
        /// <summary>
        /// 买一价
        /// </summary>
        public double BidP1
        {
            get { return _bidP1; }
            set
            {
                if (_bidP1.Equals(value))
                    return;
                _bidP1 = value;
                RaisePropertyChanged(nameof(BidP1));
                RaisePropertyChanged(nameof(BidP1Color));
                RaisePropertyChanged(nameof(LastSizeColor));
            }
        }
        public string LastSizeColor
        {
            get
            {
                if(LastPrice<=BidP1)
                {
                    return _downBrush;
                }
                else
                {
                    return _upBrush;
                }
            }
        }
        private double _bidV1 = double.NaN;
        /// <summary>
        /// 买一量
        /// </summary>
        public double BidV1
        {
            get { return _bidV1; }
            set
            {
                if (_bidV1.Equals(value))
                    return;
                _bidV1 = value;
                RaisePropertyChanged(nameof(BidV1));
            }
        }
        private double _askP1 = double.NaN;
        /// <summary>
        /// 卖一价
        /// </summary>
        public double AskP1
        {
            get { return _askP1; }
            set
            {
                if (_askP1.Equals(value))
                    return;
                _askP1 = value;
                RaisePropertyChanged(nameof(AskP1));
    RaisePropertyChanged(nameof(AskP1Color));
            }
        }
        private double _askV1 = double.NaN;
        /// <summary>
        /// 卖一量
        /// </summary>
        public double AskV1
        {
            get { return _askV1; }
            set
            {
                if (_askV1.Equals(value))
                    return;
                _askV1 = value;
                RaisePropertyChanged(nameof(AskV1));
            }
        }
        private double _LastSize = double.NaN;
        /// <summary>
        /// 最新价成交量
        /// </summary>
        public double LastSize
        {
            get { return _LastSize; }
            set
            {
                if (_LastSize.Equals(value))
                    return;
                _LastSize = value;
                RaisePropertyChanged(nameof(LastSize));
                Action RefreshHandel = kChartAction;
                RefreshHandel.BeginInvoke(null, RefreshHandel);

            }
        }
        //需要异步根据最新成交量画K图
        private void kChartAction()
        {
            if (TradeQuotesViewModel.GetInstance(null).CuotesTabControlViewModel != null)
            {
                if (TradeQuotesViewModel.GetInstance(null).CuotesTabControlViewModel.tempTime != 0 || TradeQuotesViewModel.GetInstance(null).IsFifteenMinKLineCheck || TradeQuotesViewModel.GetInstance(null).IsFiveMinKLineCheck
                    || TradeQuotesViewModel.GetInstance(null).IsOneMinKLineCheck || TradeQuotesViewModel.GetInstance(null).IsSixtyMinKLineCheck
                    || TradeQuotesViewModel.GetInstance(null).IsThirtyMinKLineCheck || TradeQuotesViewModel.GetInstance(null).IsThreeMinKLineCheck)
                {
                    TradeQuotesViewModel.GetInstance(null).CuotesTabControlViewModel.SxKChar();
                }
            }
      
             if (TradeQuotesViewModel.GetInstance(null).IsRealTimeCheck == true)
                {
                    TradeQuotesViewModel.GetInstance(null).RealTimeChart.SxFsChar();
                }
            

        }

        private double _highPrice = double.NaN;
        /// <summary>
        /// 最高价
        /// </summary>
        public double HighPrice
        {
            get
            {
                return this._highPrice;
            }
            set
            {
                if (_highPrice.Equals(value))
                    return;
                this._highPrice = value;
                RaisePropertyChanged(nameof(HighPrice));
                RaisePropertyChanged(nameof(HighPriceColor));
            }
        }

        private double _lowPrice = double.NaN;
        /// <summary>
        /// 最低价
        /// </summary>
        public double LowPrice
        {
            get
            {
                return this._lowPrice;
            }
            set
            {
                if (this._lowPrice.Equals(value))
                    return;
                this._lowPrice = value;
                RaisePropertyChanged(nameof(LowPrice));
                RaisePropertyChanged(nameof(LowPriceColor));
            }
        }
        private double _openPrice = double.NaN;
        /// <summary>
        /// 开盘价
        /// </summary>
        public double OpenPrice
        {
            get
            {
                return this._openPrice;
            }
            set
            {
                if (_openPrice.Equals(value))
                    return;
                this._openPrice = value;
                RaisePropertyChanged(nameof(OpenPrice));
                RaisePropertyChanged(nameof(OpenPriceColor));
            }
        }
        private double _preClosePrice = double.NaN;
        /// <summary>
        /// 昨日收盘价
        /// </summary>
        public double PreClosePrice
        {
            get
            {
                return this._preClosePrice;
            }
            set
            {
                if (_preClosePrice.Equals(value))
                    return;
                this._preClosePrice = value;
                RaisePropertyChanged(nameof(PreClosePrice));
                RaisePropertyChanged(nameof(Color));
                RaisePropertyChanged(nameof(Change));
                RaisePropertyChanged(nameof(ChangePct));
                RaisePropertyChanged(nameof(ChangePctColor));
                RaisePropertyChanged(nameof(ChangeColor));
            }
        }
        private double _preSettlePrice = double.NaN;
        /// <summary>
        /// 昨日结算价
        /// </summary>
        public double PreSettlePrice
        {
            get
            {
                return this._preSettlePrice;
            }
            set
            {
                if (_preSettlePrice.Equals(value))
                    return;
                this._preSettlePrice = value;
                RaisePropertyChanged(nameof(PreSettlePrice));
            }
        }

        private double _totalSize = double.NaN;
        /// <summary>
        /// 成交量
        /// </summary>
        public double TotalSize
        {
            get { return _totalSize; }
            set
            {
                if (_totalSize.Equals(value))
                    return;
                _totalSize = value;
                RaisePropertyChanged(nameof(TotalSize));
            }
        }
        private double _positionSize = double.NaN;
        /// <summary>
        /// 持仓量
        /// </summary>
        public double PositionSize
        {
            get { return _positionSize; }
            set
            {
                if (_positionSize.Equals(value))
                    return;
                _positionSize = value;
                RaisePropertyChanged(nameof(PositionSize));
            }
        }
        private int _precy = 0;
        /// <summary>
        /// 有效位数
        /// </summary>
        public int Precy
        {
            get
            {
                return this._precy;
            }
            set
            {
                if (_precy.Equals(value))
                    return;
                this._precy = value;
                RaisePropertyChanged(nameof(Precy));
                RaisePropertyChanged(nameof(KeepDigits));
            }
        }
        /// <summary>
        /// 小数位数格式化
        /// </summary>
        public string KeepDigits
        {
            get
            {
                return "F" + Precy;
            }
        }
        private string _Time = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        public string Time
        {
            get
            {
                return _Time;
            }
            set
            {
                if (_Time.Equals(value))
                    return;
                _Time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }
        private string _quotesTime=string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        public string QuotesTime
        {
            get
            {
                return _quotesTime;
            }
            set
            {
                if (_quotesTime.Equals(value))
                    return;
                _quotesTime = value;
                RaisePropertyChanged(nameof(QuotesTime));
            }
        }
       

        public string Color
        {
            get
            {
                if (LastPrice >= PreClosePrice)
                {
                    return _upBrush;
                }
                else
                {
                    return _downBrush;
                }
            }
        }
 
        #region 五档颜色
        private readonly string _downBrushTemp = "#229022";
        private readonly string _defaultBrush = "White";
   
        private readonly string _defaultBlackBrush = "Black";
        public string AskP1Color
        {
            get
            {
                return GetColorW(AskP1);
            }
        }

     
        public string AskP2Color
        {
            get
            {
                return GetColorW(AskP2);
            }
        }

  
        public string AskP3Color
        {
            get
            {
                return GetColorW(AskP3);
            }
        }
 
        public string AskP4Color
        {
            get
            {
                return GetColorW(AskP4);
            }
        }
        public string AskP5Color
        {
            get
            {
                return GetColorW(AskP5);
            }
        }
        public string BidP5Color
        {
            get
            {
                return GetColorW(BidP5);
            }
        }
        public string BidP4Color
        {
            get
            {
                return GetColorW(BidP4);
            }
        }
        public string BidP3Color
        {
            get
            {
                return GetColorW(BidP3);
            }
        }
        public string BidP2Color
        {
            get
            {
                return GetColorW(BidP2);
            }
        }
        public string BidP1Color
        {
            get
            {
                return GetColorW(BidP1);
            }
        }

         public string LastPriceColor
        {
            get
            {
                return GetColorW(LastPrice);
            }
        }
        public string ChangePctColor
        {
            get
            {
                return GetColorZ(ChangePct);
            }
        }
      
        public string OpenPriceColor
        {
            get
            {
                return GetColorW(OpenPrice);
            }
        }
        public string HighPriceColor
        {
            get
            {
                return GetColorW(HighPrice);
            }
        }
        public string LowPriceColor
        {
            get
            {
                return GetColorW(LowPrice);
            }
        }
        public string ChangeColor
        {
            get
            {
                return GetColorZ(Change);
            }
        }

        private string GetColorW(double price)
        {
            //if (Math.Abs(price - PreClosePrice) < 0.0000001)
            //{
            //    return _defaultBrush;
            //}
            if (price > PreClosePrice)
            {
                return _upBrush;
            }
            else if(price == PreClosePrice)
            {
            return _defaultBrush;
            }
            else
            {
                return _downBrush;
            }
        }

        private string GetColorZ(double price)
        {
            //if (Math.Abs(price - PreClosePrice) < 0.0000001)
            //{
            //    return _defaultBrush;
            //}
            if (price > 0)
            {
                return _upBrush;
            }
            else if (price == 0)
            {
                return _defaultBrush;
            }
            else
            {
                return _downBrush;
            }
        }


        #endregion


        public ObservableCollection<EachDealViewModel> EachDealList
        {
            get
            {
                return _EachDealList;
            }
        }

        /// <summary>
        /// 逐笔历史查询
        /// </summary>
        public ICommand EachDealHistoryCommand { get { return new RelayCommand(EachDealHistoryExecuteChanged, EachDealHistoryCanExecuteChanged); } }
        public void EachDealHistoryExecuteChanged()
        {
            EachDealHistoryWindowViewModel viewModel = new EachDealHistoryWindowViewModel(this);
            EachDealHistoryWindow dealWin = new EachDealHistoryWindow();
            dealWin.DataContext = viewModel;
            dealWin.ShowDialog();
        }
        public bool EachDealHistoryCanExecuteChanged()
        {
            return true;
        }
    }
}
