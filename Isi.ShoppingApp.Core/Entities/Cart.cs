using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Cart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly decimal GST = 0.05m;
        public static readonly decimal QST = 0.09975m;
        public long IdCart { get; }

        public decimal Subtotal
        {
            get
            {
                decimal toReturn = 0;

                foreach (Cart_Products product in products)
                {
                    toReturn += product.Subtotal;
                }
                return toReturn;
            }
        }
        public decimal Discount
        {
            get
            {
                decimal toReturn = 0;

                foreach (Cart_Products product in products)
                {
                    toReturn += product.DiscountValue;
                }
                return toReturn;
            }
        }
        public decimal Taxes
        {
            get => (Subtotal * GST) + (Subtotal * QST);            
        }

        
        
        public decimal Total
        {
            get => Subtotal + Taxes;
        }

        public bool Sold { get; set; }

        public List<Cart_Products> Products 
        { 
            get => products; 
            set
            {
                products = value;
                NotifyPropertyChanged(nameof(Subtotal));
                NotifyPropertyChanged(nameof(Taxes));
                NotifyPropertyChanged(nameof(Discount));
                NotifyPropertyChanged(nameof(Total));
            }
        }
        
        public User User { get; }


        private List<Cart_Products> products;
 
        public Cart(long idCart, User user, List<Cart_Products> products)
        {
            IdCart = idCart;
            User = user;
            Sold = false;
            Products = products;
        }
        public Cart(long id, Cart other)
            :this(id, other.User, other.Products)
        { }

        public Cart(User user)
            :this(0, user, new List<Cart_Products>())
        { }
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
