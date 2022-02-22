using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    //Created by Hector Fonseca

    public class CartSold
    {
        public DateTime SoldDate { get; }
        public decimal Discount { get; }
        public decimal Subtotal { get; }
        public decimal Taxes { get; }
        public decimal Total { get; }
        public string CustomerName { get; }

        public CartSold(DateTime soldate, decimal discount, decimal subtotal, decimal taxes, decimal total, string customerName)
        {
            SoldDate = soldate;
            Discount = discount;
            Subtotal = subtotal;
            Taxes = taxes;
            Total = total;
            CustomerName = customerName;
        }
    }
}
