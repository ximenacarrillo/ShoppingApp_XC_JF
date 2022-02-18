using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Product
    {
        public long IdProduct { get; }
        public string Name {
            get => name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                if (value >= 0)
                    price = value;
            }
        }

        public int Stock
        {
            get => stock;
            set
            {
                if (value >= 0)
                    stock = value;
            }
        }

        public decimal? Discount
        {
            get => discount;
            set
            {
                if (value == null || value > 0)
                    discount = value;
            }
        }

        public int UnitSold
        {
            get => unitSold;
            set
            {
                if (value > 0)
                    unitSold = value;
            }
        }

        public long IdCategory
        {
            get => idCategory;
            set
            {
                idCategory = value;
            }
        }


        private string name;
        private decimal price;
        private int stock;
        private decimal? discount;
        private int unitSold;
        private long idCategory;
    }
}
