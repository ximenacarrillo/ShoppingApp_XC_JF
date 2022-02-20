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
        //TODO create propierty Available
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

        public Category Category
        {
            get;
        }


        private string name;
        private decimal price;
        private int stock;
        private decimal? discount;
        private int unitSold;

        public Product(long idProduct, string name, decimal price, int stock, decimal? discount, int unitSold, Category category)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Discount = discount;
            UnitSold = unitSold;
            Category = category;
        }

        public Product(long idProduct, Product other)
            :this(idProduct, other.Name, other.Price, other.stock, other.Discount, other.UnitSold, other.Category)
        { }
        public Product(string name, decimal price, int stock, decimal? discount, Category category)
            :this(0, name, price, stock, discount, 0, category)
        { }
    }
}
