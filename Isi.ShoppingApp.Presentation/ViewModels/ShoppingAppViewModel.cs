using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Domain.Services;
using Isi.ShoppingApp.Presentation.Views;
using Isi.Utility.Results;
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
        private CartService cartService;
        private CartProductsService cartProductsService;

        #region Data fields
        public string UserName { get; set; }
        private int quantityText { get; set; }
        public int QuantityText
        {
            get => quantityText;
            set
            {
                if(value > 0)
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
        public ObservableCollection<Cart_Products> Cart_Products { get; set; }
        public Cart Cart{ get; set; }
        private Cart_Products selectedCartProduct;
        public Cart_Products SelectedCartProduct
        {
            get => selectedCartProduct;
            set{
                selectedCartProduct = value;
                NotifyPropertyChanged(nameof(SelectedCartProduct));
                AddToCartCommand.NotifyCanExecuteChanged();
                AddQuantityCommand.NotifyCanExecuteChanged();
                RemoveQuantityCommand.NotifyCanExecuteChanged();
            }
        }
        #endregion

        #region Command Properties
        public DelegateCommand LogoutCommand { get; }
        public DelegateCommand FilterProductCommand { get; }
        public DelegateCommand ClearFilterCommand { get; }
        public DelegateCommand AddToCartCommand { get; }
        public DelegateCommand AddQuantityCommand { get; }
        public DelegateCommand RemoveQuantityCommand { get; }

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
        int cartCounter = 0;
        //Constructor
        public ShoppingAppViewModel(User user, Window window)
        {
            this.user = user;
            this.window = window;
            UserName = user.Name;
            productService = new ProductService();
            cartService = new CartService();
            cartProductsService = new CartProductsService();
            
            Products = new ObservableCollection<Product>(productService.GetAllProducts());
            Cart_Products = new ObservableCollection<Cart_Products>();            

            LogoutCommand = new DelegateCommand(LogoutUser);
            FilterProductCommand = new DelegateCommand(FilterProduct);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
            AddToCartCommand = new DelegateCommand(AddToCart, CanAddToCart);
            AddQuantityCommand = new DelegateCommand(AddUnitToProduct, CanAddUnitToProduct);
            RemoveQuantityCommand = new DelegateCommand(RemoveUnitToProduct, CanRemoveUnitToProduct);

        }

        private void RemoveUnitToProduct(object obj)
        {
            if (CanAddUnitToProduct(obj)) { 
                SelectedCartProduct.Quantity--;
                SelectedCartProduct.Subtotal = getSubotal(SelectedCartProduct.Price, SelectedCartProduct.Discount, 1);
            }
        }

        private bool CanRemoveUnitToProduct(object arg)
        {
            return SelectedCartProduct != null
                && SelectedCartProduct.ProductObject.Stock > 0
                && ValidateQuantityToRemoveVsStock(SelectedCartProduct, 1) >= 0;
        }

        private void AddUnitToProduct(object obj)
        {
            if (CanAddUnitToProduct(obj))
            {
                SelectedCartProduct.Quantity++;
                SelectedCartProduct.Subtotal = getSubotal(SelectedCartProduct.Price, SelectedCartProduct.Discount, 1);
            }
        }

        private bool CanAddUnitToProduct(object arg)
        {
            return SelectedCartProduct != null
                && SelectedCartProduct.ProductObject.Stock > 0
                && ValidateQuantityToAddVsStock(SelectedCartProduct, 1) > 0;
        }

        private void AddToCart(object obj)
        {
            if(cartCounter == 0)
            {
                Result<Cart> result = cartService.CreateCart(new Cart(1, user));
                if (result.Successful)
                {
                    this.Cart = result.Data;
                    cartCounter++;
                }
            }
            if (CanAddToCart(obj))
            {
                Cart_Products cartProducts = GetCartProductIfExistsInCart();
                if (cartProducts == null)
                {
                    CreateCartProducts();
                }
                else
                {
                    foreach (Cart_Products cartProductItem in Cart_Products)
                        if (cartProductItem.ProductObject.Equals(selectedProduct))
                        { 
                            cartProductItem.Quantity += ValidateQuantityToAddVsStock(cartProductItem, QuantityText);
                            cartProductItem.Subtotal += getSubotal(cartProductItem.Price, cartProductItem.Discount, cartProductItem.Quantity);
                        }
                }
                
                
            }
            AddQuantityCommand.NotifyCanExecuteChanged();
            RemoveQuantityCommand.NotifyCanExecuteChanged();
        }

        private int ValidateQuantityToAddVsStock(Cart_Products cartProduct, int quantityToAdd)
        {
            int total = cartProduct.Quantity + quantityToAdd;
            if (total <= cartProduct.ProductObject.Stock)
                return quantityToAdd;
            else
            {
                MessageBox.Show($"Quantity to add is greather than stock of product {cartProduct.ProductObject.Name}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
        private int ValidateQuantityToRemoveVsStock(Cart_Products cartProduct, int quantityToRemove)
        {
            int total = cartProduct.Quantity - quantityToRemove;
            if (total >= 0)
                return quantityToRemove;
            else
            {
                MessageBox.Show($"I not possible to remove more unit for {cartProduct.ProductObject.Name}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }

        private void CreateCartProducts()
        {
            Result<Cart_Products> resultCartProducts = cartProductsService.CreateCartProducts(new Cart_Products(
                                    Cart, selectedProduct, QuantityText, selectedProduct.Price, selectedProduct.Discount ?? 0,
                                    getSubotal(selectedProduct.Price, selectedProduct.Discount ?? 0, QuantityText))
                                    );
            if (resultCartProducts.Successful)
                Cart_Products.Add(resultCartProducts.Data);
        }

        private decimal getSubotal(decimal price, decimal discount, decimal quantity)
        {
            return (price - discount) * quantity;
        }

        private Cart_Products GetCartProductIfExistsInCart()
        {
            foreach (Cart_Products cartProductItem in Cart_Products)
                if (cartProductItem.ProductObject.Equals(selectedProduct))
                    return cartProductItem;
            
            return null;
        }

        private bool CanAddToCart(object arg)
        {
            return selectedProduct != null
                && QuantityText > 0
                && QuantityText <= selectedProduct.Stock;
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
