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
        private int quantityText { get; set; }
        public int QuantityText
        {
            get => quantityText;
            set
            {
                quantityText = value;
                NotifyPropertyChanged(nameof(QuantityText));
                AddToCartCommand.NotifyCanExecuteChanged();
            }
        }
        public ObservableCollection<Product> Products { get; }
        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedProduct));
                AddToCartCommand.NotifyCanExecuteChanged();
            }
        }
        public ObservableCollection<Cart_Products> Cart { get; set; }
        private Product selectedCartProduct;
        public Product SelectedCartProduct
        {
            get => selectedCartProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedCartProduct));
            }
        }
        #endregion



        #region Command Properties
        public DelegateCommand LogoutCommand { get; }
        public DelegateCommand FilterProductCommand { get; }
        public DelegateCommand ClearFilterCommand { get; }
        public DelegateCommand AddToCartCommand { get; }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                NotifyPropertyChanged(nameof(FilterText));
                FilterProductCommand.NotifyCanExecuteChanged();
                if (value != string.Empty)
                    FilterProduct(null);
                else
                    ClearFilter(null);
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
            Products = new ObservableCollection<Product>(productService.GetAllProducts());
            Cart = new ObservableCollection<Cart_Products>();
            LogoutCommand = new DelegateCommand(LogoutUser);
            FilterProductCommand = new DelegateCommand(FilterProduct);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
            AddToCartCommand = new DelegateCommand(AddToCart, CanAddToCart);

        }

        private void AddToCart(object obj)
        {
            if (CanAddToCart(obj))
            {
                //Cart.Add(new Cart_Products())
            }
        }

        private bool CanAddToCart(object arg)
        {
            return selectedProduct != null
                && QuantityText > 0;
        }

        private void FilterProduct(object obj)
        {
            Products.Clear();
            List<Product> resultFilter = productService.GetProductsByFirstName(FilterText);
            if (resultFilter != null)
            {
                foreach (Product product in resultFilter)
                    Products.Add(product);
            }
        }

        private void ClearFilter(object obj)
        {
            if(Products.Count > 0)
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
