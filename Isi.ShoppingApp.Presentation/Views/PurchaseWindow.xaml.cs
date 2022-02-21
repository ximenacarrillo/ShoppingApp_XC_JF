using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Presentation.ViewModels;
using System.Windows;

namespace Isi.ShoppingApp.Presentation.Views
{
    /// <summary>
    /// Interaction logic for PurchaseWindow.xaml
    /// </summary>
    public partial class PurchaseWindow : Window
    {
        public PurchaseWindow(User user )
        {
            InitializeComponent();
            ShoppingAppViewModel shoppingAppViewModel = new ShoppingAppViewModel(user, this);
            shoppingAppViewModel.ErrorMessage += OnErrorMessage;
            DataContext = shoppingAppViewModel;
        }

        private void OnErrorMessage(string message) =>
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

    }
}
