using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Domain.Services;
using Isi.ShoppingApp.Presentation.Views;
using Isi.Utility.Results;
using Isi.Utility.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Isi.ShoppingApp.Presentation.ViewModels
{

    //Created by Hector Fonseca
    public delegate void ErrorMessageHandler(string message);
    public delegate void SuccessHandel(string message);


    public class ShoppingAppViewModel : ViewModel, INotifyPropertyChanged
    {
        //Created by Hector Fonseca
        public event ErrorMessageHandler ErrorMessage;
        public event SuccessHandel Success;

        public User user;
        private Window window;
        private ProductService productService;
        private CartService cartService;
        private CartProductsService cartProductsService;
        private Cart cart;

        #region Data fields
        public string UserName { get; set; }
        private int quantityText { get; set; }
        public decimal TotalSales { get => cartService.getTotalSales(); }
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
        public ObservableCollection<CartSold> Orders { get; }
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
        public Cart Cart{ 
            get =>cart; set { cart = value; NotifyPropertyChanged(nameof(Cart)); } }
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
                RemoveFromCartCommand.NotifyCanExecuteChanged();
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
        public DelegateCommand RemoveFromCartCommand { get; }
        public DelegateCommand EmptyCartCommand { get; }
        public DelegateCommand PlaceOrderCommand { get; }
        public DelegateCommand ViewOrdersCommand { get; }

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


        //Created by Ximena Carrillo and edited by Hector Fonseca
        //Constructor
        public ShoppingAppViewModel(User user, Window window)
        {
            this.user = user;
            this.window = window;
            UserName = user.Name;
            productService = new ProductService();
            cartService = new CartService();
            cartProductsService = new CartProductsService();
            Cart = new Cart(user);
            
            Products = new ObservableCollection<Product>(productService.GetAllProducts());
            Orders = new ObservableCollection<CartSold>(GetCarts());
            Cart_Products = new ObservableCollection<Cart_Products>();            

            LogoutCommand = new DelegateCommand(LogoutUser);
            FilterProductCommand = new DelegateCommand(FilterProduct);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
            AddToCartCommand = new DelegateCommand(AddToCart, CanAddToCart);
            AddQuantityCommand = new DelegateCommand(AddUnitToProduct, CanAddUnitToProduct);
            RemoveQuantityCommand = new DelegateCommand(RemoveUnitToProduct, CanRemoveUnitToProduct);
            EmptyCartCommand = new DelegateCommand(EmptyCart, CanEmptyCart);
            PlaceOrderCommand = new DelegateCommand(PlaceOrder, CanPlaceOrder);
            RemoveFromCartCommand = new DelegateCommand(RemoveFromCart, CanRemoveFromCart);
            ViewOrdersCommand = new DelegateCommand(ViewOrders);
        }

        private List<CartSold> GetCarts()
        {
            if (user.Role.IdRole == Role.ADMIN_ROLE)
                return cartService.GetAllCarts();
            else
                return cartService.GetAllCartsOfClient(user);

        }

        private void ViewOrders(object obj)
        {
            NotifyPropertyChanged(nameof(Orders));
            ViewOrdersCommand.NotifyCanExecuteChanged();
            OrdersView ordersView = new OrdersView(this);
            ordersView.Show();
        }
        //Created by Ximena Carrillo
        private void RemoveFromCart(object obj)
        {
            Cart_Products.Remove(SelectedCartProduct);
            UpdateCart();
            EmptyCartCommand.NotifyCanExecuteChanged();
        }
        //Created by Ximena Carrillo
        private bool CanRemoveFromCart(object arg)
        {
            return SelectedCartProduct != null
                && Cart_Products.Count > 0;
        }
        //Created by Ximena Carrillo
        private void RemoveUnitToProduct(object obj)
        {
            if (CanRemoveUnitToProduct(obj)) 
                SelectedCartProduct.Quantity--;
            EmptyCartCommand.NotifyCanExecuteChanged();
        }
        //Created by Ximena Carrillo
        private bool CanRemoveUnitToProduct(object arg)
        {
            return SelectedCartProduct != null
                && SelectedCartProduct.ProductObject.Available
                && ValidateQuantityToRemoveVsStock(SelectedCartProduct, 1) >= 0;
        }
        //Created by Ximena Carrillo
        private void AddUnitToProduct(object obj)
        {
            if (CanAddUnitToProduct(obj))
                SelectedCartProduct.Quantity++;

            EmptyCartCommand.NotifyCanExecuteChanged();

        }
        //Created by Ximena Carrillo
        private bool CanAddUnitToProduct(object arg)
        {
            return SelectedCartProduct != null
                && SelectedCartProduct.ProductObject.Available
                && ValidateQuantityToAddVsStock(SelectedCartProduct, 1) > 0;
        }
        //Created by Ximena Carrillo and edited by Hector Fonseca
        private void AddToCart(object obj)
        {
            
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
                            cartProductItem.Quantity += ValidateQuantityToAddVsStock(cartProductItem, QuantityText);                            
                        
                }
                UpdateCart();

            }
            AddQuantityCommand.NotifyCanExecuteChanged();
            RemoveQuantityCommand.NotifyCanExecuteChanged();
            EmptyCartCommand.NotifyCanExecuteChanged();
            
        }
        //Created by Ximena Carrillo
        private int ValidateQuantityToAddVsStock(Cart_Products cartProduct, int quantityToAdd)
        {
            int total = cartProduct.Quantity + quantityToAdd;
            if (total <= cartProduct.ProductObject.Stock)
                return quantityToAdd;
            else
            {
                ErrorMessage?.Invoke($"Quantity to add is greather than stock of product {cartProduct.ProductObject.Name}");
                return 0;
            }
            
        }

        //Created by Ximena Carrillo
        private int ValidateQuantityToRemoveVsStock(Cart_Products cartProduct, int quantityToRemove)
        {
            int total = cartProduct.Quantity - quantityToRemove;
            if (total > 0)
                return quantityToRemove;
            else
            {
                ErrorMessage?.Invoke($"I not possible to remove more unit for {cartProduct.ProductObject.Name}");
                return 0;
            }
            
        }

        //Created by Ximena Carrillo
        private void CreateCartProducts()
        {
            Cart_Products.Add(new Cart_Products(null, selectedProduct, QuantityText, selectedProduct.Price, selectedProduct.Discount ?? 0)); ;
        }

        //Created by Ximena Carrillo
        private Cart_Products GetCartProductIfExistsInCart()
        {
            foreach (Cart_Products cartProductItem in Cart_Products)
                if (cartProductItem.ProductObject.Equals(selectedProduct))
                    return cartProductItem;
            
            return null;
        }

        //Created by Ximena Carrillo
        private bool CanAddToCart(object arg)
        {
            return selectedProduct != null
                && QuantityText > 0
                && QuantityText <= selectedProduct.Stock;
        }

        //Created by Ximena Carrillo
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

        //Created by Ximena Carrillo
        private void ClearFilter(object obj)
        {
            if(Products.Count > 0)
                Products.Clear();
            

           List<Product> allProducts = productService.GetAllProducts();
            foreach (Product product in allProducts)
                Products.Add(product);

        }

        //Created by Ximena Carrillo
        private void LogoutUser(object obj)
        {
            Visible = false;
            LoginView LoginViewWindow = new LoginView();
            LoginViewWindow.Show();
            window.Close();

        }

        //Created by Hector Fonseca
        public bool CanEmptyCart(object obj)
        {
            return Cart_Products.Count > 0;
        }
        //Created by Hector Fonseca

        public void EmptyCart(object obj)
        {
            if (CanEmptyCart(obj))
                Cart_Products.Clear();
            UpdateCart();
            EmptyCartCommand.NotifyCanExecuteChanged();
        }
        //Created by Hector Fonseca

        private void UpdateCart()
        {
            List<Cart_Products> toSet = new List<Cart_Products>();
            cart.Products = toSet;
            foreach (Cart_Products cart_Products in Cart_Products)
            {
                toSet.Add(cart_Products);
            }
            cart.Products = toSet;
            PlaceOrderCommand.NotifyCanExecuteChanged();                                    
            ViewOrdersCommand.NotifyCanExecuteChanged();
        }

        private bool CanPlaceOrder(object obj)
        {
            return Cart.Products.Count > 0;
        }

        //Created by Hector Fonseca and edited by Ximena Carrillo
        private void PlaceOrder(object obj)
        {
            try
            {
                if (CanPlaceOrder(obj))
                {
                    Result<Cart> invoice = cartService.CreateCart(Cart);
                    if (invoice.Successful)
                    {
                        Success?.Invoke($"Thank you for your purchase. The invoice number is {invoice.Data.IdCart}, for a total of ${invoice.Data.Total}.");
                        EmptyCart(obj);
                        SelectedProduct = null;
                        AddToCartCommand.NotifyCanExecuteChanged();
                        AddQuantityCommand.NotifyCanExecuteChanged();
                        RemoveQuantityCommand.NotifyCanExecuteChanged();
                        EmptyCartCommand.NotifyCanExecuteChanged();
                        PlaceOrderCommand.NotifyCanExecuteChanged();
                        UpdateOrders();
                    }
                    else
                    {
                        ErrorMessage?.Invoke(invoice.ErrorMessage);
                    };
                }
            }
            catch(Exception e)
            {
                ErrorMessage?.Invoke($"Sorry, the purchase could not be completed.");
            }
        }
        //Created by Ximena Carrillo
        private void UpdateOrders()
        {
            Orders.Clear();

            foreach (CartSold order in GetCarts())
                Orders.Add(order);



            ViewOrdersCommand.NotifyCanExecuteChanged();
        }
    }
}
