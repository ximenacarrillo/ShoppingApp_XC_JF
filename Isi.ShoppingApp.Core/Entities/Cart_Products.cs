using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    //Created by Hector Fonseca

    public class Cart_Products : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
         
        public Cart CartObject { get; set; }
        public Product ProductObject { get; set; }

        public long IdCart_Products { get; }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (value > 0)
                {
                    quantity = value;
                    NotifyPropertyChanged(nameof(Quantity));
                    NotifyPropertyChanged(nameof(Subtotal));
                }
            }
        }
        public decimal Price 
        { 
            get => price; 
            set
            {
                if (value > 0)
                {
                    price = value;
                    NotifyPropertyChanged(nameof(Price));
                }
            }
        }
        public decimal Discount
        {
            get => discount;
            set
            {
                if (value > 0)
                {
                    discount = value;
                    NotifyPropertyChanged(nameof(Discount));
                }
            }
        }

        public decimal Subtotal
        {
            get => (Price - (Price * Discount / 100)) * Quantity;
        }

        public decimal DiscountValue
        {
            get => (Price * Quantity* Discount / 100);
        }

        private int quantity;
        private decimal price;
        private decimal discount;

        public Cart_Products(Cart cart, Product product, int quantity, decimal price, decimal discount)
            : this(0, cart, product, quantity, price, discount)
        { }
        public Cart_Products(long idCart, Cart cart, Product product, int quantity, decimal price, decimal discount)
            
        {
            IdCart_Products = idCart;
            CartObject = cart;
            ProductObject = product;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }
        public Cart_Products(long id, Cart_Products other)
            :this(id, other.CartObject, other.ProductObject, other.quantity, other.Price, other.Discount)
        { }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
