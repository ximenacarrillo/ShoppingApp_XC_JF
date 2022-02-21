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
        public decimal Discount 
        {
            get => discount;
            set
            {   
                if(value >= 0)
                    discount = value;
            }                
        }
        public decimal Taxes
        {
            get => (Subtotal * GST) + (Subtotal * QST);
            
        }

        public decimal Subtotal
        { 
            //In my opinion is a calculate property
            get => subtotal;
            set
            {
                if (value > 0)
                {
                    subtotal = value;
                    NotifyPropertyChanged(nameof(Subtotal));
                }
            }
        }
        
        public decimal Total
        {
            get => Subtotal + Taxes;
        }

        public bool Sold { get; set; }
        
        public User User { get; }

        private decimal discount;
        private decimal subtotal;
 
        public Cart(long idCart, User user)
        {
            IdCart = idCart;
            User = user;
            Discount = 0;
            Subtotal = 0;
            Sold = false;
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
