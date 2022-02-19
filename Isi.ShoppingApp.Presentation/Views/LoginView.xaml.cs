using Isi.ShoppingApp.Presentation.ViewModels;
using System.Windows;

namespace Isi.ShoppingApp.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        LoginViewModel loginViewModel;
        public LoginView()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel(this);
            //loginViewModel.ActionSucceeded += OnActionSucceded;
            DataContext = loginViewModel;
        }

        private void PasswordTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            loginViewModel.PasswordText = PasswordTextBox.Password;
        }

      
    }
}
