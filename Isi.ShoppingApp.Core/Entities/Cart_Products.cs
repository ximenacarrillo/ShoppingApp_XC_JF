using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Cart_Products : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Cart CartObject { get; set; }
        public Product ProductObject { get; set; }
        public int Quantity
        {
            get => quantity;
            set
            {
                if (value > 0)
                {
                    quantity = value;
                    NotifyPropertyChanged(nameof(Quantity));
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
                    NotifyPropertyChanged(nameof(Price));
                }
            }
        }

        public decimal Subtotal
        {
            get => subtotal;
            set { 
                if(value >= 0)
                {
                    subtotal = value;
                    NotifyPropertyChanged(nameof(Subtotal));
                }
            }
        }

        private int quantity;
        private decimal price;
        private decimal discount;
        private decimal subtotal;

        public Cart_Products(Cart cart, Product product, int quantity, decimal price, decimal discount, decimal subtotal)
        {
            CartObject = cart;
            ProductObject = product;
            Quantity = quantity;
            Price = price;
            Discount = discount;
            Subtotal = subtotal;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
