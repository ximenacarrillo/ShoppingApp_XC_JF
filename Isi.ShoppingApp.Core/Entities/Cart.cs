using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Cart
    {
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
                    subtotal = value;
            }
        }

        public decimal Total
        {
            get => Subtotal + Taxes;
        }

        public bool Sold { get; set; }
        
        public long FK_IdUser { get; }

        private decimal discount;
        private decimal subtotal;
 
        public Cart(long idCart, long fk_idUser)
        {

        }
    }
}
