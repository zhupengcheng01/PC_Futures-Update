using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.CommonClass;
using PC_Futures.FuturesBLL;
using PC_Futures.Models;
using PC_Futures.ViewModel;
using PC_Futures.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Utility;

namespace PC_Futures
{
    public class TradeLoginViewModel : ObservableObject
    {
        private MainViewModel _mainVM;
        private ScoketManager scoketManager;
        ValidCode validCode;
        private TradeLoginBLL loginBll = new TradeLoginBLL();
        private CommBLL combll = new CommBLL();
        private static TradeLoginViewModel _instance;
        public static TradeLoginViewModel GetInstance(MainViewModel mainVM)
        {
            if (_instance == null)
            {
                _instance = new TradeLoginViewModel(mainVM);
            }
            return _instance;
        }
        public TradeLoginViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;
            scoketManager = ScoketManager.GetInstance();
            GetTxtSiet();
            GetClientUsers();
            CreateImageSource();
        }
        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "UserData.ini";

        //保存文件路径
        /// <summary>
        /// 获取客户端保存的所有用户
        /// </summary>
        private void GetClientUsers()
        {
            try
            {
                string FileInfo = string.Empty;
                if (File.Exists(FilePath))
                {
                    //打开流进行读取
                    using (FileStream fs = File.OpenRead(FilePath))
                    {
                        //创建一个byte数组以读取数据
                        byte[] arr = new byte[fs.Length];
                        //继续读完文件中的所有数据
                        while (fs.Read(arr, 0, arr.Length) > 0)
                        {
                            FileInfo += Encoding.UTF8.GetString(arr);
                        }
                    }
                    FileInfo = FileInfo.Replace('\0', ' ').Trim();
                    if (!string.IsNullOrEmpty(FileInfo))
                    {
                        string fileinfo = RSAEncryption.Decrypt(FileInfo);

                        string[] values = fileinfo.Split(';');
                        foreach (var item in values)
                        {
                            UserInfoData.Add(item);
                        }
                    }
                }
                string user = IniHelper.ProfileReadValue("ServerUser", "user", IniHelper.defaultUserData);
                if (UserInfoData.Count > 0)
                {
                    if (UserInfoData.Contains(user))
                    {
                        UserInfoSelectItem = user;
                        IsSaveUserName = true;
                    }
                    else
                    {
                        UserInfoSelectItem = UserInfoData[0];
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.Message);

            }
        }


        #region 属性
        #region 锁定属性
        private bool _IsSiteEnable = true;
        public bool IsSiteEnable
        {
            get
            {
                return _IsSiteEnable;
            }
            set
            {
                if (_IsSiteEnable != value)
                {
                    _IsSiteEnable = value;
                    RaisePropertyChanged(nameof(IsSiteEnable));
                }
            }
        }
        private bool _IsUserNameEnable = true;
        public bool IsUserNameEnable
        {
            get
            {
                return _IsUserNameEnable;
            }
            set
            {
                if (_IsUserNameEnable != value)
                {
                    _IsUserNameEnable = value;
                    RaisePropertyChanged(nameof(IsUserNameEnable));
                }
            }
        }
        private Visibility _AuthCodeVisibility = Visibility.Visible;
        public Visibility AuthCodeVisibility
        {
            get
            {
                return _AuthCodeVisibility;
            }
            set
            {
                if (_AuthCodeVisibility != value)
                {
                    _AuthCodeVisibility = value;
                    RaisePropertyChanged(nameof(AuthCodeVisibility));
                }
            }
        }
        private string _LoginBtnName = "登录";
        public string LoginBtnName
        {
            get
            {
                return _LoginBtnName;
            }
            set
            {
                if (_LoginBtnName != value)
                {
                    _LoginBtnName = value;
                    RaisePropertyChanged(nameof(LoginBtnName));
                }
            }
        }

        public void SetLockInfo()
        {
            IsSiteEnable = false;
            IsUserNameEnable = false;
            AuthCodeVisibility = Visibility.Collapsed;
            LoginBtnName = "解锁";
            LoginStatus = string.Empty;
            Password = string.Empty;
            UserInfoHelper.IsLock = true;
        }
        public void SetCloseInfo()
        {
            scoketManager.StopTradeTimer();
            scoketManager.Dispose();

            #region 清空下单面板
            ClearTradeList();
            #endregion

            TradeInfoHelper.OptionalModelList.Clear();
            SetOptionalList();
            UserInfoHelper.IsLock = false;
            UserInfoHelper.IsHaveLogin = false;

            IsSiteEnable = true;
            IsUserNameEnable = true;
            AuthCodeVisibility = Visibility.Visible;
            LoginBtnName = "登录";
            LoginStatus = string.Empty;
            Password = string.Empty;
            AuthCode = string.Empty;
            CreateImageSource();

            ServerStatusInfoHelper.Instace().TradeServerStatus = string.Empty;


        }
        #endregion
        private ObservableCollection<string> _UserInfoData = new ObservableCollection<string>();
        public ObservableCollection<string> UserInfoData
        {
            get { return _UserInfoData; }
            set
            {
                if (_UserInfoData == value) return;
                _UserInfoData = value;
                RaisePropertyChanged("UserInfoData");
            }
        }
        private string _UserInfoSelectItem;
        public string UserInfoSelectItem
        {
            get { return _UserInfoSelectItem; }
            set
            {
                if (_UserInfoSelectItem == value) return;
                _UserInfoSelectItem = value;
                RaisePropertyChanged("UserInfoSelectItem");
            }
        }

        private List<string> _locationRoad = new List<string>();
        public List<string> LocationSource
        {
            get { return _locationRoad; }
            set
            {
                if (_locationRoad == value) return;
                _locationRoad = value;
                RaisePropertyChanged(nameof(LocationSource));
            }
        }
        private string _SelectItem = null;
        public string SelectItem
        {
            get { return _SelectItem; }
            set
            {
                if (value != _SelectItem)
                {
                    _SelectItem = value;
                    if (_SelectItem != null)
                    {
                        string strTemp = _SelectItem as string;
                        var SiteTempObj = _listSiteTemp.FirstOrDefault(o => o.Site == strTemp);
                        if (SiteTempObj != null)
                        {
                            scoketManager._titetemp = SiteTempObj;
                        }
                    }
                    RaisePropertyChanged(nameof(SelectItem));
                }
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }
        private bool _isSaveUserName;
        public bool IsSaveUserName
        {
            get
            {
                return _isSaveUserName;
            }
            set
            {
                _isSaveUserName = value;
                RaisePropertyChanged(nameof(IsSaveUserName));
            }
        }
        private BitmapFrame _imageSource;
        public BitmapFrame ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
        private string _mac = string.Empty;
        public string MAC
        {
            get
            {
                try
                {
                    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    if (interfaces.Length > 0)
                    {
                        string temp = interfaces[0].GetPhysicalAddress().ToString();
                        _mac = temp.Substring(0, 2) + ":" + temp.Substring(2, 2) + ":" + temp.Substring(4, 2) + ":" + temp.Substring(6, 2) + ":" + temp.Substring(8, 2) + ":" + temp.Substring(10, 2);

                    }
                }
                catch (Exception ex)
                {

                    LogHelper.Info(ex.Message);
                }
                return _mac;
            }
        }
        private string _ip;
        public string IP
        {
            get
            {
                if (string.IsNullOrEmpty(_ip))
                {
                    string HostName = Dns.GetHostName(); //得到主机名
                    IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                    for (int i = 0; i < IpEntry.AddressList.Length; i++)
                    {
                        //从IP地址列表中筛选出IPv4类型的IP地址
                        //AddressFamily.InterNetwork表示此IP为IPv4,
                        //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                        if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            _ip = IpEntry.AddressList[i].ToString();
                            break;
                        }
                    }
                }
                return _ip;
            }
        }
        private string _authCode;
        public string AuthCode
        {
            get
            {
                return _authCode;
            }
            set
            {
                _authCode = value;
                RaisePropertyChanged(nameof(AuthCode));
            }
        }
        private string _loginStatus;
        public string LoginStatus
        {
            get
            {
                return _loginStatus;
            }
            set
            {
                if (_loginStatus != value)
                {
                    _loginStatus = value;
                    RaisePropertyChanged(nameof(LoginStatus));
                }

            }
        }
        private bool _loginBtnIsEnabled = true;
        public bool LoginBtnIsEnabled
        {
            get
            {
                return _loginBtnIsEnabled;
            }
            set
            {
                _loginBtnIsEnabled = value;
                RaisePropertyChanged(nameof(LoginBtnIsEnabled));
            }
        }
        #endregion

        public ICommand LoginCommand { get { return new RelayCommand(LoginExecuteChanged, LoginCanExecuteChanged); } }
        public void LoginExecuteChanged()
        {
            //LogHelper.Info("登录开始");
            //var model= _mainVM.VarietyList.FirstOrDefault().Value[0];
            //QuotesTabControlViewModel.GetInstance(null).ScrollIntoView(model);
            if (UserInfoHelper.IsLock)
            {
                UnLockDoLogin();
            }
            else
            {
                DoLoginTemp();
            }

        }
        public bool LoginCanExecuteChanged()
        {
            return true;
        }
        public ICommand ExitCommand { get { return new RelayCommand(ExitExecuteChanged, ExitCanExecuteChanged); } }
        public void ExitExecuteChanged()
        {
            _mainVM.TradeVisibility = Visibility.Collapsed;

        }
        public bool ExitCanExecuteChanged()
        {
            return true;
        }

        public void DeleteUserChanged(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                UserInfoData.Remove(username);
            }
        }

