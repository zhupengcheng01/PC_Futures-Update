using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.CommonClass;
using PC_Futures.CommonClass.ParameterSetting;
using PC_Futures.Models;
using PC_Futures.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PC_Futures
{
    public class TransactionViewModel : ObservableObject
    {
        #region 变量

        #region 闪电交易
        private bool _IsByClose = true;
        private bool _IsSellClose = false;
        private bool _IsAuto = false;
        public bool IsByClose
        {
            get { return _IsByClose; }
            set
            {
                if (value != _IsByClose)
                {
                    _IsByClose = value;
                    RaisePropertyChanged("IsByClose");
                }
            }
        }
        public bool IsSellClose
        {
            get { return _IsSellClose; }
            set
            {
                if (value != _IsSellClose)
                {
                    _IsSellClose = value;
                    RaisePropertyChanged("IsSellClose");
                }
            }
        }
        public bool IsAuto
        {
            get { return _IsAuto; }
            set
            {
                if (value != _IsAuto)
                {
                    _IsAuto = value;
                    RaisePropertyChanged("IsAuto");
                }
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        private List<string> _PriceType = new List<string>();
        public List<string> PriceType
        {
            get { return _PriceType; }
            set
            {
                if (value != _PriceType)
                {
                    _PriceType = value;
                    RaisePropertyChanged("PriceType");
                }
            }
        }

        private string _PriceTypeSelectedItem;
        public string PriceTypeSelectedItem
        {
            get { return _PriceTypeSelectedItem; }
            set
            {
                if (value != _PriceTypeSelectedItem)
                {
                    _PriceTypeSelectedItem = value;
                    ShowPrice = _PriceTypeSelectedItem;
                    if (PriceTypeSelectedItem == "限价")
                    {
                        UCTransaction.cd1();
                    }
                    RaisePropertyChanged("PriceTypeSelectedItem");
                }
            }
        }
        /// <summary>
        /// 品种
        /// </summary>
        private List<string> _Variety = new List<string>();
        public List<string> Variety
        {
            get { return _Variety; }
            set
            {
                if (value != _Variety)
                {
                    _Variety = value;
                    RaisePropertyChanged("Variety");
                }
            }
        }
        private string _VarietySelectedItem;

        public string VarietySelectedItem
        {
            get { return _VarietySelectedItem; }
            set
            {
                if (value != _VarietySelectedItem)
                {
                    _VarietySelectedItem = value;
                    RaisePropertyChanged("VarietySelectedItem");
                }
            }
        }

        private List<SysCodeModel> _VarietyCode = new List<SysCodeModel>();
        public List<SysCodeModel> VarietyCode
        {
            get { return _VarietyCode; }
            set
            {
                if (value != _VarietyCode)
                {
                    _VarietyCode = value;
                    RaisePropertyChanged("VarietyCode");
                }
            }
        }


        /// <summary>
        /// 合约号
        /// </summary>
        private List<SysCodeModel> _ContractCode = new List<SysCodeModel>();
        public List<SysCodeModel> ContractCode
        {
            get { return _ContractCode; }
            set
            {
                if (value != _ContractCode)
                {
                    _ContractCode = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        private SysCodeModel _ContractCodeSelectedItem;
        public SysCodeModel ContractCodeSelectedItem
        {
            get { return _ContractCodeSelectedItem; }
            set
            {
                if (value != _ContractCodeSelectedItem)
                {
                    _ContractCodeSelectedItem = value;
                    RaisePropertyChanged("ContractCodeSelectedItem");
                }
            }
        }

        public string dic = null;

        private double _FTBuyOPen;
        /// <summary>
        /// 买可开
        /// </summary>
        public double FTBuyOPen
        {
            get { return _FTBuyOPen; }
            set
            {
                if (value != _FTBuyOPen)
                {
                    _FTBuyOPen = value;
                    RaisePropertyChanged("FTBuyOPen");
                }
            }
        }
        private double _FTSellOPen;
        /// <summary>
        /// 卖可开
        /// </summary>
        public double FTSellOPen
        {
            get { return _FTSellOPen; }
            set
            {
                if (value != _FTSellOPen)
                {
                    _FTSellOPen = value;
                    RaisePropertyChanged("FTSellOPen");
                }
            }
        }
        private double _FTBuyClose;
        /// <summary>
        /// 买可平
        /// </summary>
        public double FTBuyClose
        {
            get { return _FTBuyClose; }
            set
            {
                if (value != _FTBuyClose)
                {
                    _FTBuyClose = value;
                    RaisePropertyChanged("FTBuyClose");
                }
            }
        }
        private double _FTSellClose;
        /// <summary>
        /// 卖可开
        /// </summary>
        public double FTSellClose
        {
            get { return _FTSellClose; }
            set
            {
                if (value != _FTSellClose)
                {
                    _FTSellClose = value;
                    RaisePropertyChanged("FTSellClose");
                }
            }
        }

        private int _MaxLot = 999999999;
        public int MaxLot
        {
            get { return _MaxLot; }
            set
            {
                if (value != _MaxLot)
                {
                    _MaxLot = value;
                    RaisePropertyChanged("MaxLot");
                }
            }
        }

        private int _FlashTradingNum = 1;
        /// <summary>
        /// 数量
        /// </summary>
        public int FTNum
        {
            get { return _FlashTradingNum; }
            set
            {
                if (value != _FlashTradingNum)
                {
                    _FlashTradingNum = value;
                    RaisePropertyChanged("FTNum");
                }
            }
        }
        private double _FTPrice;
        /// <summary>
        /// 价格
        /// </summary>
        public double FTPrice
        {
            get { return _FTPrice; }
            set
            {
                if (value != _FTPrice)
                {
                    _FTPrice = Math.Round(value, precision);
                    RaisePropertyChanged("FTPrice");
                }
            }
        }

        private double _FTSellPrice;
        /// <summary>
        /// 价格
        /// </summary>
        public double FTSellPrice
        {
            get { return _FTSellPrice; }
            set
            {
                if (value != _FTSellPrice)
                {
                    _FTSellPrice = Math.Round(value, precision);
                    RaisePropertyChanged("FTSellPrice");
                }
            }
        }
        private double _SJClosePrice;
        /// <summary>
        /// 价格
        /// </summary>
        public double SJClosePrice
        {
            get { return _SJClosePrice; }
            set
            {
                if (value != _SJClosePrice)
                {
                    _SJClosePrice = Math.Round(value, precision);
                    RaisePropertyChanged("SJClosePrice");
                }
            }
        }
        private string _ShowPrice;
        public string ShowPrice
        {
            get { return _ShowPrice; }
            set
            {
                if (value != _ShowPrice)
                {
                    _ShowPrice = value;
                    double tt = 0;
                    if (double.TryParse(value, out tt))
                    {
                        _FTChangePrice = Math.Round(tt, precision);
                        FTSellPrice = Math.Round(tt, precision);
                        FTPrice = Math.Round(tt, precision);
                        SJClosePrice = Math.Round(tt, precision);
                    }
                    RaisePropertyChanged("ShowPrice");
                }
            }
        }

        private double _FTChangePrice;
        /// <summary>
        /// 价格
        /// </summary>
        public double FTChangePrice
        {
            get { return _FTChangePrice; }
            set
            {
                if (PriceTypeSelectedItem == "市价") return;

                if (value != _FTChangePrice)
                {
                    PriceTypeSelectedItem = "限价";
                    _FTChangePrice = Math.Round(value, precision);
                    FTSellPrice = Math.Round(value, precision);
                    FTPrice = Math.Round(value, precision);
                    SJClosePrice = Math.Round(value, precision);
                    RaisePropertyChanged("FTChangePrice");
                    ShowPrice = _FTChangePrice.ToString();
                }
                else
                {
                    if (PriceTypeSelectedItem == "限价")
                        ShowPrice = _FTChangePrice.ToString();
                }
            }
        }

        private Visibility _IsShow = Visibility.Visible;
        public Visibility IsShow
        {
            get { return _IsShow; }
            set
            {
                if (value != _IsShow)
                {
                    _IsShow = value;
                    RaisePropertyChanged("IsShow");
                }
            }
        }

        private Visibility _IsBuyNum = Visibility.Visible;
        public Visibility IsBuyNum
        {
            get { return _IsBuyNum; }
            set
            {
                if (value != _IsBuyNum)
                {
                    _IsBuyNum = value;
                    RaisePropertyChanged("IsBuyNum");
                }
            }
        }
        private Visibility _IsSellNum = Visibility.Collapsed;
        public Visibility IsSellNum
        {
            get { return _IsSellNum; }
            set
            {
                if (value != _IsSellNum)
                {
                    _IsSellNum = value;
                    RaisePropertyChanged("IsSellNum");
                }
            }
        }
        private double _Increment = 1;
        public double Increment
        {
            get { return _Increment; }
            set
            {
                if (value != _Increment)
                {
                    _Increment = value;
                    RaisePropertyChanged("Increment");
                }
            }
        }

        private bool _CTIsBuy = true;
        private bool _CTIsSell = false;
        public bool CTIsBuy
        {
            get { return _CTIsBuy; }
            set
            {
                if (value != _CTIsBuy)
                {
                    _CTIsBuy = value;
                    RaisePropertyChanged("CTIsBuy");
                }
            }
        }
        public bool CTIsSell
        {
            get { return _CTIsSell; }
            set
            {
                if (value != _CTIsSell)
                {
                    _CTIsSell = value;
                    RaisePropertyChanged("CTIsSell");
                }
            }
        }
        private bool _IsEnabled = false;
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (value != _IsEnabled)
                {
                    _IsEnabled = value;
                    RaisePropertyChanged("IsEnabled");
                }
            }
        }

        public FuturesViewModel Futures = null;

        public int precision = 0;
        #endregion

        #endregion

        #region 构造函数
        private static TransactionViewModel _TransactionViewModel;
        public static TransactionViewModel Instance()
        {
            if (_TransactionViewModel == null)
            {
                _TransactionViewModel = new TransactionViewModel();
            }
            return _TransactionViewModel;
        }

        private TransactionViewModel()
        {
            string priceType = IniHelper.ProfileReadValue("price", "priceType", IniHelper.configpath);
            string[] prices = priceType.Split(',');
            foreach (var item in prices)
            {
                PriceType.Add(item);
            }
            PriceTypeSelectedItem = PriceType[0];
            if (PriceTypeSelectedItem == "限价")
            {
                IsShow = Visibility.Visible;
            }
            else
            {
                IsShow = Visibility.Hidden;
            }
            Variety = MainViewModel.GetInstance().VarietyList.Keys.ToList();
            if (CommParameterSetting.OrderVersion != null)
            {
                if (CommParameterSetting.OrderVersion.DefaultLot > 0)
                    FTNum = CommParameterSetting.OrderVersion.DefaultLot;
                if (CommParameterSetting.OrderVersion.MaxLot > 0)
                    MaxLot = CommParameterSetting.OrderVersion.MaxLot;
            }
        }
        #endregion

        #region 自定义方法

        public double FTChangePriceTemp { get; set; }
        /// <summary>
        /// 提供行情信息 显示要交易的期货行情的信息
        /// </summary>
        /// <param name="futures"></param>
        public void SetXamlValues(FuturesViewModel futures)
        {
            Futures = futures;
            isfrist = true;
            if (futures == null)
            {
                VarietySelectedItem = null;
                FTChangePriceTemp = 0;
                FTChangePrice = 0;
                ContractCodeSelectedItem = null;
                return;
            }

            if (double.IsNaN(futures.Tick.LastPrice))
            {
                FTChangePriceTemp = 0;
                FTChangePrice = 0;
            }
            else
            {
                FTChangePriceTemp = futures.Tick.LastPrice;
                FTChangePrice = futures.Tick.LastPrice;
            }
            if (PriceTypeSelectedItem != "限价")
            {
                FTSellPrice = futures.Tick.BidP1;
                FTPrice = futures.Tick.AskP1;
            }
            else
            {
                FTChangePriceTemp = futures.Tick.LastPrice;
                FTChangePrice = futures.Tick.LastPrice;
            }
            if (CommParameterSetting.OrderVersion != null)
            {
                if (CommParameterSetting.OrderVersion.DefaultLot > 0)
                    FTNum = CommParameterSetting.OrderVersion.DefaultLot;
                if (CommParameterSetting.OrderVersion.MaxLot > 0)
                    MaxLot = CommParameterSetting.OrderVersion.MaxLot;
            }
            //选中合约后计算查可开数据查可平的数据,第一次推送行情
            PriceTypeSelectedItem = "对手价";
            FigureUpNum(futures);

            List<PotionDetailModelViewModel> listpdmv = PositionViewModel.Instance().PMList.Where(x => x.ContractCode == futures.ContractCode).ToList();
            if (listpdmv == null || listpdmv.Count == 0 || listpdmv.Count > 1)
            {
                SJClosePrice = 0;
                IsEnabled = false;

            }
            else
            {
                if (listpdmv[0].Direction == "B")
                {
                    SJClosePrice = futures.Tick.BidP1;
                }
                else
                {
                    SJClosePrice = futures.Tick.AskP1;
                }
                IsEnabled = true;
                PositionViewModel.Instance().SelectedItem = listpdmv[0];
            }
            VarietySelectedItem = futures.ProductCode + " " + futures.ProductName;
            if (VarietySelectedItem != null)
            {
                ContractCode = null;
                ContractCode = MainViewModel.GetInstance().VarietyList[VarietySelectedItem].ToList();
            }
            ContractCodeSelectedItem = ContractCode.FirstOrDefault(o => o.SystemName == futures.ContractCode);
        }

        bool isfrist = true;
        public QuotationEntity _futures = null;
        /// <summary>
        /// 提供行情信息 显示要交易的期货行情的信息
        /// </summary>
        /// <param name="futures"></param>
        public void SendMarket(QuotationEntity futures)
        {
            if (futures == null) return;
            _futures = futures;
            if (PriceTypeSelectedItem != "限价")
            {
                FTPrice = futures.ap1;
                FTSellPrice = futures.bp1;
                if (dic == "B")
                {
                    SJClosePrice = futures.bp1;
                }
                else if (dic == "S")
                {
                    SJClosePrice = futures.ap1;
                }
            }
            FTChangePriceTemp = futures.lp;
            if (isfrist)
            {
                //FTChangePrice = FTChangePriceTemp;

            }
            List<PotionDetailModelViewModel> listpdmv = PositionViewModel.Instance().PMList.Where(x => x.ContractCode == futures.cd).ToList();
            if (listpdmv == null || listpdmv.Count == 0 || listpdmv.Count > 1 && (PositionViewModel.Instance().SelectedItem == null || PositionViewModel.Instance().SelectedItem.ContractCode != futures.cd))
            {
                SJClosePrice = 0;
            }
            else
            {
                if (PriceTypeSelectedItem != "限价")
                {
                    if (PositionViewModel.Instance().SelectedItem == null)
                    {
                        PositionViewModel.Instance().SelectedItem = listpdmv[0];
                    }
                    if (PositionViewModel.Instance().SelectedItem.Direction == "B")
                    {
                        SJClosePrice = futures.bp1;
                    }
                    else
                    {
                        SJClosePrice = futures.ap1;
                    }
                }

            }
        }

        /// <summary>
        ///计算查可开数据查可平的数据
        /// </summary>
        public void FigureUpNum(QuotationEntity futures)
        {
            PositionViewModel.Instance().JSAbleVolume();
            if (ContractCodeSelectedItem == null) return;
            if (DescriptViewModel.Instance().Funds == null || DescriptViewModel.Instance().Funds.Count == 0)
            {
                MessageBox.Show("未查询到账户的资金信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            };
            if (VarietySelectedItem == null)
            {
                MessageBox.Show("品种不能为空!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            RestTodayFundsViewModel tfvm = DescriptViewModel.Instance().Funds[0];
            string[] values = ContractCodeSelectedItem.SysVarietyCode.Split(' ');
            if (values.Length != 3)
            {
                return;
            }
            string varietie = values[1];
            VarietyModel vm = null;
            if (ContractVariety.Varieties.ContainsKey(varietie))
            {
                vm = ContractVariety.Varieties[varietie];
            }
            if (vm == null) return;
            MarginModel mm = ContractVariety.Margins.FirstOrDefault(x => x.product_id == vm.product_id);
            if (mm == null) return;
            ParitiesModel pm = null;
            if (ContractVariety.Parities.ContainsKey(vm.currency_code))
            {
                pm = ContractVariety.Parities[vm.currency_code];
            }
            if (pm == null) return;
            if (ContractVariety.Depostion.calc_deposit_model == (int)MarginCalcType.MCT_Ratio)
            {//比例
                if (futures == null) return;
                if (PriceTypeSelectedItem == "限价")
                {
                    FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.sell_value * FTChangePrice * pm.exchange_rate));
                    FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.buy_value * FTChangePrice * pm.exchange_rate));
                }
                else if (PriceTypeSelectedItem == "对手价")
                {
                    FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.sell_value * futures.bp1 * pm.exchange_rate));
                    FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.buy_value * futures.ap1 * pm.exchange_rate));
                }
                else
                {
                    FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.sell_value * futures.lp * pm.exchange_rate));
                    FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.buy_value * futures.lp * pm.exchange_rate));
                }

            }
            else if (ContractVariety.Depostion.calc_deposit_model == (int)MarginCalcType.MCT_Fixed)
            {//固定值
                FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) / (mm.sell_value * pm.exchange_rate));
                FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) / (mm.buy_value * pm.exchange_rate));
            }
            if (FTSellOPen < 0)
            {
                FTSellOPen = 0;
            }
            if (FTBuyOPen < 0)
            {
                FTBuyOPen = 0;
            }
            PotionDetailModelViewModel pdmvm = PositionAllViewModel.Instance().PMList.FirstOrDefault(x => x.ContractCode == ContractCodeSelectedItem.SysVarietyCode && x.Direction == "B");
            List<DelegationModelViewModel> pdmvm1 = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == ContractCodeSelectedItem.SysVarietyCode && x.Direction == "S" && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();


            PotionDetailModelViewModel Spdmvm = PositionAllViewModel.Instance().PMList.FirstOrDefault(x => x.ContractCode == ContractCodeSelectedItem.SysVarietyCode && x.Direction == "S");
            List<DelegationModelViewModel> Spdmvm1 = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractID == ContractCodeSelectedItem.SysVarietyCode && x.OpenOffset == (int)OffsetType.OFFSET_COVER && x.Direction == "B").ToList();

            if (pdmvm != null)
            {
                int cknum0 = 0;
                if (pdmvm1 != null && pdmvm1.Count > 0)
                {
                    cknum0 = pdmvm1.Sum(x => x.OrderVolume);
                }

                FTBuyClose = pdmvm.PositionVolume - cknum0;
            }
            else
            {
                FTBuyClose = 0;
            }
            if (Spdmvm != null)
            {
                int cknum0 = 0;
                if (Spdmvm1 != null && Spdmvm1.Count > 0)
                {
                    cknum0 = Spdmvm1.Sum(x => x.OrderVolume);
                }
                FTSellClose = Spdmvm.PositionVolume - cknum0;
            }
            else
            {
                FTSellClose = 0;
            }
            if (CTIsBuy)
            {
                IsBuyNum = Visibility.Collapsed;
                IsSellNum = Visibility.Visible;
            }
            else
            {
                IsBuyNum = Visibility.Visible;
                IsSellNum = Visibility.Collapsed;

            }
        }
        /// <summary>
        ///计算查可开数据查可平的数据
        /// </summary>
        private void FigureUpNum(FuturesViewModel futures)
        {
            PositionViewModel.Instance().JSAbleVolume();
            if (ContractCodeSelectedItem == null) return;
            if (DescriptViewModel.Instance().Funds == null || DescriptViewModel.Instance().Funds.Count == 0)
            {
                MessageBox.Show("未查询到账户的资金信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            };
            if (VarietySelectedItem == null)
            {
                MessageBox.Show("品种不能为空!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            RestTodayFundsViewModel tfvm = DescriptViewModel.Instance().Funds[0];
            string[] values = ContractCodeSelectedItem.SysVarietyCode.Split(' ');
            if (values.Length != 3)
            {
                return;
            }
            string varietie = values[1];
            VarietyModel vm = null;
            if (ContractVariety.Varieties.ContainsKey(varietie))
            {
                vm = ContractVariety.Varieties[varietie];
            }
            if (vm == null) return;
            MarginModel mm = ContractVariety.Margins.FirstOrDefault(x => x.product_id == vm.product_id);
            if (mm == null) return;
            ParitiesModel pm = null;
            if (ContractVariety.Parities.ContainsKey(vm.currency_code))
            {
                pm = ContractVariety.Parities[vm.currency_code];
            }
            if (pm == null) return;
            if (ContractVariety.Depostion.calc_deposit_model == (int)MarginCalcType.MCT_Ratio)
            {//比例
                if (futures == null) return;
                if (PriceTypeSelectedItem == "限价")
                {
                    FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.sell_value * FTChangePrice * pm.exchange_rate));
                    FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.buy_value * FTChangePrice * pm.exchange_rate));
                }
                else if (PriceTypeSelectedItem == "对手价")
                {
                    FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.sell_value * futures.Tick.BidP1 * pm.exchange_rate));
                    FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.buy_value * futures.Tick.AskP1 * pm.exchange_rate));
                }
                else
                {
                    FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.sell_value * futures.Tick.LastPrice * pm.exchange_rate));
                    FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) * 100 / (vm.multiple * mm.buy_value * futures.Tick.LastPrice * pm.exchange_rate));
                }

            }
            else if (ContractVariety.Depostion.calc_deposit_model == (int)MarginCalcType.MCT_Fixed)
            {//固定值
                FTSellOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) / (mm.sell_value * pm.exchange_rate));
                FTBuyOPen = (int)((tfvm.CurrentEquity - tfvm.UseMargin) / (mm.buy_value * pm.exchange_rate));
            }
            if (FTSellOPen < 0)
            {
                FTSellOPen = 0;
            }
            if (FTBuyOPen < 0)
            {
                FTBuyOPen = 0;
            }
            PotionDetailModelViewModel pdmvm = PositionAllViewModel.Instance().PMList.FirstOrDefault(x => x.ContractCode == futures.ContractCode && x.Direction == "B");
            List<DelegationModelViewModel> pdmvm1 = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == futures.ContractCode && x.Direction == "S" && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();

            PotionDetailModelViewModel Spdmvm = PositionAllViewModel.Instance().PMList.FirstOrDefault(x => x.ContractCode == futures.ContractCode && x.Direction == "S");
            List<DelegationModelViewModel> Spdmvm1 = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractID == futures.ContractCode && x.OpenOffset == (int)OffsetType.OFFSET_COVER && x.Direction == "B").ToList();

            if (pdmvm != null)
            {
                int cknum0 = 0;
                if (pdmvm1 != null && pdmvm1.Count > 0)
                {
                    cknum0 = pdmvm1.Sum(x => x.OrderVolume);
                }
                FTBuyClose = pdmvm.PositionVolume - cknum0;
            }
            else
            {
                FTBuyClose = 0;
            }
            if (Spdmvm != null)
            {
                int cknum0 = 0;
                if (Spdmvm1 != null && Spdmvm1.Count > 0)
                {
                    cknum0 = Spdmvm1.Sum(x => x.OrderVolume);
                }
                FTSellClose = Spdmvm.PositionVolume - cknum0;
            }
            else
            {
                FTSellClose = 0;
            }
            if (CTIsBuy)
            {
                IsBuyNum = Visibility.Collapsed;
                IsSellNum = Visibility.Visible;
            }
            else
            {
                IsBuyNum = Visibility.Visible;
                IsSellNum = Visibility.Collapsed;

            }
            if (FTSellOPen < 0)
            {
                FTSellOPen = 0;
            }
            if (FTBuyOPen < 0)
            {
                FTBuyOPen = 0;
            }
        }
        /// <summary>
        /// 从持仓、 委托、当日成交中选中一条合约，反向显示当前合约的相关信息
        /// </summary>
        /// <param name="ContractCode">合约号</param>
        public void SelectFutures(string contractCode, int num = 0)
        {
            //1从行情集合中查找相应的行情信息
            //  ContractCode = MainViewModel.GetInstance().VarietyList[VarietySelectedItem].ToList();
            //2.把当前的行情更新到界面
            if (string.IsNullOrEmpty(contractCode))
            {
                return;
            }
            string[] values = contractCode.Split(' ');
            if (values.Length != 3) return;
            VarietySelectedItem = Variety.FirstOrDefault(x => x.Contains(values[1]));
            if (VarietySelectedItem == null) return;
            ContractCode = MainViewModel.GetInstance().VarietyList[VarietySelectedItem].ToList();
            foreach (SysCodeModel item in ContractCode)
            {
                if (item.SystemName == contractCode)//判断字段不一定对
                {
                    ContractCodeSelectedItem = item;
                    if (item != null)
                    {
                        QuotesTabControlViewModel.GetInstance(null).ScrollIntoView(item);
                        ContractCodeSelectedItem = item;
                    }
                    break;
                }
            }
            if (num > 0)
            {
                FTNum = num;
                //FTBuyClose = num;
            }
        }



        #endregion

        #region 命令
        /// <summary>
        /// 选择品种
        /// </summary>
        public ICommand VarietyChangedCommand { get { return new RelayCommand(VarietyChangedExecuteChanged, VarietyChangedCanExecuteChanged); } }
        public void VarietyChangedExecuteChanged()
        {
            if (VarietySelectedItem != null)
            {
                ContractCode = MainViewModel.GetInstance().VarietyList[VarietySelectedItem].ToList();
            }
        }
        public bool VarietyChangedCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 切换tableItem
        /// </summary>
        public ICommand TableChange { get { return new RelayCommand(TableChangeExecuteChanged, TableChangeCanExecuteChanged); } }
        public void TableChangeExecuteChanged()
        {
            FTNum = 1;
            if (CommParameterSetting.OrderVersion != null)
            {
                if (CommParameterSetting.OrderVersion.DefaultLot > 0)
                    FTNum = CommParameterSetting.OrderVersion.DefaultLot;
                if (CommParameterSetting.OrderVersion.MaxLot > 0)
                    MaxLot = CommParameterSetting.OrderVersion.MaxLot;
            }
            FTChangePrice = FTChangePriceTemp;
            PriceTypeSelectedItem = PriceType[0];
            IsByClose = true;
            IsSellClose = false;
            CTIsBuy = true;
            CTIsSell = false;
            IsByClose = true;
            IsSellClose = false;
            UCTransaction.cd();
        }
        public bool TableChangeCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 选择合约
        /// </summary>
        public ICommand AgreementChangedCommand { get { return new RelayCommand(AgreementChangedExecuteChanged, AgreementChangedCanExecuteChanged); } }
        public void AgreementChangedExecuteChanged()
        {
            try
            {
                if (ContractCodeSelectedItem != null)
                {
                    VarietyModel vm = null;
                    string[] values = ContractCodeSelectedItem.SystemName.Split(' ');
                    if (values.Length != 3)
                    {
                        return;
                    }
                    string varietie = values[1];
                    if (ContractVariety.Varieties.ContainsKey(varietie))
                    {
                        vm = ContractVariety.Varieties[varietie];
                    }
                    if (vm == null) return;
                    Increment = vm.tick_size;
                    precision = vm.precision;
                    QuotesTabControlViewModel.GetInstance(null).ScrollIntoView(ContractCodeSelectedItem);
                    if (ContractCodeSelectedItem != null)
                    {
                        List<PotionDetailModelViewModel> tems = PositionAllViewModel.Instance().DetPMList.Where(x => x.ContractId == ContractCodeSelectedItem.SystemName).ToList();

                        if (tems == null || tems.Count == 0)
                        {
                            IsEnabled = false;
                        }
                        else
                        {
                            IsEnabled = true;
                        }
                    }
                }
                dic = null;

            }
            catch (Exception ex)
            {

                LogHelper.Info(ex.Message);
                return;
            }

        }

        public bool AgreementChangedCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 条件单
        /// </summary>
        public ICommand ConditionBillCommand { get { return new RelayCommand(ConditionBillExecuteChanged, ConditionBillCanExecuteChanged); } }
        public void ConditionBillExecuteChanged()
        {
            if (ContractCodeSelectedItem == null)
            {
                MessageBox.Show("请选择合约", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //if (_futures == null || _futures.lp <= 0 || double.IsNaN(_futures.lp))
            //{
            //    MessageBox.Show("当前合约没有行情!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            ConditionBill cfs = new ConditionBill(ContractCodeSelectedItem.SystemName, FTChangePrice, PriceTypeSelectedItem, FTNum);
            cfs.Owner = ContractVariety.main;
            cfs.ShowDialog();

        }
        public bool ConditionBillCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 买
        /// </summary>
        public ICommand BuyCommand { get { return new RelayCommand(BuyExecuteChanged, BuyCanExecuteChanged); } }
        public void BuyExecuteChanged()
        {
            if (ContractCodeSelectedItem == null) return;
            if (IsByClose)
            {

                TransactionModel tm = new TransactionModel();
                tm.direction = "B";
                tm.user_id = UserInfoHelper.UserId;
                tm.contract_id = ContractCodeSelectedItem.SystemName;
                tm.open_offset = IsByClose == true ? (int)OffsetType.OFFSET_OPEN : (int)OffsetType.OFFSET_COVER;
                tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                tm.order_orderref = Guid.NewGuid().ToString();
                tm.order_price = FTPrice;
                tm.operator_id = UserInfoHelper.LoginName;
                if (PriceTypeSelectedItem == "限价")
                {
                    if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                    {
                        MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    tm.order_price = FTChangePrice;
                    tm.price_type = "L";//根据选中的来判断；
                }
                else if (PriceTypeSelectedItem == "市价")
                {
                    tm.price_type = "M";//根据选中的来判断；
                    tm.order_price = 0;
                }
                else
                {

                    tm.price_type = "L";//根据选中的来判断；
                }
                if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
                {
                    if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  买入  开仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                }
                tm.order_volume = FTNum;
                ReqTransactionModel rtm = new ReqTransactionModel();
                rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
                rtm.content = tm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));
            }
            else if (IsSellClose)
            {
                // if (MessageBox.Show("确定买平合约" + ContractCodeSelectedItem.SystemName + "吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                //将持仓的卖了
                List<PotionDetailModelViewModel> tems = PositionAllViewModel.Instance().DetPMList.Where(x => x.ContractId == ContractCodeSelectedItem.SystemName && x.Direction == "S").ToList();
                if (tems == null || tems.Count == 0)
                {
                    MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int num = tems.Sum(x => x.PositionVolume);
                List<DelegationModelViewModel> cktemp = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == ContractCodeSelectedItem.SystemName && x.Direction == "B" && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
                if (cktemp != null && cktemp.Count > 0)
                {
                    num = num - cktemp.Sum(x => x.OrderVolume);
                }
                if (num < FTNum)
                {
                    MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                TransactionModel tm = new TransactionModel();
                tm.direction = "B";
                tm.contract_id = ContractCodeSelectedItem.SystemName;
                tm.user_id = UserInfoHelper.UserId;
                tm.open_offset = (int)OffsetType.OFFSET_COVER;
                tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                tm.order_orderref = Guid.NewGuid().ToString();
                tm.order_price = FTChangePrice;
                tm.operator_id = UserInfoHelper.LoginName;
                if (PriceTypeSelectedItem == "限价")
                {
                    if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                    {
                        MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    tm.price_type = "L";//根据选中的来判断；
                }
                else if (PriceTypeSelectedItem == "市价")
                {
                    tm.price_type = "M";//根据选中的来判断；
                    tm.order_price = 0;
                }
                else
                {

                    tm.price_type = "L";//根据选中的来判断；
                    tm.order_price = FTPrice;
                }
                tm.order_volume = FTNum;
                if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
                {
                    if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  买入  平仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                }
                ReqTransactionModel rtm = new ReqTransactionModel();
                rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
                rtm.content = tm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));
            }
        }
        public bool BuyCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 卖
        /// </summary>
        public ICommand SellCommand { get { return new RelayCommand(SellExecuteChanged, SellCanExecuteChanged); } }
        public void SellExecuteChanged()
        {
            if (ContractCodeSelectedItem == null) return;
            if (IsByClose)
            {

                TransactionModel tm = new TransactionModel();
                tm.direction = "S";
                tm.contract_id = ContractCodeSelectedItem.SystemName;
                tm.user_id = UserInfoHelper.UserId;
                tm.open_offset = IsByClose == true ? (int)OffsetType.OFFSET_OPEN : (int)OffsetType.OFFSET_COVER;
                tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                tm.order_orderref = Guid.NewGuid().ToString();
                tm.order_price = FTSellPrice;
                tm.operator_id = UserInfoHelper.LoginName;
                if (PriceTypeSelectedItem == "限价")
                {
                    if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                    {
                        MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    tm.order_price = FTChangePrice;
                    tm.price_type = "L";//根据选中的来判断；
                }
                else if (PriceTypeSelectedItem == "市价")
                {
                    tm.price_type = "M";//根据选中的来判断；
                    tm.order_price = 0;
                }
                else
                {

                    tm.price_type = "L";//根据选中的来判断；
                }
                tm.order_volume = FTNum;
                if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
                {
                    if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  卖出  开仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                }
                ReqTransactionModel rtm = new ReqTransactionModel();
                rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
                rtm.content = tm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));
            }
            else if (IsSellClose)
            {
                ////将持仓的卖了
                List<PotionDetailModelViewModel> tems = PositionAllViewModel.Instance().DetPMList.Where(x => x.ContractId == ContractCodeSelectedItem.SystemName && x.Direction == "B").ToList();
                if (tems == null || tems.Count == 0)
                {
                    MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int num = tems.Sum(x => x.PositionVolume);
                List<DelegationModelViewModel> cktemp = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == ContractCodeSelectedItem.SystemName && x.Direction == "S" && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
                if (cktemp != null && cktemp.Count > 0)
                {
                    num = num - cktemp.Sum(x => x.OrderVolume);
                }
                if (num < FTNum)
                {
                    MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                TransactionModel tm = new TransactionModel();
                tm.direction = "S";
                tm.contract_id = ContractCodeSelectedItem.SystemName;
                tm.user_id = UserInfoHelper.UserId;
                tm.open_offset = (int)OffsetType.OFFSET_COVER;
                tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                tm.order_orderref = Guid.NewGuid().ToString();
                tm.order_price = FTChangePrice;
                tm.operator_id = UserInfoHelper.LoginName;
                if (PriceTypeSelectedItem == "限价")
                {
                    if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                    {
                        MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    tm.price_type = "L";//根据选中的来判断；
                }
                else if (PriceTypeSelectedItem == "市价")
                {
                    tm.price_type = "M";//根据选中的来判断；
                    tm.order_price = 0;
                }
                else
                {

                    tm.price_type = "L";//根据选中的来判断；
                    tm.order_price = FTSellPrice;
                }
                tm.order_volume = FTNum;
                if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
                {
                    if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  卖出  平仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                }
                ReqTransactionModel rtm = new ReqTransactionModel();
                rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
                rtm.content = tm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));
            }

        }
        public bool SellCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 选择价格类型
        /// </summary>
    //    public ICommand TypeChangedCommand { get { return new RelayCommand(TypeChangedCommandExecuteChanged, TypeChangedCommandCanExecuteChanged); } }
        public void TypeChangedCommandExecuteChanged(string pricetypeitem)
        {
            PriceTypeSelectedItem = pricetypeitem;
            if (!string.IsNullOrEmpty(PriceTypeSelectedItem))
            {
                if (PriceTypeSelectedItem == "市价" || PriceTypeSelectedItem == "对手价")
                {
                    IsShow = Visibility.Hidden;
                }
                else
                {
                    IsShow = Visibility.Visible;
                    FTChangePrice = FTChangePriceTemp;
                }
            }

        }
        public bool TypeChangedCommandCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 开仓
        /// </summary>
        public ICommand BuyClose { get { return new RelayCommand(BuyCloseCommandExecuteChanged, BuyCloseCommandCanExecuteChanged); } }
        public void BuyCloseCommandExecuteChanged()
        {
            IsByClose = true;
            IsSellClose = false;

        }
        public bool BuyCloseCommandCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 平仓
        /// </summary>
        public ICommand SellClose { get { return new RelayCommand(SellCloseCommandExecuteChanged, SellCloseCanExecuteChanged); } }
        public void SellCloseCommandExecuteChanged()
        {
            IsByClose = false;
            IsSellClose = true;

        }
        public bool SellCloseCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 传统下单买
        /// </summary>
        public ICommand CTBuyCommand { get { return new RelayCommand(CTBuyCommandExecuteChanged, CTBuyCommandCanExecuteChanged); } }
        public void CTBuyCommandExecuteChanged()
        {
            CTIsBuy = true;
            CTIsSell = false;
            SelectNumExecuteChanged();

        }
        public bool CTBuyCommandCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 传统下单卖
        /// </summary>
        public ICommand CTSellCommand { get { return new RelayCommand(CTSellCommandExecuteChanged, CTSellCommandCanExecuteChanged); } }
        public void CTSellCommandExecuteChanged()
        {
            CTIsBuy = false;
            CTIsSell = true;
            SelectNumExecuteChanged();
        }
        public bool CTSellCommandCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 买
        /// </summary>
        public ICommand SJBuyCommand { get { return new RelayCommand(SJBuyExecuteChanged, SJBuyCanExecuteChanged); } }
        public void SJBuyExecuteChanged()
        {
            if (ContractCodeSelectedItem == null) return;
            //if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            //{
            //    if (MessageBox.Show("确定买开合约" + ContractCodeSelectedItem.SystemName + "吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            //}
            TransactionModel tm = new TransactionModel();
            tm.direction = "B";
            tm.user_id = UserInfoHelper.UserId;
            tm.contract_id = ContractCodeSelectedItem.SystemName;
            tm.open_offset = (int)OffsetType.OFFSET_OPEN;
            tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            tm.order_orderref = Guid.NewGuid().ToString();
            tm.order_price = FTPrice;
            tm.operator_id = UserInfoHelper.LoginName;
            if (PriceTypeSelectedItem == "限价")
            {
                if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                {
                    MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
                tm.order_price = FTChangePrice;
                tm.price_type = "L";//根据选中的来判断；
            }
            else if (PriceTypeSelectedItem == "市价")
            {
                tm.price_type = "M";//根据选中的来判断；
                tm.order_price = 0;
            }
            else
            {

                tm.price_type = "L";//根据选中的来判断；
            }
            if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            {
                if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  买入  开仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            }
            tm.order_volume = FTNum;
            ReqTransactionModel rtm = new ReqTransactionModel();
            rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
            rtm.content = tm;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));


        }
        public bool SJBuyCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 卖
        /// </summary>
        public ICommand SJSellCommand { get { return new RelayCommand(SJSellExecuteChanged, SJSellCanExecuteChanged); } }
        public void SJSellExecuteChanged()
        {
            if (ContractCodeSelectedItem == null) return;
            //if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            //{
            //    if (MessageBox.Show("确定卖开合约" + ContractCodeSelectedItem.SystemName + "吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            //}
            TransactionModel tm = new TransactionModel();
            tm.direction = "S";
            tm.contract_id = ContractCodeSelectedItem.SystemName;
            tm.user_id = UserInfoHelper.UserId;
            tm.open_offset = (int)OffsetType.OFFSET_OPEN;
            tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            tm.order_orderref = Guid.NewGuid().ToString();
            tm.order_price = FTSellPrice;
            tm.operator_id = UserInfoHelper.LoginName;
            if (PriceTypeSelectedItem == "限价")
            {
                if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                {
                    MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
                tm.price_type = "L";//根据选中的来判断；
                tm.order_price = FTChangePrice;
            }
            else if (PriceTypeSelectedItem == "市价")
            {
                tm.price_type = "M";//根据选中的来判断；
                tm.order_price = 0;
            }
            else
            {

                tm.price_type = "L";//根据选中的来判断；
            }
            tm.order_volume = FTNum;
            if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            {
                if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  卖出  开仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            }
            ReqTransactionModel rtm = new ReqTransactionModel();
            rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
            rtm.content = tm;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));

        }
        public bool SJSellCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// pingcang
        /// </summary>
        public ICommand SJClosingCommand { get { return new RelayCommand(SJClosingExecuteChanged, SJClosingCanExecuteChanged); } }
        public void SJClosingExecuteChanged()
        {
            List<PotionDetailModelViewModel> listpdmv = PositionViewModel.Instance().PMList.Where(x => x.ContractCode == ContractCodeSelectedItem.SystemName).ToList();
            if ((listpdmv == null || listpdmv.Count != 1) && PositionViewModel.Instance().SelectedItem == null)
            {
                MessageBox.Show("请在持仓中选择平仓的列表!", "操作提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string diction = PositionViewModel.Instance().SelectedItem.Direction;
            //将持仓的卖了
            List<PotionDetailModelViewModel> tems = PositionAllViewModel.Instance().DetPMList.Where(x => x.ContractId == ContractCodeSelectedItem.SystemName && x.Direction == diction).ToList();
            if (tems == null || tems.Count == 0)
            {
                MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int num = tems.Sum(x => x.PositionVolume);
            List<DelegationModelViewModel> cktemp = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == ContractCodeSelectedItem.SystemName && x.Direction != diction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
            if (cktemp != null && cktemp.Count > 0)
            {
                num = num - cktemp.Sum(x => x.OrderVolume);
            }
            if (num < FTNum)
            {
                MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TransactionModel tm = new TransactionModel();
            tm.direction = diction == "B" ? "S" : "B";
            tm.contract_id = ContractCodeSelectedItem.SystemName;
            tm.user_id = UserInfoHelper.UserId;
            tm.open_offset = (int)OffsetType.OFFSET_COVER;
            tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            tm.order_orderref = Guid.NewGuid().ToString();
            tm.order_price = SJClosePrice;
            tm.operator_id = UserInfoHelper.LoginName;
            if (PriceTypeSelectedItem == "限价")
            {
                if (!UserControlHelper.IsNum(SJClosePrice.ToString()))
                {
                    MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
                tm.price_type = "L";//根据选中的来判断；
            }
            else if (PriceTypeSelectedItem == "市价")
            {
                tm.price_type = "M";//根据选中的来判断；
                tm.order_price = 0;
            }
            else
            {

                tm.price_type = "L";//根据选中的来判断；
            }
            tm.order_volume = FTNum;
            if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            {
                if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  " + (diction == "B" ? "卖出" : "买入") + "  平仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            }
            ReqTransactionModel rtm = new ReqTransactionModel();
            rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
            rtm.content = tm;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));

        }
        public bool SJClosingCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// pingcang
        /// </summary>
        public ICommand CTBuyOrderCommand { get { return new RelayCommand(CTBuyOrderExecuteChanged, CTBuyOrderCanExecuteChanged); } }
        public void CTBuyOrderExecuteChanged()
        {
            if (ContractCodeSelectedItem == null) return;
            TransactionModel tm = new TransactionModel();
            if (CTIsBuy)
            {
                tm.direction = "B";
                tm.order_price = FTPrice;
            }
            else
            {
                tm.direction = "S";
                tm.order_price = FTSellPrice;
            }

            tm.contract_id = ContractCodeSelectedItem.SystemName;
            tm.user_id = UserInfoHelper.UserId;
            if (IsByClose)
            {
                tm.open_offset = (int)OffsetType.OFFSET_OPEN;
                tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                tm.order_orderref = Guid.NewGuid().ToString();
                // tm.order_price = FTChangePrice;
                tm.operator_id = UserInfoHelper.LoginName;
                if (PriceTypeSelectedItem == "限价")
                {
                    if (!UserControlHelper.IsNum(FTChangePrice.ToString()))
                    {
                        MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    tm.price_type = "L";//根据选中的来判断；
                    tm.order_price = FTChangePrice;
                }
                else if (PriceTypeSelectedItem == "市价")
                {
                    tm.price_type = "M";//根据选中的来判断；
                    tm.order_price = 0;

                }
                else
                {

                    tm.price_type = "L";//根据选中的来判断；
                }
                tm.order_volume = FTNum;
                if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
                {
                    if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  " + (tm.direction == "S" ? "卖出" : "买入") + "  开仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                }
                ReqTransactionModel rtm = new ReqTransactionModel();
                rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
                rtm.content = tm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));
            }
            else
            {
                tm.open_offset = (int)OffsetType.OFFSET_COVER;
                List<PotionDetailModelViewModel> tems = null;
                List<DelegationModelViewModel> cktemp = null;
                if (CTIsBuy)
                {
                    tems = PositionAllViewModel.Instance().DetPMList.Where(x => x.ContractId == ContractCodeSelectedItem.SystemName && x.Direction == "S").ToList();
                    cktemp = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == ContractCodeSelectedItem.SystemName && x.Direction == "B" && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();


                }
                else
                {
                    tems = PositionAllViewModel.Instance().DetPMList.Where(x => x.ContractId == ContractCodeSelectedItem.SystemName && x.Direction == "B").ToList();
                    cktemp = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == ContractCodeSelectedItem.SystemName && x.Direction == "S" && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();


                }
                //将持仓的卖了
                if (tems == null || tems.Count == 0)
                {
                    MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int num = tems.Sum(x => x.PositionVolume);
                if (cktemp != null && cktemp.Count > 0)
                {
                    num = num - cktemp.Sum(x => x.OrderVolume);
                }
                if (num < FTNum)
                {
                    MessageBox.Show("可平手数不足", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                tm.order_price = SJClosePrice;
                if (PriceTypeSelectedItem == "限价")
                {
                    tm.price_type = "L";//根据选中的来判断；
                    tm.order_price = FTChangePrice;
                }
                else if (PriceTypeSelectedItem == "市价")
                {
                    tm.price_type = "M";//根据选中的来判断；
                    tm.order_price = 0;

                }
                else
                {

                    tm.price_type = "L";//根据选中的来判断；
                }

                if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
                {
                    if (MessageBox.Show(ContractCodeSelectedItem.SystemName + "  " + (tm.direction == "S" ? "卖出" : "买入") + "  平仓  " + FTNum + "手  价格:" + (PriceTypeSelectedItem == "市价" ? "市价" : tm.order_price.ToString()) + "\n确认吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                }
                tm.order_volume = FTNum;
                ReqTransactionModel rtm = new ReqTransactionModel();
                rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
                rtm.content = tm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));

            }



        }
        public bool CTBuyOrderCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// pingcang
        /// </summary>
        public ICommand SelectNumCommand { get { return new RelayCommand(SelectNumExecuteChanged, SelectNumCanExecuteChanged); } }
        public void SelectNumExecuteChanged()
        {
            FigureUpNum(_futures);
        }
        public bool SelectNumCanExecuteChanged()
        {
            return true;
        }
        #endregion

    }
}
