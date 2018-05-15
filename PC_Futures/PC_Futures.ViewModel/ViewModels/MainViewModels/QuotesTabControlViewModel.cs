using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class QuotesTabControlViewModel : ObservableObject
    {
        private MainViewModel _mainVM;
        public event EventHandler ContractChanged;
        private static QuotesTabControlViewModel _instance;
        public static QuotesTabControlViewModel GetInstance(MainViewModel mainVM)
        {
            if (_instance == null)
            {
                _instance = new QuotesTabControlViewModel(mainVM);
            }
            return _instance;
        }
        public QuotesTabControlViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;
            SetFuturesList();
        }
        private List<FuturesViewModel> _FuturesViewModelList = new List<FuturesViewModel>();
        public List<FuturesViewModel> FuturesViewModelList
        {
            get
            {
                return _FuturesViewModelList;
            }
        }
        #region 属性

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    //_mainVM.SelectItemViewModel = null;
                    SetSelectItem(_selectedIndex);
                    RaisePropertyChanged(nameof(SelectedIndex));


                }
            }
        }
        public Action<int> SetSelectItemAction { get; set; }
        public void SetSelectItem(int index)
        {
            SetSelectItemAction?.Invoke(index);
        }
        #endregion

        #region 方法
        public void SetFuturesList()
        {
            foreach (var item in CommHelper.ContractModelGroupList)
            {
                _FuturesViewModelList.AddRange(item.Value);
                for (int i = 0; i < item.Value.Count; i++)
                {
                    item.Value[i].Seq = i + 1;
                    var SysVarietyCodeName = item.Value[i].ProductCode + " " + item.Value[i].ProductName;
                    if (!_mainVM.VarietyList.ContainsKey(SysVarietyCodeName))
                    {
                        _mainVM.VarietyList.Add(SysVarietyCodeName, new List<SysCodeModel>());
                    }
                    SysCodeModel model = new SysCodeModel() { SystemName = item.Value[i].ContractCode, SysVarietyCode = item.Value[i].ContractCode };
                    _mainVM.VarietyList[SysVarietyCodeName].Add(model);
                }
            }
        }


        public void ScrollIntoView(SysCodeModel stock)
        {
            if (stock == null || (_mainVM.SelectItemViewModel != null && string.Equals(_mainVM.SelectItemViewModel.ContractCode, stock.SystemName)))
                return;
            if (!TradeQuotesViewModel.GetInstance(null).IsInternationalCheck)
            {
                TradeQuotesViewModel.GetInstance(null).InternationalCheckType = 1;
                TradeQuotesViewModel.GetInstance(null).IsInternationalCheck = true;
            }
            ContractChanged?.Invoke(stock, EventArgs.Empty);
        }
        #endregion
    }
}