        #region 方法
        private void DoLoginTemp()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("请输入账号/密码！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(AuthCode))
            {
                MessageBox.Show("请输入验证码！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (AuthCode.ToUpper() == validCode.ReturnCode.ToUpper())
            {
                scoketManager.Dispose();
                LoginBtnIsEnabled = false;
                if (!scoketManager.StartTradeSocket(true))
                {
                    LoginBtnIsEnabled = true;
                    ServerStatusInfoHelper.Instace().TradeServerStatus = string.Empty;
                    MessageBox.Show("服务器无法连接,请检查服务器,重新登录!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                loginBll.SendLoginInfo(UserName, Password, MAC, IP);
            }
            else
            {
                AuthCode = string.Empty;
                CreateImageSource();
                MessageBox.Show("验证码输入有误，请检查后输入！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            SetClientUsers(UserName);
        }
        private void UnLockDoLogin()
        {
            if (!string.Equals(Password, UserInfoHelper.Password))
            {
                MessageBox.Show("解锁密码不正确，请重新输入！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            UserInfoHelper.IsLock = false;
            _mainVM.TradeLoginVisibility = Visibility.Collapsed;
        }
        /// <summary>
        /// 设置记录客户端登录成功的用户
        /// </summary>
        /// <param name="UserInfo"></param>
        private void SetClientUsers(string data)
        {
            try
            {
                if (IsSaveUserName)
                {
                    if (!UserInfoData.Contains(UserName))
                    {
                        UserInfoData.Add(UserName);
                    }
                    WriteFileStream();
                    IniHelper.ProfileWriteValue("ServerUser", "server", SelectItem, IniHelper.defaultUserData);
                    IniHelper.ProfileWriteValue("ServerUser", "user", UserName, IniHelper.defaultUserData);
                }
            }
            catch
            {
                LogHelper.Info("设置客户端用户错误");
            }
        }


        /// <summary>
        /// 向文件中写入数据流
        /// </summary>
        private void WriteFileStream()
        {
            try
            {
                string saveStr = string.Empty;
                foreach (var da in UserInfoData)
                {
                    saveStr += da + ";";
                }
                saveStr = RSAEncryption.Encrypt(saveStr.TrimEnd(';'));
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] bytes = new byte[saveStr.ToArray().Length];
                    bytes = Encoding.UTF8.GetBytes(saveStr);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void HandleRecvLogin(ResultTradeLoginModel resultModel)
        {
            if (resultModel.errcode == 0)
            {
                if (!LoginBtnIsEnabled)
                {
                    if (resultModel.content != null)
                    {
                        UserInfoHelper.LoginName = UserName;
                        UserInfoHelper.Password = Password;
                        UserInfoHelper.MAC = MAC;
                        UserInfoHelper.IP = IP;
                        UserInfoHelper.InstitudId = resultModel.content.institud_id;
                        UserInfoHelper.UserId = resultModel.content.user_id;
                    }
                    //请求自选
                    combll.ReqTcp(RequestCmdCode.RequestOptional, resultModel.content.user_id);
                  
                }
                else
                {

                    SendPotion();
                    SendOrderCancel();
                    SendConditionBill();
                    ToDayTrade();
                }
            }
            else
            {
                LoginBtnIsEnabled = true;
                MessageBox.Show(resultModel.errmsg, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void SendPotion(bool isClear = true)
        {

            ReqPotion rpd = new ReqPotion();
            rpd.cmdcode = RequestCmdCode.SelectPotionDetialCode;
            rpd.content = new ReqLoginName() { user_id = UserInfoHelper.UserId };
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));
            if (isClear)
            {
                PositionAllViewModel.Instance().DetPMList.Clear();
                PositionAllViewModel.Instance().PMList.Clear();
                PositionViewModel.Instance().PMList.Clear();
            }
        }
        public void ClearTradeList()
        {
            PositionAllViewModel.Instance().DetPMList.Clear();
            PositionAllViewModel.Instance().PMList.Clear();
            PositionViewModel.Instance().PMList.Clear();

            OrderCancelViewModel.Instance().KCDelegations.Clear();
            OrderCancelViewModel.Instance().Delegations.Clear();

            UCConditionBillViewModel.Instance().ConditionBillList.Clear();

            TodayTraderViewModels.Instance().TodayTraderList.Clear();
        }

        private void SendOrderCancel(bool isClear = true)
        {
            ReqPotion rpd = new ReqPotion();
            rpd.cmdcode = RequestCmdCode.SelectOrderCancel;
            rpd.content = new ReqLoginName() { user_id = UserInfoHelper.UserId };
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));
            if (isClear)
            {
                OrderCancelViewModel.Instance().KCDelegations.Clear();
                OrderCancelViewModel.Instance().Delegations.Clear();
            }
        }
        private void SendConditionBill(bool isClear = true)
        {
            ReqPotion rp = new ReqPotion();
            rp.cmdcode = RequestCmdCode.SelectConditionBill;
            rp.content = new ReqLoginName()
            {
                user_id = UserInfoHelper.UserId
            };

            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));
            if (isClear)
            {
                UCConditionBillViewModel.Instance().ConditionBillList.Clear();
            }
        }

        private void ToDayTrade(bool isClear = true)
        {
            //成交明细
            ReqPotion rp = new ReqPotion();
            rp.cmdcode = RequestCmdCode.ToDayTradeCode;
            rp.content = new ReqLoginName()
            {
                user_id = UserInfoHelper.UserId
            };

            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));
            if (isClear)
            {
                TodayTraderViewModels.Instance().TodayTraderList.Clear();
            }
        }

        internal void ReqParities()
        {
            // Thread.Sleep(2000);
            ReqPotion rpd = new ReqPotion();
            rpd.cmdcode = RequestCmdCode.SelectParities;
            rpd.content = null;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));
        }
        /// <summary>
        /// 请求合约品种
        /// </summary>
        private void ReqVariety()
        {
            // Thread.Sleep(2000);
            ReqPotion rpd = new ReqPotion();
            rpd.cmdcode = RequestCmdCode.SelectVariety;
            rpd.content = null;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));

        }
        /// <summary>
        /// 请求收费方式
        /// </summary>
        public void ReqDetType()
        {
            // Thread.Sleep(2000);
            ReqPotion rpd = new ReqPotion();
            rpd.cmdcode = RequestCmdCode.ReqCalcDeposit;
            rpd.content = null;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));

        }
        /// <summary>
        /// 请求保证金
        /// </summary>
        public void ReqMargin()
        {
            ReqPotion rp = new ReqPotion();
            rp.cmdcode = RequestCmdCode.ReqMargin;
            rp.content = new ReqLoginName()
            {
                user_id = UserInfoHelper.UserId
            };
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));

        }
        public void HandleOptional()
        {
            //请求资金
            combll.ReqTcp(RequestCmdCode.SelectFundsCode, UserInfoHelper.UserId);
        }
        public void HandleFunds()
        {
            ReqVariety();
        }
        /// <summary>
        /// 登录成功
        /// </summary>
        public void LoginSuccess()
        {
            DataTable dt = SQLiteHelper.ExecuteDataset(SQLiteHelper.DBPath, CommandType.Text, "select * from AutoStopLoss where UserID='" + UserInfoHelper.UserId + "'").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string ContractID = dr["ContractID"].ToString();
                    string PostionID = dr["PostionID"].ToString();
                    object lossprice = dr["LossPrice"];
                    double losspriced = 0;
                    if (lossprice != null)
                    {
                        losspriced = Convert.ToDouble(lossprice);
                    }
                    object newprice = dr["NewPrice"];
                    double newpriced = 0;
                    if (lossprice != null)
                    {
                        newpriced = Convert.ToDouble(lossprice);
                    }
                    if (!ContractVariety.ContracPostionID.ContainsKey(ContractID))
                    {
                        continue;//不在设置的合约中就不添加进来
                    }
                    ContractVariety.ContracPostionID[ContractID].Add(PostionID);
                    if (!ContractVariety.PostionPrice.ContainsKey(PostionID))
                    {
                        ContractVariety.PostionPrice.Add(PostionID, new AutoLossPrice());
                    }
                    ContractVariety.PostionPrice[PostionID].LossPrice = losspriced;
                    ContractVariety.PostionPrice[PostionID].NewPrice = newpriced;
                }
            }
            LoginBtnIsEnabled = true;
            //给界面自选赋值
            SetOptionalList();
            _mainVM.TradePanelViewModel = TradePanelViewModel.GetInstance(_mainVM);
            _mainVM.TradeLoginVisibility = Visibility.Collapsed;
            ContractVariety.IsLoginSuccess = true;
            UserInfoHelper.IsHaveLogin = true;
            SendPotion(false);
            SendOrderCancel(false);
            SendConditionBill(false);
            ToDayTrade(false);
            //给界面资金赋值
            var fundsmodel = FundsViewModel.GetInstance();
            var dataModel = TradeInfoHelper.FundsDataModel;
            fundsmodel.LoginName = UserInfoHelper.LoginName;
            fundsmodel.AccountName = UserInfoHelper.AccountName;
            fundsmodel.AbleFund = dataModel.able_fund;
            fundsmodel.CurrentEquity = dataModel.current_equity;
            fundsmodel.Floatprofitloss = dataModel.float_profit_loss;
            if (_mainVM.SelectItemViewModel != null)
            {
                TransactionViewModel.Instance().SetXamlValues(_mainVM.SelectItemViewModel);
            }
        }

        /// <summary>
        /// 请求资金
        /// </summary>
        private void RequestFunds()
        {
            //ReqPotion rp = new ReqPotion();
            //rp.cmdcode = RequestCmdCode.SelectPotionCode;
            //rp.content = new ReqLoginName() { user_id = UserInfoHelper.LoginName, resource = (int)OperatorTradeType.OPERATOR_TRADE_PC };
        }

        List<SiteTemp> _listSiteTemp = new List<SiteTemp>();
        /// <summary>
        /// 获取文件站点
        /// </summary>
        /// <returns></returns>
        private void GetTxtSiet()
        {
            try
            {
                string CurDir = System.AppDomain.CurrentDomain.BaseDirectory + @"FuturesSite\";    //设置当前目录  
                if (!Directory.Exists(CurDir)) Directory.CreateDirectory(CurDir);   //该路径不存在时，在当前文件目录下创建文件夹
                string filePath = CurDir + "TradeSite.ini";
                SiteTemp siteMode;
                string str = string.Empty;
                //获取文件内容  
                if (File.Exists(filePath))
                {
                    StreamReader file1 = new StreamReader(filePath, System.Text.Encoding.Default);//读取文件中的数据  
                    str = file1.ReadToEnd();
                    file1.Close();
                    file1.Dispose();
                    //List<string> _listSite = new List<string>();
                    string[] _listSite = str.Replace("\r\n", "").Split(';');
                    for (int i = 0; i < _listSite.Count() - 1; i++)
                    {
                        siteMode = new SiteTemp();
                        string[] _SiteTemp = _listSite[i].Split(',');
                        siteMode.Site = _SiteTemp[0];
                        _locationRoad.Add(_SiteTemp[0]);
                        siteMode.IP = _SiteTemp[1].Substring(_SiteTemp[1].IndexOf(":") + 1);
                        siteMode.IPPort = _SiteTemp[2].Substring(_SiteTemp[2].IndexOf(":") + 1);
                        _listSiteTemp.Add(siteMode);
                    }

                }
                string site = IniHelper.ProfileReadValue("ServerUser", "server", IniHelper.defaultUserData);
                if (!string.IsNullOrEmpty(site))
                {
                    if (_locationRoad.Contains(site))
                    {
                        SelectItem = site;
                    }
                    else
                    {
                        SelectItem = _locationRoad[0];
                    }
                }
                else
                {
                    SelectItem = _locationRoad[0];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("站点获取失败，请检查站点配置！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void CreateImageSource()
        {
            validCode = new ValidCode(4, ValidCode.CodeType.Numbers);
            ImageSource = BitmapFrame.Create(validCode.CreateCheckCodeImage());
        }

        public void SetOptionalList()
        {
            if (TradeInfoHelper.OptionalModelList.Count > 0)
            {
                List<FuturesViewModel> list = new List<FuturesViewModel>();
                int seq = 0;
                foreach (var item in TradeInfoHelper.OptionalModelList)
                {
                    var datamodel = TradeInfoHelper.ContractModelList.FirstOrDefault(o => string.Equals(o.contractCode, item.contract_id));
                    if (datamodel != null)
                    {
                        seq++;
                        var viewModel = new FuturesViewModel(datamodel);
                        viewModel.Seq = seq;
                        viewModel.IsOptionalStock = true;
                        viewModel.OptionalSerialNumber = item.serial_number;
                        QuotationEntity quotesmodel = null;
                        lock (TradeInfoHelper.SubscribedContractList)
                        {
                            quotesmodel = TradeInfoHelper.SubscribedContractList.FirstOrDefault(o => string.Equals(o.cd, item.contract_id));
                        }
                        if (quotesmodel != null)
                        {
                            scoketManager.UpdateModelInfo(viewModel, quotesmodel);
                        }
                        list.Add(viewModel);
                    }
                }
                TradeQuotesViewModel.GetInstance(null).OptionalList = list;

            }
            else
            {
                TradeQuotesViewModel.GetInstance(null).OptionalList.Clear();
            }
            TradeQuotesViewModel.GetInstance(null).SetOptionalList();
        }
        #endregion
    }
}
