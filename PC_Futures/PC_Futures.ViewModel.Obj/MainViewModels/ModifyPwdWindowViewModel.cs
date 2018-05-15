using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PC_Futures.ViewModel
{
    public class ModifyPwdWindowViewModel : ObservableObject
    {
        public bool IsEdit { get; set; }
        private static ModifyPwdWindowViewModel _instance;
        public static ModifyPwdWindowViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ModifyPwdWindowViewModel();
            }
            return _instance;
        }
        private string _OldPassword;
        public string OldPassword
        {
            get
            {
                return _OldPassword;
            }
            set
            {
                _OldPassword = value;
                RaisePropertyChanged(nameof(OldPassword));
            }
        }
        private string _NewPassword;
        public string NewPassword
        {
            get
            {
                return _NewPassword;
            }
            set
            {
                _NewPassword = value;
                RaisePropertyChanged(nameof(NewPassword));
            }
        }
        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _ConfirmPassword;
            }
            set
            {
                _ConfirmPassword = value;
                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }
        public Action CloseAction
        {
            get; set;
        }
        public void Close()
        {
            _instance = null;
            CloseAction?.Invoke();
        }

        public ICommand ModifyPwdCommand { get { return new RelayCommand(ModifyPwdExecuteChanged, ModifyPwdCanExecuteChanged); } }
        public void ModifyPwdExecuteChanged()
        {
            if (!string.Equals(OldPassword, UserInfoHelper.Password))
            {
                MessageBox.Show("原密码输入不正确！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                MessageBox.Show("新密码不能为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!string.Equals(NewPassword, ConfirmPassword))
            {
                MessageBox.Show("两次输入的密码不一致！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            IsEdit = true;
            RequestModifyPwd dataModel = new RequestModifyPwd();
            dataModel.cmdcode = RequestCmdCode.RequestModifyPwd;
            dataModel.content = new ModifyPwd()
            {
                user_id = UserInfoHelper.UserId,
                old_password = OldPassword,
                new_password = NewPassword,
                operator_id = UserInfoHelper.LoginName,
                resource = (int)OperatorTradeType.OPERATOR_TRADE_PC
            };
            string msg = JsonConvert.SerializeObject(dataModel);
            ScoketManager.GetInstance().SendTradeWSInfo(msg);
        }
        public bool ModifyPwdCanExecuteChanged()
        {
            return true;
        }

        public void HandlePwd(ResultModifyPwdModel pm)
        {
            if (pm != null)
            {
                if (pm.errcode == 0)
                {
                    if (pm.content != null)
                    {
                        if (IsEdit)
                        {
                            IsEdit = false;
                            Close();
                        }
                    }
                }
            }
        }
    }
}
