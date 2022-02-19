using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Cart_Products
    {
        public long FK_IdCart { get; }
        public long FK_IdProduct { get; }
        public int Quantity
        {
            get => quantity;
            set
            {
                if (value > 0)
                    quantity = value;
            }
        }
        public decimal Price 
        { 
            get => price; 
            set
            {
                if (value > 0)
                    price = value;
            }
        }
        public decimal Discount
        {
            get => discount;
            set
            {
                if (value > 0)
                    discount = value;
            }
        }

        public decimal Subtotal
        {
            get => (Price - Discount) * Quantity;
        }

        private int quantity;
        private decimal price;
        private decimal discount;
        public Cart_Products()
        {

        }
    }
}
