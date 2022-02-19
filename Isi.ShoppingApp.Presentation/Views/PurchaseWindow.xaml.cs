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
        public PurchaseWindow(Users user )
        {
            InitializeComponent();
            ShoppingAppViewModel shoppingAppViewModel = new ShoppingAppViewModel(user);
            DataContext = shoppingAppViewModel;
        }
    }
}
