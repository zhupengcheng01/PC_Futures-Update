using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.CommonClass;
using PC_Futures.Models;
using PC_Futures.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PC_Futures
{
    public class ConditionBillViewModel : ObservableObject
    {
        #region 变量
        private bool _IsFirst = false;
        private bool _IsSecond = false;
        private bool _IsThird = false;
        private bool _IsBuy = true;
        private bool _IsSell = false;
        private bool _IsOpen = true;
        private bool _IsCloseing = false;
        private string _ContractCode = "11111";
        private double _Price = 20.23;
        private int _Num = 1;

        private string _priceType = "市价";
        public string ContractCode
        {
            get { return _ContractCode; }
            set
            {
                if (_ContractCode != value)
                {
                    _ContractCode = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        public double Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    RaisePropertyChanged("Price");
                }
            }
        }
        public int Num
        {
            get { return _Num; }
            set
            {
                if (_Num != value)
                {
                    _Num = value;
                    RaisePropertyChanged("Num");
                }
            }
        }
        private double _Increment;
        public double Increment
        {
            get { return _Increment; }
            set
            {
                if (_Increment != value)
                {
                    _Increment = value;
                    RaisePropertyChanged("Increment");
                }
            }
        }


        public bool IsFirst
        {
            get { return _IsFirst; }
            set
            {
                if (value != _IsFirst)
                {
                    _IsFirst = value;
                    RaisePropertyChanged("IsFirst");
                }
            }
        }
        public bool IsSecond
        {
            get { return _IsSecond; }
            set
            {
                if (value != _IsSecond)
                {
                    _IsSecond = value;
                    RaisePropertyChanged("IsSecond");
                }
            }
        }
        public bool IsThird
        {
            get { return _IsThird; }
            set
            {
                if (value != _IsThird)
                {
                    _IsThird = value;
                    RaisePropertyChanged("IsThird");
                }
            }
        }

        private bool _IsEnabled = true;
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

        public bool IsBuy
        {
            get { return _IsBuy; }
            set
            {
                if (value != _IsBuy)
                {
                    _IsBuy = value;
                    RaisePropertyChanged("IsBuy");
                }
            }
        }

        public bool IsSell
        {
            get { return _IsSell; }
            set
            {
                if (value != _IsSell)
                {
                    _IsSell = value;
                    RaisePropertyChanged("IsSell");
                }
            }
        }
        public bool IsOpen
        {
            get { return _IsOpen; }
            set
            {
                if (value != _IsOpen)
                {
                    _IsOpen = value;
                    RaisePropertyChanged("IsOpen");
                }
            }
        }

        public bool IsCloseing
        {
            get { return _IsCloseing; }
            set
            {
                if (value != _IsCloseing)
                {
                    _IsCloseing = value;
                    RaisePropertyChanged("IsCloseing");
                }
            }
        }
        private double _OrderPrice;
        public double OrderPrice
        {
            get { return _OrderPrice; }
            set
            {
                if (value != _OrderPrice)
                {
                    _OrderPrice = Math.Round(value, lend);
                    RaisePropertyChanged("OrderPrice");
                }
            }
        }

        private double _OrderPrice3;
        public double OrderPrice3
        {
            get { return _OrderPrice3; }
            set
            {
                if (value != _OrderPrice3)
                {
                    _OrderPrice3 = Math.Round(value, lend);
                    RaisePropertyChanged("OrderPrice3");
                }
            }
        }



        private string _TrrigerTime = "12:00:00";

        public string TrrigerTime
        {
            get { return _TrrigerTime; }
            set
            {
                if (value != _TrrigerTime)
                {

                    if (value.ToString() == "1.00:00:00")
                    {
                        return;
                    }
                    else if (value.ToString() == "-01:00:00")
                    {
                        value = "23:00:00";
                    }
                    _TrrigerTime = value;
                    RaisePropertyChanged("TrrigerTime");
                }
            }

        }
        private string _TrrigerTime3 = "12:00:00";

        public string TrrigerTime3
        {
            get { return _TrrigerTime3; }
            set
            {
                if (value != _TrrigerTime3)
                {
                    if (value.ToString() == "1.00:00:00")
                    {
                        return;
                    }
                    else if (value.ToString() == "-01:00:00")
                    {
                        value = "23:00:00";
                    }
                    _TrrigerTime3 = value;
                    RaisePropertyChanged("TrrigerTime3");
                }
            }

        }

        private string _PriceTypeBill;
        public string PriceTypeBill
        {
            get { return _PriceTypeBill; }
            set
            {
                if (value != _PriceTypeBill)
                {
                    _PriceTypeBill = value;
                    RaisePropertyChanged("PriceTypeBill");
                }
            }

        }

        private Visibility _IsTypePrice = Visibility.Collapsed;

        public Visibility IsTypePrice
        {
            get { return _IsTypePrice; }
            set
            {
                if (value != _IsTypePrice)
                {
                    _IsTypePrice = value;
                    RaisePropertyChanged("IsTypePrice");
                }
            }

        }
        private Visibility _IsPrice = Visibility.Collapsed;

        public Visibility IsPrice
        {
            get { return _IsPrice; }
            set
            {
                if (value != _IsPrice)
                {
                    _IsPrice = value;
                    RaisePropertyChanged("IsPrice");
                }
            }

        }

        ObservableCollection<KeyValue> _PriceList = new ObservableCollection<KeyValue>();
        public ObservableCollection<KeyValue> PriceList
        {
            get { return _PriceList; }
            set
            {
                if (value != _PriceList)
                {
                    _PriceList = value;
                    RaisePropertyChanged("PriceList");
                }
            }
        }

        private KeyValue _PriceItem = null;
        public KeyValue PriceItem
        {
            get { return _PriceItem; }
            set
            {
                if (value != _PriceItem)
                {
                    _PriceItem = value;
                    RaisePropertyChanged("PriceItem");
                }
            }
        }

        ObservableCollection<KeyValue> _TrrigerCondition = new ObservableCollection<KeyValue>();
        public ObservableCollection<KeyValue> TrrigerCondition
        {
            get { return _TrrigerCondition; }
            set
            {
                if (value != _TrrigerCondition)
                {
                    _TrrigerCondition = value;
                    RaisePropertyChanged("TrrigerCondition");
                }
            }
        }
        private KeyValue _TrrigerConditionItem = null;
        public KeyValue TrrigerConditionItem
        {
            get { return _TrrigerConditionItem; }
            set
            {
                if (value != _TrrigerConditionItem)
                {
                    _TrrigerConditionItem = value;
                    RaisePropertyChanged("TrrigerConditionItem");
                }
            }
        }

        ObservableCollection<KeyValue> _PriceList3 = new ObservableCollection<KeyValue>();
        public ObservableCollection<KeyValue> PriceList3
        {
            get { return _PriceList3; }
            set
            {
                if (value != _PriceList3)
                {
                    _PriceList3 = value;
                    RaisePropertyChanged("PriceList3");
                }
            }
        }

        private KeyValue _PriceItem3 = null;
        public KeyValue PriceItem3
        {
            get { return _PriceItem3; }
            set
            {
                if (value != _PriceItem3)
                {
                    _PriceItem3 = value;
                    RaisePropertyChanged("PriceItem3");
                }
            }
        }

        ObservableCollection<KeyValue> _TrrigerCondition3 = new ObservableCollection<KeyValue>();
        public ObservableCollection<KeyValue> TrrigerCondition3
        {
            get { return _TrrigerCondition3; }
            set
            {
                if (value != _TrrigerCondition3)
                {
                    _TrrigerCondition3 = value;
                    RaisePropertyChanged("TrrigerCondition3");
                }
            }
        }

        private KeyValue _TrrigerConditionItem3 = null;
        public KeyValue TrrigerConditionItem3
        {
            get { return _TrrigerConditionItem3; }
            set
            {
                if (value != _TrrigerConditionItem3)
                {
                    _TrrigerConditionItem3 = value;
                    RaisePropertyChanged("TrrigerConditionItem3");
                }
            }
        }

        #endregion
        private static ConditionBillViewModel _ConditionBillViewModel;


        public static ConditionBillViewModel Intstace(string ContractCode, double price, string pricetype, int num)
        {

            if (_ConditionBillViewModel == null)
            {
                _ConditionBillViewModel = new ConditionBillViewModel(ContractCode, price, pricetype, num);
            }

            return _ConditionBillViewModel;
        }
        public static ConditionBillViewModel Intstace(ConditionBillModelViewModel condtion)
        {
            if (_ConditionBillViewModel == null)
            {
                _ConditionBillViewModel = new ConditionBillViewModel(condtion);
            }

            return _ConditionBillViewModel;
        }


        private string typePrice = "M";

        private ConditionBillModelViewModel Upatatecondtion = null;
        private ConditionBillViewModel(string contractCode, double price, string pricetype, int num)
        {
            _IsEnabled = true;
            //需要合约号 下单的类型和价格 以及数量
            ContractCode = contractCode;
            VarietyModel vm = null;
            string[] values = contractCode.Split(' ');
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
            lend = vm.precision;
            if (pricetype == "市价")
            {
                IsTypePrice = Visibility.Visible;
                IsPrice = Visibility.Collapsed;
                Price = 0;
                typePrice = "M";

            }
            else if (pricetype == "对手价")
            {
                Price = 0;
                typePrice = "R";
                IsTypePrice = Visibility.Visible;
                IsPrice = Visibility.Collapsed;
            }
            else
            {
                Price = price;
                typePrice = "L";
                IsTypePrice = Visibility.Collapsed;
                IsPrice = Visibility.Visible;
            }
            PriceTypeBill = pricetype;
            _priceType = pricetype;
            OrderPrice = price;
            OrderPrice3 = price;
            Num = num;
            //触发的类型
            KeyValue kv0 = new KeyValue() { Id = 0, Value = "最新价" };
            KeyValue kv1 = new KeyValue() { Id = 1, Value = "买一价" };
            KeyValue kv2 = new KeyValue() { Id = 2, Value = "卖一价" };
            PriceList.Add(kv0);
            PriceList.Add(kv1);
            PriceList.Add(kv2);
            PriceItem = kv0;
            KeyValue ttype0 = new KeyValue() { Id = 0, Value = ">=" };
            KeyValue ttype1 = new KeyValue() { Id = 1, Value = "<=" };
            TrrigerConditionItem = ttype0;
            TrrigerCondition.Add(ttype0);
            TrrigerCondition.Add(ttype1);
            KeyValue kv03 = new KeyValue() { Id = 0, Value = "最新价" };
            KeyValue kv13 = new KeyValue() { Id = 1, Value = "买一价" };
            KeyValue kv23 = new KeyValue() { Id = 2, Value = "卖一价" };
            PriceList3.Add(kv03);
            PriceList3.Add(kv13);
            PriceList3.Add(kv23);
            PriceItem3 = kv03;
            KeyValue ttype03 = new KeyValue() { Id = 0, Value = ">=" };
            KeyValue ttype13 = new KeyValue() { Id = 1, Value = "<=" };
            IsFirst = true;
            TrrigerCondition3.Add(ttype03);
            TrrigerCondition3.Add(ttype13);
            TrrigerConditionItem3 = ttype03;
        }

        private int lend = 0;
        private ConditionBillViewModel(ConditionBillModelViewModel condtion)
        {
            //需要合约号 下单的类型和价格 以及数量
            _IsEnabled = false;
            ContractCode = condtion.ContractCode;
            VarietyModel vm = null;
            string[] values = condtion.ContractCode.Split(' ');
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
            lend = vm.precision;
            typePrice = condtion.PriceType;
            if (typePrice == "M")
            {
                IsTypePrice = Visibility.Visible;
                IsPrice = Visibility.Collapsed;
                Price = 0;
                PriceTypeBill = "市价";
                _priceType = "市价";
            }
            else if (typePrice == "R")
            {
                Price = 0;
                PriceTypeBill = "对手价";
                _priceType = "对手价";
                IsTypePrice = Visibility.Visible;
                IsPrice = Visibility.Collapsed;
            }
            else
            {
                Price = condtion.OrderPrice;
                typePrice = "L";
                IsTypePrice = Visibility.Collapsed;
                IsPrice = Visibility.Visible;
            }

            OrderPrice = condtion.TrrigerPrice;
            OrderPrice3 = condtion.TrrigerPrice;
            Num = condtion.OrderVolume;
            //触发的类型
            KeyValue kv0 = new KeyValue() { Id = 0, Value = "最新价" };
            KeyValue kv1 = new KeyValue() { Id = 1, Value = "买一价" };
            KeyValue kv2 = new KeyValue() { Id = 2, Value = "卖一价" };
            PriceList.Add(kv0);
            PriceList.Add(kv1);
            PriceList.Add(kv2);
            PriceItem = kv0;
            KeyValue ttype0 = new KeyValue() { Id = 0, Value = ">=" };
            KeyValue ttype1 = new KeyValue() { Id = 1, Value = "<=" };
            TrrigerConditionItem = ttype0;
            TrrigerCondition.Add(ttype0);
            TrrigerCondition.Add(ttype1);
            KeyValue kv03 = new KeyValue() { Id = 0, Value = "最新价" };
            KeyValue kv13 = new KeyValue() { Id = 1, Value = "买一价" };
            KeyValue kv23 = new KeyValue() { Id = 2, Value = "卖一价" };
            PriceList3.Add(kv03);
            PriceList3.Add(kv13);
            PriceList3.Add(kv23);
            PriceItem3 = kv03;
            KeyValue ttype03 = new KeyValue() { Id = 0, Value = ">=" };
            KeyValue ttype13 = new KeyValue() { Id = 1, Value = "<=" };
            IsFirst = true;
            TrrigerCondition3.Add(ttype03);
            TrrigerCondition3.Add(ttype13);
            TrrigerConditionItem3 = ttype03;

            if (condtion.Direction == "B")
            {
                IsBuy = true;
                IsSell = false;
            }
            else if (condtion.Direction == "S")
            {
                IsBuy = false;
                IsSell = true;
            }
            if (condtion.OpenOffset == (int)OffsetType.OFFSET_OPEN)
            {
                IsOpen = true;
                IsCloseing = false;
            }
            else if (condtion.OpenOffset == (int)OffsetType.OFFSET_COVER)
            {
                IsCloseing = true;
                IsOpen = false;
            }
            if (condtion.ConditionType == (int)YunConditionType.Y_PRICE)
            {
                IsFirst = true;
                IsSecond = false;
                IsThird = false;
                TrrigerConditionItem = TrrigerCondition[condtion.TrrigerCondition];
                PriceItem = PriceList[condtion.TrrigerPriceType];

            }
            else if (condtion.ConditionType == (int)YunConditionType.Y_TIME)
            {
                IsFirst = false;
                IsSecond = true;
                IsThird = false;
                TrrigerTime = condtion.TrrigerContime; //trriger_contime
            }
            else if (condtion.ConditionType == (int)YunConditionType.Y_TIMEPRICE)
            {
                IsFirst = false;
                IsSecond = false;
                IsThird = true;
                TrrigerConditionItem = TrrigerCondition3[condtion.TrrigerCondition];
                PriceItem3 = PriceList3[condtion.TrrigerPriceType];
                TrrigerTime3 = condtion.TrrigerContime;
            }
            Upatatecondtion = condtion;
        }


        /// <summary>
        /// 编辑的参数
        /// </summary>
        /// <param name="para"></param>
        public ConditionBillViewModel(object para)
        {
            //需要合约号 下单的类型和价格 以及数量

            //触发的类型
            KeyValue kv0 = new KeyValue() { Id = 0, Value = "最新价" };
            KeyValue kv1 = new KeyValue() { Id = 1, Value = "买一价" };
            KeyValue kv2 = new KeyValue() { Id = 2, Value = "卖一价" };
            PriceList.Add(kv0);
            PriceList.Add(kv1);
            PriceList.Add(kv2);

            KeyValue ttype0 = new KeyValue() { Id = 0, Value = ">=" };
            KeyValue ttype1 = new KeyValue() { Id = 1, Value = "<=" };

            TrrigerCondition.Add(ttype0);
            TrrigerCondition.Add(ttype1);
            KeyValue kv03 = new KeyValue() { Id = 0, Value = "最新价" };
            KeyValue kv13 = new KeyValue() { Id = 1, Value = "买一价" };
            KeyValue kv23 = new KeyValue() { Id = 2, Value = "卖一价" };
            PriceList3.Add(kv03);
            PriceList3.Add(kv13);
            PriceList3.Add(kv23);

            KeyValue ttype03 = new KeyValue() { Id = 0, Value = ">=" };
            KeyValue ttype13 = new KeyValue() { Id = 1, Value = "<=" };

            TrrigerCondition3.Add(ttype03);
            TrrigerCondition3.Add(ttype13);
        }

        public ICommand FirstCommand { get { return new RelayCommand(FirstExecuteChanged, FirstCanExecuteChanged); } }
        public void FirstExecuteChanged()
        {
            IsFirst = true;
            if (IsFirst)
            {
                IsSecond = false;
                IsThird = false;
            }

        }
        public bool FirstCanExecuteChanged()
        {
            return true;
        }

        public ICommand SecondCommand { get { return new RelayCommand(SecondExecuteChanged, SecondCanExecuteChanged); } }
        public void SecondExecuteChanged()
        {
            IsSecond = true;
            if (IsSecond)
            {
                IsFirst = false;
                IsThird = false;
            }

        }
        public bool SecondCanExecuteChanged()
        {
            return true;
        }
        public ICommand ThirdCommand { get { return new RelayCommand(ThirdExecuteChanged, ThirdCanExecuteChanged); } }
        public void ThirdExecuteChanged()
        {
            IsThird = true;
            if (IsThird)
            {
                IsSecond = false;
                IsFirst = false;
            }

        }
        public bool ThirdCanExecuteChanged()
        {
            return true;
        }
        public ICommand BuyCommand { get { return new RelayCommand(BuyExecuteChanged, BuyCanExecuteChanged); } }
        public void BuyExecuteChanged()
        {
            IsBuy = true;
            IsSell = false;
        }
        public bool BuyCanExecuteChanged()
        {
            return true;
        }
        public ICommand SellCommand { get { return new RelayCommand(SellExecuteChanged, SellCanExecuteChanged); } }


        public void SellExecuteChanged()
        {
            IsBuy = false;
            IsSell = true;

        }
        public bool SellCanExecuteChanged()
        {
            return true;
        }
        public ICommand OpenCommand { get { return new RelayCommand(OpenExecuteChanged, OpenCanExecuteChanged); } }

        public void OpenExecuteChanged()
        {
            IsOpen = true;
            IsCloseing = false;
        }
        public bool OpenCanExecuteChanged()
        {
            return true;
        }
        public ICommand CloseingCommand { get { return new RelayCommand(CloseingExecuteChanged, CloseingCanExecuteChanged); } }

        public void CloseingExecuteChanged()
        {
            IsOpen = false;
            IsCloseing = true;

        }
        public bool CloseingCanExecuteChanged()
        {
            return true;
        }

        public ICommand ButClose { get { return new RelayCommand(ButCloseExecuteChanged, ButCloseCanExecuteChanged); } }
        public void ButCloseExecuteChanged()
        {
            Close();
        }
        public bool ButCloseCanExecuteChanged()
        {
            return true;
        }

        public ICommand EnterCommand { get { return new RelayCommand(EnterCommandExecuteChanged, EnterCommandCanExecuteChanged); } }
        public void EnterCommandExecuteChanged()
        {
            ReqConditionBillModel rcbm = new ReqConditionBillModel();

            RConditionBillModel cbm = new RConditionBillModel();
            cbm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            cbm.contract_id = ContractCode;
            cbm.order_price = Price;//提供
            cbm.order_volume = Num;//提供
            cbm.user_id = UserInfoHelper.UserId;
            if (IsBuy)
            {
                cbm.direction = "B";
            }
            else if (IsSell)
            {
                cbm.direction = "S";
            }
            else
            {
                MessageBox.Show("请选择买卖方向", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (IsOpen)
            {
                cbm.open_offset = (int)OffsetType.OFFSET_OPEN;
            }
            else if (IsCloseing)
            {
                cbm.open_offset = (int)OffsetType.OFFSET_COVER;
            }
            else
            {
                MessageBox.Show("请选择买卖方向", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsFirst)
            {
                cbm.condition_type = (int)YunConditionType.Y_PRICE;
                cbm.trriger_price = OrderPrice;
                if (PriceItem == null)
                {
                    MessageBox.Show("请选择触发价格类型", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                cbm.trriger_price_type = PriceItem.Id;
                if (TrrigerConditionItem == null)
                {
                    MessageBox.Show("请选择触条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                cbm.trriger_condition = TrrigerConditionItem.Id;
                if (cbm.trriger_price_type == 0)
                {
                    if (cbm.trriger_condition == 0)
                    {
                        if (OrderPrice <= TransactionViewModel.Instance().Futures.Tick.LastPrice)
                        {
                            MessageBox.Show("设置的最新价不能小于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (OrderPrice >= TransactionViewModel.Instance().Futures.Tick.LastPrice)
                        {
                            MessageBox.Show("设置的最新价不能大于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
                else if (cbm.trriger_price_type == 1)
                {
                    if (cbm.trriger_condition == 0)
                    {
                        if (OrderPrice <= TransactionViewModel.Instance().Futures.Tick.BidP1)
                        {
                            MessageBox.Show("设置的买一价不能小于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (OrderPrice >= TransactionViewModel.Instance().Futures.Tick.BidP1)
                        {
                            MessageBox.Show("设置的买一价不能大于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
                else if (cbm.trriger_price_type == 2)
                {
                    if (cbm.trriger_condition == 0)
                    {
                        if (OrderPrice <= TransactionViewModel.Instance().Futures.Tick.AskP1)
                        {
                            MessageBox.Show("设置的卖一价不能小于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (OrderPrice >= TransactionViewModel.Instance().Futures.Tick.AskP1)
                        {
                            MessageBox.Show("设置的卖一价不能大于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
            }
            else if (IsSecond)
            {
                cbm.condition_type = (int)YunConditionType.Y_TIME;
                cbm.trriger_contime = TrrigerTime;
            }
            else if (IsThird)
            {
                cbm.condition_type = (int)YunConditionType.Y_TIMEPRICE;
                cbm.trriger_price = OrderPrice3;
                if (PriceItem3 == null)
                {
                    MessageBox.Show("请选择触发价格类型", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                cbm.trriger_price_type = PriceItem3.Id;
                if (TrrigerConditionItem3 == null)
                {
                    MessageBox.Show("请选择触条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                cbm.trriger_condition = TrrigerConditionItem3.Id;
                cbm.trriger_contime = TrrigerTime3;
                if (cbm.trriger_price_type == 0)
                {
                    if (cbm.trriger_condition == 0)
                    {
                        if (OrderPrice3 <= TransactionViewModel.Instance().Futures.Tick.LastPrice)
                        {
                            MessageBox.Show("设置的最新价不能小于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (OrderPrice3 >= TransactionViewModel.Instance().Futures.Tick.LastPrice)
                        {
                            MessageBox.Show("设置的最新价不能大于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
                else if (cbm.trriger_price_type == 1)
                {
                    if (cbm.trriger_condition == 0)
                    {
                        if (OrderPrice3 <= TransactionViewModel.Instance().Futures.Tick.BidP1)
                        {
                            MessageBox.Show("设置的买一价不能小于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (OrderPrice3 >= TransactionViewModel.Instance().Futures.Tick.BidP1)
                        {
                            MessageBox.Show("设置的买一价不能大于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
                else if (cbm.trriger_price_type == 2)
                {
                    if (cbm.trriger_condition == 0)
                    {
                        if (OrderPrice3 <= TransactionViewModel.Instance().Futures.Tick.AskP1)
                        {
                            MessageBox.Show("设置的卖一价不能小于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (OrderPrice3 >= TransactionViewModel.Instance().Futures.Tick.AskP1)
                        {
                            MessageBox.Show("设置的卖一价不能大于当前的行情最新价", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            cbm.price_type = typePrice;
            rcbm.content = cbm;
            if (Upatatecondtion == null)
            {
                rcbm.cmdcode = RequestCmdCode.AddConditionBill;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rcbm));
            }
            else
            {
                rcbm.content.condition_orderID = Upatatecondtion.ConditionOrderID;
                rcbm.content.trriger_condate = Upatatecondtion.TrrigerCondate;
                rcbm.content.price_type = Upatatecondtion.PriceType;
                rcbm.cmdcode = RequestCmdCode.UpConditionBill;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rcbm));
            }
        }
        public bool EnterCommandCanExecuteChanged()
        {
            return true;
        }

        public Action CloseAction
        {
            get; set;
        }
        public void Close()
        {
            if (CloseAction != null)
            {
                _ConditionBillViewModel = null;
                CloseAction();

            }
        }

        public void Clear()
        {

            _ConditionBillViewModel = null;

        }

    }

    public class KeyValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }


}
