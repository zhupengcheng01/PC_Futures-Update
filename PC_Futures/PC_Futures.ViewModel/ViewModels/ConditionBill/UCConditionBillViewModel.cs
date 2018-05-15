
using Futures.Enum;
using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class UCConditionBillViewModel : ObservableObject
    {
        private ObservableCollection<ConditionBillModelViewModel> _ConditionBillList = new ObservableCollection<ConditionBillModelViewModel>();
        public ObservableCollection<ConditionBillModelViewModel> ConditionBillList
        {
            get { return _ConditionBillList; }
            set
            {
                if (_ConditionBillList != value)
                {
                    _ConditionBillList = value;
                    RaisePropertyChanged("ConditionBillList");
                }
            }

        }
        private ConditionBillModelViewModel _SelectedItem;
        public ConditionBillModelViewModel SelectedItem
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

        private static UCConditionBillViewModel _UCConditionBillViewModel;

        public static UCConditionBillViewModel Instance()
        {
            if (_UCConditionBillViewModel == null)
            {
                _UCConditionBillViewModel = new UCConditionBillViewModel();
            }
            return _UCConditionBillViewModel;
        }


        private UCConditionBillViewModel()
        {
            //LoadData();
        }

        public void LoadData()
        {
            //条件单
            ReqPotion rp = new ReqPotion();
            rp.cmdcode = RequestCmdCode.SelectConditionBill;
            rp.content = new ReqLoginName()
            {
                user_id = UserInfoHelper.UserId
            };

            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));
        }



        #region 命令
        /// <summary>
        /// 删除  
        /// </summary>
        public ICommand DeleteCommand { get { return new RelayCommand(OrderCancelExecuteChanged, OrderCancelCanExecuteChanged); } }
        public void OrderCancelExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择删除的行!", "提示");
                return;
            }
            if (MessageBox.Show("是否确定删除该条件单?", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                DeleteCondtionModel de = new DeleteCondtionModel();
                de.cmdcode = RequestCmdCode.DeleteConditionBill;

                DeleteModel dm = new DeleteModel();
                dm.condition_orderID = SelectedItem.ConditionOrderID;
                dm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
                dm.user_id = UserInfoHelper.UserId;
                de.content = dm;
                ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(de));
            }
        }
        public bool OrderCancelCanExecuteChanged()
        {
            return true;
        }


        /// <summary>
        /// 修改
        /// </summary>
        public ICommand UPDateCommand { get { return new RelayCommand(UPDateExecuteChanged, UPDateCanExecuteChanged); } }
        public void UPDateExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择修改的行!", "提示");
                return;
            }
            if (SelectedItem.Status != (int)ConditionStateType.CONDITION_STATE_WORKING)
            {
                MessageBox.Show("当前状态不可修改!", "提示");
                return;
            }
            //ConditionBill cb = new ConditionBill(SelectedItem);
            //cb.ShowDialog();
        }
        public bool UPDateCanExecuteChanged()
        {
            return true;
        }
        public ICommand SelectionChangedCommand { get { return new RelayCommand(SelectionChangedExecuteChanged, SelectionChangedCanExecuteChanged); } }
        public void SelectionChangedExecuteChanged()
        {
            if (SelectedItem == null) return;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId);

        }
        public bool SelectionChangedCanExecuteChanged()
        {
            return true;
        }

        public ICommand ExportCommand { get { return new RelayCommand(ExportExecute, ExportCanExecuteChanged); } }
        public void ExportExecute()
        {
           // ExprotHelper.ExportToExcelTemp(ConditionBillList, "条件单", 5);
        }
        public bool ExportCanExecuteChanged()
        {
            return true;
        }
        #endregion

        public void Sorting(string name, bool isDesc)
        {
            List<ConditionBillModelViewModel> temp = null;
            switch (name)
            {
                case "ContractCode":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.ContractCode).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.ContractCode).ToList();
                    }

                    break;
                case "Direction":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.Direction).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.Direction).ToList();
                    }

                    break;
                case "OpenOffset":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.OpenOffset).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.OpenOffset).ToList();
                    }

                    break;
                case "Status":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.Status).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.Status).ToList();
                    }

                    break;
                case "TrrigerCond":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.TrrigerCond).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.TrrigerCond).ToList();
                    }

                    break;
                case "OrderPrice":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.OrderPrice).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.OrderPrice).ToList();
                    }

                    break;
                case "OrderVolume":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.OrderVolume).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.OrderVolume).ToList();
                    }

                    break;
                case "CreateTime":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.CreateTime).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.CreateTime).ToList();
                    }

                    break;
                case "TrrigerTime":
                    if (isDesc)
                    {
                        temp = ConditionBillList.OrderBy(x => x.TrrigerTime).ToList();
                    }
                    else
                    {
                        temp = ConditionBillList.OrderByDescending(x => x.TrrigerTime).ToList();
                    }

                    break;

            }
            ConditionBillList.Clear();
            foreach (var item in temp)
            {
                ConditionBillList.Add(item);
            }
        }
    }
}
