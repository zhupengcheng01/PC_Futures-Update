using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PC_Futures.ViewModel
{
    public class AutoStopLossModelViewModel : ObservableObject
    {

        public AutoStopLossModelViewModel()
        {

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
                 
                    VarietyChangedExecuteChanged();
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
        //private string _Variety;
        ///// <summary>
        ///// 合约号
        ///// </summary>
        //public string Variety
        //{
        //    get { return _Variety; }
        //    set
        //    {
        //        if (_Variety != value)
        //        {
        //            _Variety = value;
        //            RaisePropertyChanged("Variety");
        //        }
        //    }
        //}

        private string _Agreement;
        /// <summary>
        /// 合约号
        /// </summary>
        public string Agreement
        {
            get { return _Agreement; }
            set
            {
                if (_Agreement != value)
                {
                    _Agreement = value;
                    RaisePropertyChanged("Agreement");
                }
            }
        }
        private string _Direction;
        /// <summary>
        /// 方向
        /// </summary>
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

        private int _StopLossPotion;

        /// <summary>
        /// 止损点位
        /// </summary>
        public int StopLossPotion
        {
            get { return _StopLossPotion; }
            set
            {
                if (_StopLossPotion != value)
                {
                    _StopLossPotion = value;
                    RaisePropertyChanged("StopLossPotion");
                }
            }
        }
        private int _StopProfitPotion;
        /// <summary>
        /// 止盈点位
        /// </summary>
        public int StopProfitPotion
        {
            get { return _StopProfitPotion; }
            set
            {
                if (_StopProfitPotion != value)
                {
                    _StopProfitPotion = value;
                    RaisePropertyChanged("StopProfitPotion");
                }
            }
        }

        private int _FloatingProfitAndLoss;
        /// <summary>
        /// 浮动盈亏
        /// </summary>
        public int FloatingProfitAndLoss
        {
            get { return _FloatingProfitAndLoss; }
            set
            {
                if (_FloatingProfitAndLoss != value)
                {
                    _FloatingProfitAndLoss = value;
                    RaisePropertyChanged("FloatingProfitAndLoss");
                }
            }
        }

        /// <summary>
        /// 选择品种
        /// </summary>
        public ICommand VarietyChangedCommand { get { return new RelayCommand(VarietyChangedExecuteChanged, VarietyChangedCanExecuteChanged); } }
        public void VarietyChangedExecuteChanged()
        {

            if (VarietySelectedItem != null)
            {
               ContractCode = MainViewModel.GetInstance().VarietyList[VarietySelectedItem].ToList();
                Agreement = null;
            }

        }
        public bool VarietyChangedCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 选择合约
        /// </summary>
        public ICommand AgreementChangedCommand { get { return new RelayCommand(AgreementChangedExecuteChanged, AgreementChangedCanExecuteChanged); } }
        public void AgreementChangedExecuteChanged()
        {

            if (ContractCodeSelectedItem != null)
            {
             //   QuotesTabControlViewModel.GetInstance(null).ScrollIntoView(ContractCodeSelectedItem);
               Agreement = ContractCodeSelectedItem.SystemName;

            }

        }
        public bool AgreementChangedCanExecuteChanged()
        {
            return true;
        }
    }
}
