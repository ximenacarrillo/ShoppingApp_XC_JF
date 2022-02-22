using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Presentation.ViewModels;
using System.Windows;

namespace Isi.ShoppingApp.Presentation.Views
{
    /// <summary>
    /// Interaction logic for PurchaseWindow.xaml
    /// </summary>
    ///
    
    //Created by Ximena Carrillo
    public partial class PurchaseWindow : Window
    {
        

        public PurchaseWindow(User user )
        {
            InitializeComponent();
            ShoppingAppViewModel shoppingAppViewModel = new ShoppingAppViewModel(user, this);
            shoppingAppViewModel.ErrorMessage += OnErrorMessage;
            shoppingAppViewModel.Success += OnSuccessMessage;
            DataContext = shoppingAppViewModel;

            if (user.Role.IdRole == Role.ADMIN_ROLE)
                HeaderView.ViewOrdersButton.Content = "View All Orders";
            else
                HeaderView.ViewOrdersButton.Content = "View My Orders";

        }

        private void OnErrorMessage(string message) =>
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        private void OnSuccessMessage(string message) =>
                MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

    }
}
