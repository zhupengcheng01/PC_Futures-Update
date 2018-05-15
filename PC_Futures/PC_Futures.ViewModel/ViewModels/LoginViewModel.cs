using MicroMvvm;
using PC_Futures.WebScoket;
using System;
using System.Configuration;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private ScoketManager _scoketManager;
        #region 变量     
        private string _UserName;
        private string _Password;
        private bool _IsSave = false;

        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (value != _UserName)
                {
                    _UserName = value;
                    RaisePropertyChanged("UserName");
                }
            }

        }
        public string Password
        {
            get { return _Password; }
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                    RaisePropertyChanged("Password");
                }
            }

        }
        public bool IsSave
        {
            get { return _IsSave; }
            set
            {
                if (value != _IsSave)
                {
                    _IsSave = value;
                    RaisePropertyChanged("IsSave");
                }
            }

        }
        #endregion

        #region 构造方法
        string UserNameTemp = null;
        string Passwordtemp = null;
        public LoginViewModel()
        {
            UserNameTemp = IniHelper.ProfileReadValue("User", "Username", IniHelper.configpath);
            UserName = UserNameTemp;
            string pwd = IniHelper.ProfileReadValue("User", "Password", IniHelper.configpath);
            if (!string.IsNullOrEmpty(pwd))
            {
                Password = RSAEncryption.Decrypt(pwd);
            }
            Passwordtemp = Password;
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                IsSave = true;
            }
        }
        #endregion

        #region 命令

        public ICommand LoginCommand { get { return new RelayCommand(LoginExecuteChanged, LoginCanExecuteChanged); } }
        public void LoginExecuteChanged()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                MessageBox.Show("用户名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("用户名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(UserNameTemp))
            {
                if (UserNameTemp != UserName || Passwordtemp != Password)
                {
                    MessageBox.Show("用户名或密码不正确!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            _scoketManager = ScoketManager.GetInstance();
            string  socketUrl = ConfigurationManager.AppSettings["QuotesAddress"];
            if (!_scoketManager.StartQuotesSocket(socketUrl))
            {
                MessageBox.Show("行情服务器无法连接,请检查服务器,重新登录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsSave)
            {
                IniHelper.ProfileWriteValue("User", "Username", UserName, IniHelper.configpath);
                string pasword = RSAEncryption.Encrypt(Password);
                IniHelper.ProfileWriteValue("User", "Password", pasword, IniHelper.configpath);
            }
            else
            {
                IniHelper.ProfileWriteValue("User", "Username", "", IniHelper.configpath);
                IniHelper.ProfileWriteValue("User", "Password", "", IniHelper.configpath);
            }
            KeyCodeValue.AddKeys();
            //MainWindow main = new MainWindow();
            //ContractVariety.main = main;
            //main.Show();
            this.Close();

        }
        public bool LoginCanExecuteChanged()
        {
            return true;
        }

        public ICommand ButCloseCommand { get { return new RelayCommand(ButCloseExecuteChanged, ButCloseCanExecuteChanged); } }
        public void ButCloseExecuteChanged()
        {
            //if (MessageBox.Show("是否确定退出", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //    Environment.Exit(0);
            //}
            Environment.Exit(0);
        }
        public bool ButCloseCanExecuteChanged()
        {
            return true;
        }

        public Action CloseAction
        {
            get; set;
        }
        public void Close()
        {
            CloseAction?.Invoke();
        }
        #endregion
    }
}
