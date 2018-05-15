using MicroMvvm;
using PC_Futures.FuturesBLL;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PC_Futures
{
    public class FuturesViewModel : ObservableObject
    {
        private ContractBLL contractBll = new ContractBLL();
        private ContractModel _contractModel;
        public FuturesViewModel(ContractModel contractModel)
        {
            _contractModel = contractModel;
        }
        private int _seq;
        /// <summary>
        /// 序号
        /// </summary>
        public int Seq
        {
            get
            {
                return _seq;
            }
            set
            {
                _seq = value;
                RaisePropertyChanged(nameof(Seq));
            }
        }
        //private int _RowFontSize=25;
        ///// <summary>
        ///// 行字体大小
        ///// </summary>
        //public int RowFontSize
        //{
        //    get
        //    {
        //        return _RowFontSize;
        //    }
        //    set
        //    {
        //        if(_RowFontSize!=value)
        //        {
        //            _RowFontSize = value;
        //            RaisePropertyChanged(nameof(RowFontSize));
        //        }
        //    }
        //}
        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractCode
        {
            get
            {
                return _contractModel.contractCode;
            }
        }
        /// <summary>
        /// 合约名称
        /// </summary>
        public string ContractName
        {
            get
            {
                return _contractModel.contractName;
            }
        }
        /// <summary>
        /// 合约代码(面板需要)
        /// </summary>
        public string ContractCodeT
        {
            get
            {
         
                string contractCodeTemp = _contractModel.contractCode.Substring(_contractModel.contractCode.IndexOf(' '));
                return "("+contractCodeTemp.Replace(" ", "")+")";
            }
        }

        /// <summary>
        /// 品种代码
        /// </summary>
        public string ProductCode
        {
            get
            {
                return _contractModel.productCode;
            }
        }
        /// <summary>
        /// 品种名称
        /// </summary>
        public string ProductName
        {
            get
            {
                return _contractModel.productName;
            }
        }
        /// <summary>
        /// 所属页签
        /// </summary>
        public string ExchangeDisplay
        {
            get
            {
                return _contractModel.exchangeDisplay;
            }
        }
        /// <summary>
        /// 交割日
        /// </summary>
        public string DeliveryDate
        {
            get
            {
                return _contractModel.deliveryDate;
            }
        }
        /// <summary>
        /// 有效标识
        /// </summary>
        public int? Status
        {
            get
            {
                return _contractModel.status;
            }
        }
        private bool _isOptionalStock;
        //是否为自选合约
        public bool IsOptionalStock
        {
            get { return _isOptionalStock; }
            set
            {
                if (_isOptionalStock == value) return;
                _isOptionalStock = value;
                RaisePropertyChanged(nameof(IsOptionalStock));
            }

        }
        private string _OptionalSerialNumber;
        //自选唯一标识
        public string OptionalSerialNumber
        {
            get { return _OptionalSerialNumber; }
            set
            {
                if (_OptionalSerialNumber == value) return;
                _OptionalSerialNumber = value;
                RaisePropertyChanged(nameof(OptionalSerialNumber));
            }

        }

        private FuturesTickViewModel _tick;
        public FuturesTickViewModel Tick
        {
            get
            {
                if (_tick == null)
                    _tick = new FuturesTickViewModel(this);
                return _tick;
            }
        }

        /// <summary>
        /// 加入自选
        /// </summary>
        public ICommand AddCommand { get { return new RelayCommand(AddExecuteChanged, AddCanExecuteChanged); } }
        public void AddExecuteChanged()
        {
            contractBll.AddOptional(ContractCode);
        }
        public bool AddCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 删除自选
        /// </summary>
        public ICommand DelCommand { get { return new RelayCommand(DelExecuteChanged, DelCanExecuteChanged); } }
        public void DelExecuteChanged()
        {
            contractBll.DelOptional(OptionalSerialNumber);
        }
        public bool DelCanExecuteChanged()
        {
            return true;
        }

    }
}
