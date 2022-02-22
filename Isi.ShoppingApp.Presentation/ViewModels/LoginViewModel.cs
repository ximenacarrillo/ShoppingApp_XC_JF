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

    //Created by Ximena Carrillo
    public class LoginViewModel : ViewModel, INotifyPropertyChanged
    {
        public event Action<string> CommandFailed;
        private UserService userService;

    #region Data fields
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
    #endregion
    #region Command Properties
        public DelegateCommand LoginCommand { get; }
        #endregion
    #region Constructor
        public LoginViewModel(Window window)
        {
            userService = new UserService();
            LoginCommand = new DelegateCommand(LoginUser, CanLoginUser);
            LoginViewWindow = window;
        }
        #endregion

        //Created by Ximena Carrillo

        private void LoginUser(object parameter)
        {
            if (CanLoginUser(parameter))
            {
                Result<User> result =  userService.LoginUser(PasswordText, UserNameText);
                if (result.Successful)
                {
                    Window purchaseWindow = new PurchaseWindow(result.Data);
                    purchaseWindow.Show();
                    LoginViewWindow.Close();
                }
                else
                {
                    CommandFailed?.Invoke(result.ErrorMessage);
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        //Created by Ximena Carrillo
        private bool CanLoginUser(object obj)
        {     
            return !string.IsNullOrEmpty(UserNameText)
                && !string.IsNullOrEmpty(PasswordText);
        }
    }
}

