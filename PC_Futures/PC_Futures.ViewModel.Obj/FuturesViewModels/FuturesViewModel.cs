using MicroMvvm;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System.Windows.Input;
using Utilities;

namespace PC_Futures
{
    public class FuturesViewModel : ObservableObject
    {
       
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
          AddOptional(ContractCode);
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
           DelOptional(OptionalSerialNumber);
        }
        public bool DelCanExecuteChanged()
        {
            return true;
        }

        private ScoketManager scoketManager;
      
        public void AddOptional(string contractid)
        {
            scoketManager = ScoketManager.GetInstance();
            if (string.IsNullOrEmpty(UserInfoHelper.UserId))
            {              
                return;
            }
            string msg = "{\"cmdcode\":2246,\"content\":{\"user_id\":\"" + UserInfoHelper.UserId + "\",\"contract_id\":\"" + contractid + "\"}}";
            scoketManager.SendTradeWSInfo(msg);
        }
        public void DelOptional(string serialnumber)
        {
            scoketManager = ScoketManager.GetInstance();
            if (string.IsNullOrEmpty(serialnumber))
                return;
            string msg = "{\"cmdcode\":2248,\"content\":{\"serial_number\":\"" + serialnumber + "\"}}";
            scoketManager.SendTradeWSInfo(msg);
        }
    }
}
