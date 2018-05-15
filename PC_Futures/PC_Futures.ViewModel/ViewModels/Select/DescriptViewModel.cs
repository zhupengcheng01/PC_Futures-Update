using Futures.Enum;
using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class DescriptViewModel : ObservableObject
    {
        private string _JSType = "日结算";
        public string JSType
        {
            get
            {
                return _JSType;
            }
            set
            {
                if (_JSType != value)
                {
                    _JSType = value;
                    RaisePropertyChanged("JSType");
                }
            }
        }

        private string _Connet;
        public string Connet
        {
            get
            {
                return _Connet;
            }
            set
            {
                if (_Connet != value)
                {
                    _Connet = value;
                    RaisePropertyChanged("Connet");
                }
            }
        }
        private Visibility _IsShow = Visibility.Collapsed;

        public Visibility IsShow
        {
            get
            {
                return _IsShow;
            }
            set
            {
                if (_IsShow != value)
                {
                    _IsShow = value;
                    RaisePropertyChanged("IsShow");
                }
            }
        }

        private ObservableCollection<RestTodayFundsViewModel> _Funds = new ObservableCollection<RestTodayFundsViewModel>();
        public ObservableCollection<RestTodayFundsViewModel> Funds
        {
            get
            {
                return _Funds;
            }
            set
            {
                if (_Funds != value)
                {
                    _Funds = value;
                    RaisePropertyChanged("Funds");
                }
            }
        }

        private Visibility _IsShowDay = Visibility.Visible;
        public Visibility IsShowDay
        {
            get
            {
                return _IsShowDay;
            }
            set
            {
                if (_IsShowDay != value)
                {
                    _IsShowDay = value;
                    RaisePropertyChanged("IsShowDay");
                }
            }
        }


        private Visibility _IsShowMouth = Visibility.Collapsed;
        public Visibility IsShowMouth
        {
            get
            {
                return _IsShowMouth;
            }
            set
            {
                if (_IsShowMouth != value)
                {
                    _IsShowMouth = value;
                    RaisePropertyChanged("IsShowMouth");
                }
            }
        }
        private string _Date = DateTime.Now.ToString("yyyy-MM-dd");
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        private bool isFrist = false;

        private string _DateMouth = DateTime.Now.ToString("yyyy-MM");
        public string DateMouth
        {
            get
            {
                return _DateMouth;
            }
            set
            {
                if (_DateMouth != value)
                {
                    _DateMouth = value;
                    RaisePropertyChanged("DateMouth");
                }
            }
        }
        private static DescriptViewModel _DescriptViewModel;
        public static DescriptViewModel Instance()
        {
            if (_DescriptViewModel == null)
            {
                _DescriptViewModel = new DescriptViewModel();
            }
            return _DescriptViewModel;
        }

        private DescriptViewModel()
        {
            isFrist = true;
        }
        #region 命令
        /// <summary>
        /// 查询
        /// </summary>
        public ICommand SelectCommand { get { return new RelayCommand(SelectExecuteChanged, SelectCanExecuteChanged); } }
        public void SelectExecuteChanged()
        {
            if (IsShow == Visibility.Collapsed) return;
            ReqDescriptModel rdvm = new ReqDescriptModel();
            RDescriptModel rm = new RDescriptModel();
            if (JSType == "日结算")
            {
                rm.start_date = Convert.ToInt32(Convert.ToDateTime(Date).ToString("yyyyMMdd"));
                rm.end_date = Convert.ToInt32(Convert.ToDateTime(Date).ToString("yyyyMMdd"));
                rm.settle_type = (int)SysSettleType.Sys_SettleDate;
            }
            else
            {
                rm.start_date = Convert.ToInt32(Convert.ToDateTime(DateMouth).ToString("yyyyMM01"));
                rm.end_date = Convert.ToInt32(Convert.ToDateTime(DateMouth).AddMonths(1).ToString("yyyyMM01"));
                rm.settle_type = (int)SysSettleType.Sys_SettleMonth;
            }
            rm.user_id = UserInfoHelper.UserId;
            rdvm.cmdcode = RequestCmdCode.ReqDescript;
            rdvm.content = rm;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rdvm));
        }
        public bool SelectCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 查询
        /// </summary>
        public ICommand TableChange { get { return new RelayCommand(TableChangeChanged, TableChangeCanExecuteChanged); } }
        public void TableChangeChanged()
        {
            if (isFrist)
            {
                IsShow = Visibility.Collapsed;
                isFrist = false;
                return;
            }
            if (IsShow == Visibility.Collapsed)
            {
                IsShow = Visibility.Visible;
            }
            else
            {
                IsShow = Visibility.Collapsed;
            }

        }
        public bool TableChangeCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        public ICommand DataChange { get { return new RelayCommand(DataChangeChanged, DataChangeCanExecuteChanged); } }
        public void DataChangeChanged()
        {

            if (JSType == "日结算")
            {
                IsShowMouth = Visibility.Visible;
                IsShowDay = Visibility.Collapsed;

            }
            else
            {
                IsShowDay = Visibility.Visible;
                IsShowMouth = Visibility.Collapsed;

            }
        }
        public bool DataChangeCanExecuteChanged()
        {
            return true;
        }

        public ICommand ExportCommand { get { return new RelayCommand(ExportExecute, ExportCanExecuteChanged); } }
        public void ExportExecute()
        {
            try
            {
                if (string.IsNullOrEmpty(Connet)) return;
                System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                //frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
                frm.Filter = "(*.txt)|*.txt|" + "(*.*)|*.*";
                string dataname = null;
                if (JSType == "日结算")
                {
                    dataname = Convert.ToDateTime(Date).ToString("yyyy-MM-dd");
                }
                else
                {
                    dataname = Convert.ToDateTime(DateMouth).ToString("yyyy-MM");
                }
                frm.FileName = "结算单" + dataname + ".txt";
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter FileWriter = new StreamWriter(frm.FileName, false); //写文件
                    FileWriter.Write(Connet);//将字符串写入
                    FileWriter.Close(); //关闭StreamWriter对象
                    MessageBox.Show("导出成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //using (StreamReader sr=new StreamReader(frm.FileName))
                //{
                //    string str = sr.ReadToEnd();
                //    Connet = null;
                //    Connet = str;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("导出异常:" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool ExportCanExecuteChanged()
        {
            return true;
        }

        public void GotFocus()
        {
            IsShow = Visibility.Collapsed;
        }
        public void GotFocus1()
        {
            IsShow = Visibility.Visible;
        }
        #endregion
    }
}
