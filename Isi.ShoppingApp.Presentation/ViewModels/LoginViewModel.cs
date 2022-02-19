using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Domain.Services;
using Isi.ShoppingApp.Presentation.Views;
using Isi.Utility.Results;
using Isi.Utility.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Isi.ShoppingApp.Presentation.ViewModels
{
    public class LoginViewModel : ViewModel, INotifyPropertyChanged
    {
        public event Action<string> CommandFailed;
        private UserService userService;

        //Data fields
        public Window LoginViewWindow;
        private string userNameText;
        public string UserNameText
        {
            get => userNameText;
            set
            {
                userNameText = value;
                NotifyPropertyChanged(nameof(UserNameText));
                LoginCommand.NotifyCanExecuteChanged();
            }
        }
        private string passwordText;
        public string PasswordText
        {
            get => passwordText;
            set
            {
                passwordText = value;
                NotifyPropertyChanged(nameof(PasswordText));
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        //Command Properties
        public DelegateCommand LoginCommand { get; }
        public LoginViewModel(Window window)
        {
            userService = new UserService();
            LoginCommand = new DelegateCommand(LoginUser, CanLoginUser);
            LoginViewWindow = window;
        }

        private void LoginUser(object parameter)
        {
            if (CanLoginUser(parameter))
            {
                Result<Users> result =  userService.LoginUser(PasswordText, UserNameText);
                if (result.Successful)
                {
                    Window purchaseWindow = new PurchaseWindow(result.Data);
                    purchaseWindow.Show();
                    LoginViewWindow.Close();
                }
                else
                    CommandFailed?.Invoke(result.ErrorMessage);
            }
        }

        private bool CanLoginUser(object obj)
        {     
            return !string.IsNullOrEmpty(UserNameText)
                && !string.IsNullOrEmpty(PasswordText);
        }
    }
}

