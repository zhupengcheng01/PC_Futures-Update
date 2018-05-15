using Futures.Enum;
using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class CheckFullStopViewModel : ObservableObject
    {

        PotionDetailModelViewModel _PositionModelViewModel;
        /// <summary>
        /// 合约号
        /// </summary>
        private string _ContractCode;
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

        /// <summary>
        /// 方向
        /// </summary>
        private string _Direction;
        public string Direction
        {
            get { return _Direction; }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    RaisePropertyChanged("Direction");
                }
            }

        }

        private ObservableCollection<CheckFullStopModelViewModel> _CFSMVMList = new ObservableCollection<CheckFullStopModelViewModel>();

        public ObservableCollection<CheckFullStopModelViewModel> CFSMVMList
        {
            get { return _CFSMVMList; }
            set
            {
                if (_CFSMVMList != value)
                {
                    _CFSMVMList = value;
                    RaisePropertyChanged("CFSMVMList");
                }
            }

        }
        private CheckFullStopModelViewModel _SelectedItem;

        public bool IsHaveData = false;
        public CheckFullStopModelViewModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }

        }

        private bool _IsOne = false;
        private bool _IsAuto = true;

        public bool IsOne
        {
            get { return _IsOne; }
            set
            {
                if (_IsOne != value)
                {
                    _IsOne = value;
                    RaisePropertyChanged("IsOne");
                }
            }
        }
        public bool IsAuto
        {
            get { return _IsAuto; }
            set
            {
                if (_IsAuto != value)
                {
                    _IsAuto = value;
                    RaisePropertyChanged("IsAuto");
                }
            }
        }


        int _AllNum = 0;
        /// <summary>
        /// 合约号
        /// </summary>
        /// <param name="contract_code"></param>
        /// 
        private static CheckFullStopViewModel _CheckFullStopViewModel;

        public static CheckFullStopViewModel Instance(PotionDetailModelViewModel pmvm)
        {

            if (pmvm == null&& _CheckFullStopViewModel == null)
            {
                return null;
            }
            if (_CheckFullStopViewModel == null)
            {
                _CheckFullStopViewModel = new CheckFullStopViewModel(pmvm);
            }
            return _CheckFullStopViewModel;
        }

        public static bool IsInstance()
        {
            if (_CheckFullStopViewModel == null)
            {
                return false;
            }
            return true;
        }
        private double Increment = 0;
        private int length = 1;
        private CheckFullStopViewModel(PotionDetailModelViewModel pmvm)
        {
            _PositionModelViewModel = pmvm;
            ContractCode = pmvm.ContractCode;
            _AllNum = _PositionModelViewModel.PositionVolume;
            Direction = pmvm.Direction == "B" ? " (买入，投机) " : " (卖出，投机) ";


            VarietyModel vm = null;
            string[] values = pmvm.ContractCode.Split(' ');
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
            length = vm.precision;
            ////发送命令
            //ReqPotion rp = new ReqPotion();
            //rp.cmdcode = RequestCmdCode.SelectStopLoss;
            //rp.content = new ReqLoginName() { user_id = UserInfoHelper.UserId };
            //ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));    
            if (CommHelper.CFSmvmList.ContainsKey(_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction))
            {
                foreach (var item in CommHelper.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction])
                {
                    item.Increment = Increment;              
                    item.Precision = length;
                    CheckFullStopModel cfsm = new CheckFullStopModel();
                    cfsm.contract_code = _PositionModelViewModel.ContractCode;
                    cfsm.contract_id = _PositionModelViewModel.ContractId;
                    cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                    cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
                    cfsm.user_id = UserInfoHelper.UserId;
                    cfsm.order_volume = _PositionModelViewModel.PositionVolume;
                    cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                    cfsm.direction = item.Direction;
                    cfsm.float_loss = item.FloatLoss;
                    cfsm.float_loss_flag = item.FloatLossFlag;
                    cfsm.open_offset = item.OpenOffset;
                    cfsm.order_volume = item.OrderVolume;
                   
                    CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
                    cfsvm.Increment = item.Increment;
                    cfsvm.Precision = item.Precision;
                    cfsvm.Direction = item.Direction;
                    cfsvm.MaxNum = item.MaxNum;
                    cfsvm.StopprofitPrice = item.StopprofitPrice;
                    cfsvm.StoplossId = item.StoplossId;
                    cfsvm.TrrigerCondition = item.TrrigerCondition;
                    cfsvm.TrrigerPrice = item.TrrigerPrice;
                    cfsvm.FloatLoss = item.FloatLoss;
                  
                    cfsvm.StoplossPrice = item.StoplossPrice;
                    cfsvm.CreateDate = item.CreateDate;
                    cfsvm.OrderVolume = item.OrderVolume;
                    CFSMVMList.Add(cfsvm);
                }
            }
            if (CFSMVMList.Count > 0)
            {
                IsHaveData = true;
            }
            AddData();
        }
        internal void AddData()
        {
            if (CFSMVMList.Count == 0)
            {
                CheckFullStopModel cfsm = new CheckFullStopModel();
                cfsm.contract_code = _PositionModelViewModel.ContractCode;
                cfsm.contract_id = _PositionModelViewModel.ContractId;
                cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
                cfsm.user_id = UserInfoHelper.UserId;
                cfsm.order_volume = _PositionModelViewModel.PositionVolume;
                cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
                cfsvm.MaxNum = _AllNum;
                
                cfsvm.Increment = Increment;
                cfsvm.Precision = length;
                CFSMVMList.Add(cfsvm);
            }
            else
            {
                int num = _AllNum - CFSMVMList.Sum(s => s.OrderVolume);
                if (num > 0)
                {
                    CheckFullStopModel cfsm = new CheckFullStopModel();
                    cfsm.contract_code = _PositionModelViewModel.ContractCode;
                    cfsm.contract_id = _PositionModelViewModel.ContractId;
                    cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                    cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
                    cfsm.user_id = UserInfoHelper.UserId;
                    cfsm.order_volume = num;
                    cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                    CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
                    cfsvm.MaxNum = _AllNum;
                    cfsvm.Precision = length;
                    cfsvm.Increment = Increment;

                    CFSMVMList.Add(cfsvm);
                }
            }
        }
        #region 命令
        /// <summary>
        /// 确定
        /// </summary>
        public ICommand EnterCommand { get { return new RelayCommand(EnterExecuteChanged, EnterCanExecuteChanged); } }
        public void EnterExecuteChanged()
        {
            List<CheckFullStopModelViewModel> temps = CFSMVMList.ToList();
            List<CheckFullStopModel> cfsmList = new List<CheckFullStopModel>();
           
            foreach (CheckFullStopModelViewModel item in temps)
            {
                if ((item.StoplossPrice > 0 || item.StopprofitPrice > 0)&&item.OrderVolume>0)
                {
                    
                    CheckFullStopModel cfsm = new CheckFullStopModel();
                    cfsm.user_id = item.UserId;
                    cfsm.contract_id = _PositionModelViewModel.ContractId;
                    cfsm.direction = _PositionModelViewModel.Direction;
                    cfsm.open_offset = (int)OffsetType.OFFSET_COVER;
                    cfsm.order_volume = item.OrderVolume;
                    cfsm.stoploss_price = item.StoplossPrice;
                    cfsm.stopprofit_price = item.StopprofitPrice;
                    cfsm.float_loss = item.FloatLoss;
                    cfsm.float_loss_flag = (int)FloatLossFlag.FLF_FloatLoss;
                    if (CommParameterSetting.StopLossModel.StopLossPrice == "最新价")
                    {
                        cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_LASTPRICE;
                        if (_PositionModelViewModel.Direction == "B")
                        {
                            if (cfsm.stoploss_price >= TransactionViewModel.Instance().Futures.Tick.LastPrice&& cfsm.stoploss_price!=0)
                            {
                                MessageBox.Show("设置的止损价不能大于当前的行情最新价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (cfsm.stopprofit_price <= TransactionViewModel.Instance().Futures.Tick.LastPrice&& cfsm.stopprofit_price != 0)
                            {
                                MessageBox.Show("设置的止盈价不能小于当前的行情最新价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            if (cfsm.stoploss_price <= TransactionViewModel.Instance().Futures.Tick.LastPrice&& cfsm.stoploss_price != 0)
                            {
                                MessageBox.Show("设置的止损价不能小于当前的行情最新价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (cfsm.stopprofit_price >= TransactionViewModel.Instance().Futures.Tick.LastPrice && cfsm.stopprofit_price != 0)
                            {
                                MessageBox.Show("设置的止盈价不能大于当前的行情最新价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (_PositionModelViewModel.Direction == "S")
                        {
                            cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_SELLONEPRICE;

                        }
                        else
                        {
                            cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_BUYONEPRICE;
                        }

                        if (_PositionModelViewModel.Direction == "B")
                        {
                            if (cfsm.stoploss_price >= TransactionViewModel.Instance().Futures.Tick.BidP1&& cfsm.stoploss_price != 0)
                            {
                                MessageBox.Show("设置的止损价不能大于当前的行情的对手价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (cfsm.stopprofit_price <= TransactionViewModel.Instance().Futures.Tick.BidP1 && cfsm.stopprofit_price != 0)
                            {
                                MessageBox.Show("设置的止盈价不能小于当前的行情的对手价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            if (cfsm.stoploss_price <= TransactionViewModel.Instance().Futures.Tick.AskP1 && cfsm.stoploss_price != 0)
                            {
                                MessageBox.Show("设置的止损价不能小于当前的行情的对手价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (cfsm.stopprofit_price >= TransactionViewModel.Instance().Futures.Tick.AskP1 && cfsm.stopprofit_price != 0)
                            {
                                MessageBox.Show("设置的止盈价不能大于当前的行情的对手价", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    if (CommParameterSetting.StopLossModel.StopLossDelegation == "市价")
                    {
                        cfsm.price_type = "M";
                    }
                    else if (CommParameterSetting.StopLossModel.StopLossDelegation == "最新价")
                    {
                        cfsm.price_type = "L";
                        if (_PositionModelViewModel.Direction == "S")
                        {

                            cfsm.order_add_price = CommParameterSetting.StopLossModel.BuyNum * Increment;
                        }
                        else
                        {
                            cfsm.order_add_price = CommParameterSetting.StopLossModel.SellNum * Increment;

                        }
                    }
                    else
                    {
                        cfsm.price_type = "R";
                        if (_PositionModelViewModel.Direction == "S")
                        {

                            cfsm.order_add_price = CommParameterSetting.StopLossModel.BuyNum * Increment;
                        }
                        else
                        {
                            cfsm.order_add_price = CommParameterSetting.StopLossModel.SellNum * Increment;

                        }
                    }
                    cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                    cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
                    cfsmList.Add(cfsm);

                    CheckFullStopModelViewModel cfsmvm = new CheckFullStopModelViewModel(cfsm);
                   // ContractVariety.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction].Add(cfsmvm);
                }
            }
            if (cfsmList.Count > 0)
            {
              

                ReqCheckFullStopModel rcfsmv = new ReqCheckFullStopModel();
                rcfsmv.cmdcode = RequestCmdCode.AddStopLoss;
                rcfsmv.content = cfsmList;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rcfsmv));
                if (CommHelper.CFSmvmList.ContainsKey(_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction))
                {
                    CommHelper.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction].Clear();
                }
                else
                {
                    CommHelper.CFSmvmList.Add(_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction, new List<CheckFullStopModelViewModel>());

                }

                CommHelper.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction].Clear();
            }
            else
            {
                if (IsHaveData)
                {
                    ReqDeleteCheckFullStop rdmv = new ReqDeleteCheckFullStop();
                    DeleteCheckFullStop scfm = new DeleteCheckFullStop();
                    scfm.contract_id = _PositionModelViewModel.ContractId;
                    scfm.direction = _PositionModelViewModel.Direction;
                    scfm.resource = (int)OperatorTradeType.OPERATOR_TRADE_MC;
                    scfm.user_id = UserInfoHelper.UserId;
                    rdmv.cmdcode = RequestCmdCode.DeleteStopLoss;
                    rdmv.content = scfm;
                    ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rdmv));
                }
            }
        }
        public void Result()
        {

            List<CheckFullStopModelViewModel> temps = CFSMVMList.ToList();
            double maxprice = 99999999999;
            double minprice = 0;
            int maxnum = 0;
            int minnum = 0;

            if (CommHelper.CFSmvmList.ContainsKey(_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction))
            {
                CommHelper.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction].Clear();
            }
            else
            {
                CommHelper.CFSmvmList.Add(_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction, new List<CheckFullStopModelViewModel>());

            }

            CommHelper.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction].Clear();

            foreach (CheckFullStopModelViewModel item in temps)
            {
                if (item.StoplossPrice > 0 || item.StopprofitPrice > 0)
                {
                    if (item.StopprofitPrice > 0)
                    {
                        if (item.StopprofitPrice < maxprice)
                        {
                            maxprice = item.StopprofitPrice;
                            maxnum = item.OrderVolume;
                        }
                        else if (item.StopprofitPrice == maxprice)
                        {
                            maxnum += item.OrderVolume;
                        }

                    }
                    if (item.StoplossPrice > 0)
                    {
                        if (item.StoplossPrice > minprice)
                        {
                            minprice = item.StoplossPrice;
                            minnum = item.OrderVolume;
                        }
                        else if (item.StoplossPrice == minprice)
                        {
                            minnum += item.OrderVolume;
                        }
                    }
                    CheckFullStopModel cfsm = new CheckFullStopModel();
                    cfsm.user_id = item.UserId;
                    cfsm.contract_id = _PositionModelViewModel.ContractId;
                    cfsm.direction = _PositionModelViewModel.Direction;
                    cfsm.open_offset = (int)OffsetType.OFFSET_COVER;
                    cfsm.order_volume = item.OrderVolume;
                    cfsm.stoploss_price = item.StoplossPrice;
                    cfsm.stopprofit_price = item.StopprofitPrice;
                    cfsm.float_loss = item.FloatLoss;
                    cfsm.float_loss_flag = (int)FloatLossFlag.FLF_FloatLoss;
                    if (CommParameterSetting.StopLossModel.StopLossPrice == "最新价")
                    {
                        cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_LASTPRICE;
                    }
                    else
                    {
                        if (_PositionModelViewModel.Direction == "S")
                        {
                            cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_BUYONEPRICE;
                            cfsm.order_add_price = CommParameterSetting.StopLossModel.BuyNum;
                        }
                        else
                        {
                            cfsm.order_add_price = CommParameterSetting.StopLossModel.SellNum;
                            cfsm.trriger_price_type = (int)YunTrrigerStyle.Y_SELLONEPRICE;
                        }
                    }
                    if (CommParameterSetting.StopLossModel.StopProfitDelegation == "市价")
                    {
                        cfsm.price_type = "M";
                    }
                    else
                    {
                        cfsm.price_type = "L";
                    }
                    cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                    cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");


                    CheckFullStopModelViewModel cfsmvm = new CheckFullStopModelViewModel(cfsm);
                    CommHelper.CFSmvmList[_PositionModelViewModel.ContractId + _PositionModelViewModel.Direction].Add(cfsmvm);
                }
            }
            if (maxprice < 99999999999 && maxnum > 0)
            {
                _PositionModelViewModel.ProfitVolume = maxprice + "/" + maxnum;

            }
            if (minprice > 0 && minnum > 0)
            {
                _PositionModelViewModel.LossVolume = minprice + "/" + minnum;

            }
            this.Close();
        }

        public bool EnterCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 取消
        /// </summary>
        public ICommand CannelCommand { get { return new RelayCommand(CannelExecuteChanged, CannelCanExecuteChanged); } }
        public void CannelExecuteChanged()
        {
            Close();

        }
        public bool CannelCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 全部删除
        /// </summary>
        public ICommand DeleteAllCommand { get { return new RelayCommand(DeleteExecuteChanged, DeleteCanExecuteChanged); } }
        public void DeleteExecuteChanged()
        {
            CFSMVMList.Clear();
            CheckFullStopModel cfsm = new CheckFullStopModel();
            cfsm.contract_code = _PositionModelViewModel.ContractCode;
            cfsm.contract_id = _PositionModelViewModel.ContractId;
            cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
            cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
            cfsm.user_id = UserInfoHelper.UserId;
            cfsm.order_volume = _AllNum;
            cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
            CFSMVMList.Add(cfsvm);

        }
        public bool DeleteCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 自动
        /// </summary>
        public ICommand AutoCommand { get { return new RelayCommand(AutoExecuteChanged, AutoCanExecuteChanged); } }
        public void AutoExecuteChanged()
        {
            IsAuto = true;
            IsOne = false;
            int countNum = 0;//表示已经设置过了价格的数量
            List<CheckFullStopModelViewModel> temps = CFSMVMList.ToList();
            CFSMVMList.Clear();
            foreach (CheckFullStopModelViewModel item in temps)
            {
                if (item.StoplossPrice > 0 || item.StopprofitPrice > 0)
                {
                    countNum += item.OrderVolume;
                    CFSMVMList.Add(item);
                }
            }
            int shengcount = _AllNum - countNum;

            CheckFullStopModel cfsm = new CheckFullStopModel();
            cfsm.contract_code = _PositionModelViewModel.ContractCode;
            cfsm.contract_id = _PositionModelViewModel.ContractId;
            cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
            cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
            cfsm.user_id = UserInfoHelper.UserId;
            cfsm.order_volume = shengcount;
            cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
            CFSMVMList.Add(cfsvm);


        }
        public bool AutoCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 一手
        /// </summary>
        public ICommand OneCommand { get { return new RelayCommand(OneExecuteChanged, OneCanExecuteChanged); } }
        public void OneExecuteChanged()
        {
            IsAuto = false;
            IsOne = true;
            int countNum = 0;//表示已经设置过了价格的数量
            List<CheckFullStopModelViewModel> temps = CFSMVMList.ToList();
            CFSMVMList.Clear();
            foreach (CheckFullStopModelViewModel item in temps)
            {
                if (item.StoplossPrice > 0 || item.StopprofitPrice > 0)
                {
                    countNum += item.OrderVolume;
                    CFSMVMList.Add(item);
                }
            }
            int shengcount = _AllNum - countNum;
            while (shengcount > 0)
            {
                CheckFullStopModel cfsm = new CheckFullStopModel();
                cfsm.contract_code = _PositionModelViewModel.ContractCode;
                cfsm.contract_id = _PositionModelViewModel.ContractId;
                cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
                cfsm.user_id = UserInfoHelper.UserId;
                cfsm.order_volume = 1;
                cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
                cfsvm.Increment = Increment;
                cfsvm.Precision = length;
                CFSMVMList.Add(cfsvm);
                shengcount--;
            }
        }
        public bool OneCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 计算最大值
        /// </summary>
        public ICommand SelectionChangedCommand { get { return new RelayCommand(SelectionChangedExecuteChanged, SelectionChangedCanExecuteChanged); } }
        public void SelectionChangedExecuteChanged()
        {
            int MaxNum = 0;
            if (CFSMVMList.Count > 0)
            {
                MaxNum = _AllNum - CFSMVMList.Where(x => x.StoplossPrice > 0 || x.StopprofitPrice > 0).ToList().Sum(s => s.OrderVolume);
                if (SelectedItem != null)
                {
                    if (SelectedItem.StoplossPrice > 0 || SelectedItem.StopprofitPrice > 0)
                    {
                        SelectedItem.MaxNum = MaxNum + SelectedItem.OrderVolume;
                    }
                    else
                    {
                        SelectedItem.MaxNum = MaxNum;
                    }
                }
            }

        }
        public bool SelectionChangedCanExecuteChanged()
        {
            return true;
        }


        /// <summary>
        /// 数量改变的触发
        /// </summary>
        public ICommand SelectedCommand { get { return new RelayCommand(NumChangedExecuteChanged, NumChangedCanExecuteChanged); } }
        public void NumChangedExecuteChanged()
        {
            int countNum = 0;
            int count = 0;//表示止损或止盈价已经设置了
            foreach (CheckFullStopModelViewModel item in CFSMVMList)
            {
                if (item.StoplossPrice > 0 || item.StopprofitPrice > 0)
                {
                    count++;
                }
                countNum += item.OrderVolume;
            }
            if (countNum < _AllNum && CFSMVMList.Count - count <= 1)
            {

                CheckFullStopModel cfsm = new CheckFullStopModel();
                cfsm.contract_code = _PositionModelViewModel.ContractCode;
                cfsm.contract_id = _PositionModelViewModel.ContractId;
                cfsm.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                cfsm.create_time = DateTime.Now.ToString("HH:mm:ss");
                cfsm.user_id = UserInfoHelper.UserId;
                cfsm.order_volume = _AllNum - countNum;
                cfsm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                CheckFullStopModelViewModel cfsvm = new CheckFullStopModelViewModel(cfsm);
                if (SelectedItem.OrderVolume == 0)
                {
                    CFSMVMList.Remove(SelectedItem);
                }
                cfsvm.Increment = Increment;
                cfsvm.Precision = length;
                CFSMVMList.Add(cfsvm);

            }
            else if (countNum < _AllNum && CFSMVMList.Count - count >= 2)
            {
                int tempcount = CFSMVMList.Sum(x => x.OrderVolume);
                foreach (CheckFullStopModelViewModel item in CFSMVMList)
                {
                    if (item.StoplossPrice == 0 && item.StopprofitPrice == 0 && (item.CreateDate + item.CreateTime) != (SelectedItem.CreateDate + SelectedItem.CreateTime))
                    {
                        item.OrderVolume = _AllNum - tempcount + item.OrderVolume;
                        if (item.OrderVolume == 0)
                            CFSMVMList.Remove(item);
                        break;
                    }
                }
                if (SelectedItem.OrderVolume == 0)
                {
                    CFSMVMList.Remove(SelectedItem);
                }
            }
            else if (countNum > _AllNum && count <= CFSMVMList.Count - 1)
            {
                List<CheckFullStopModelViewModel> CFSMVMListtemp = CFSMVMList.Where(x => x.StoplossPrice == 0 && x.StopprofitPrice == 0).ToList();
                int num = CFSMVMList.Where(x => x.StoplossPrice > 0 || x.StopprofitPrice > 0).Sum(o => o.OrderVolume);
                if (CFSMVMListtemp.Count > 1)
                {
                    if (CFSMVMListtemp[0] == SelectedItem)
                    {
                        CFSMVMListtemp[1].OrderVolume = _AllNum - SelectedItem.OrderVolume - num;
                        if (CFSMVMListtemp[1].OrderVolume == 0)
                        {
                            CFSMVMListtemp.Remove(CFSMVMListtemp[1]);
                        }
                    }
                    else if (CFSMVMListtemp[1] == SelectedItem)
                    {
                        CFSMVMListtemp[0].OrderVolume = _AllNum - SelectedItem.OrderVolume - num;
                        if (CFSMVMListtemp[0].OrderVolume == 0)
                        {
                            CFSMVMListtemp.Remove(CFSMVMListtemp[0]);
                        }
                    }
                }
                else if (CFSMVMListtemp.Count == 1)
                {
                    if (CFSMVMListtemp[0] == SelectedItem)
                    {
                        CFSMVMListtemp[1].OrderVolume = _AllNum - SelectedItem.OrderVolume - num;
                        if (CFSMVMListtemp[1].OrderVolume == 0)
                        {
                            CFSMVMListtemp.Remove(CFSMVMListtemp[1]);
                        }
                    }
                    else
                    {
                        CFSMVMListtemp[0].OrderVolume = _AllNum - num;
                        if (CFSMVMListtemp[0].OrderVolume == 0)

                        {
                            CFSMVMListtemp.Remove(CFSMVMListtemp[0]);
                        }
                    }
                }
                foreach (CheckFullStopModelViewModel item in CFSMVMList)
                {
                    if (item.OrderVolume == 0)
                    {
                        CFSMVMList.Remove(item);
                        break;
                    }
                }
            }


        }
        public bool NumChangedCanExecuteChanged()
        {
            return true;
        }

        #endregion
        public Action CloseAction
        {
            get; set;
        }
        public void Close()
        {
            _CheckFullStopViewModel = null;
            CloseAction?.Invoke();
        }

        public void ClearData()
        {
            _CheckFullStopViewModel = null;

        }
    }
}
