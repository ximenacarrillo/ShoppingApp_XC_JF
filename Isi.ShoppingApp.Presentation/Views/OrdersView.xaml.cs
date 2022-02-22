using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Isi.ShoppingApp.Presentation.Views
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Window
    {
        public OrdersView(ShoppingAppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            if (viewModel.user.Role.IdRole == Role.ADMIN_ROLE)
            {
                TitleOrderLabel.Content = "All Orders";
                TotalSalesPanel.Visibility = Visibility.Visible;
            }
            else
            {
                OrdersDataGrid.Columns[5].Visibility = Visibility.Hidden;
                TitleOrderLabel.Content = "My Orders";
            }

        }
    }
}
