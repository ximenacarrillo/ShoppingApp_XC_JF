using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Domain.Services;
using Isi.ShoppingApp.Presentation.Views;
using Isi.Utility.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Isi.ShoppingApp.Presentation.ViewModels
{
    public class ShoppingAppViewModel : ViewModel, INotifyPropertyChanged
    {
        private User user;
        private Window window;
        private ProductService productService;

        #region Data fields
        public string UserName { get; set; }
        public ObservableCollection<Product> Products { get; }
        private Product selectedProduct;
        private Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedProduct));
            }
        }
        #endregion



        #region Command Properties
        public DelegateCommand LogoutCommand { get; }
        public DelegateCommand FilterProductCommand { get; }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                FilterProduct(null);
                NotifyPropertyChanged(nameof(FilterText));
                FilterProductCommand.NotifyCanExecuteChanged();
            }
        }
        #endregion

        //Constructor
        public ShoppingAppViewModel(User user, Window window)
        {
            this.user = user;
            this.window = window;
            UserName = user.Name;
            productService = new ProductService();
            LogoutCommand = new DelegateCommand(LogoutUser);
            Products = new ObservableCollection<Product>(productService.GetAllProducts());
            FilterProductCommand = new DelegateCommand(FilterProduct);

        }

        private void FilterProduct(object obj)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                ClearFilter(obj);
            }
            else
            {
                Products.Clear();
                List<Product> resultFilter = productService.GetProductsByFirstName(FilterText);
                if (resultFilter != null)
                {
                    foreach (Product product in resultFilter)
                        Products.Add(product);
                }
            }
        }

        private void ClearFilter(object obj)
        {
            Products.Clear();

            List<Product> allProducts = productService.GetAllProducts();
            foreach (Product product in allProducts)
                Products.Add(product);
        }

        private void LogoutUser(object obj)
        {
            Visible = false;
            Window LoginViewWindow = new LoginView();
            LoginViewWindow.Show();
            window.Close();

        }
    }
}
