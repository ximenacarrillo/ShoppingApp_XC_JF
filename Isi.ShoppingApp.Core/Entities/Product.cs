using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    
        public long IdProduct { get; }
        public string Name {
            get => name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }
        //Created by Hector Fonseca and edited by Ximena Carrillo
        public decimal Price
        {
            get => price;
            set
            {
                if (value >= 0)
                {
                    price = value;
                    NotifyPropertyChanged(nameof(Price));
                }
            }
        }

        //Created by Hector Fonseca and edited by Ximena Carrillo
        public int Stock
        {
            get => stock;
            private set
            {
                if (value >= 0)
                {
                    stock = value;
                    NotifyPropertyChanged(nameof(Stock));
                    NotifyPropertyChanged(nameof(Available));
                }
            }
        }

        //Created by Hector Fonseca and edited by Ximena Carrillo
        public decimal? Discount
        {
            get => discount;
            set
            {
                if (value == null || value > 0)
                {
                    discount = value;
                    NotifyPropertyChanged(nameof(Discount));
                }
            }
        }

        //Created by Hector Fonseca and edited by Ximena Carrillo
        public int UnitSold
        {
            get => unitSold;
            private set
            {
                if (value > 0)
                {
                    unitSold = value;
                    NotifyPropertyChanged(nameof(UnitSold));
                    NotifyPropertyChanged(nameof(Available));
                }
            }
        }

        //Created by Hector Fonseca
        public Category Category
        {
            get;
        }
        public bool Available
        {
            get => stock > 0;
            
        }


        private string name;
        private decimal price;
        private int stock;
        private decimal? discount;
        private int unitSold;

        //Created by Hector Fonseca
        public Product(long idProduct, string name, decimal price, int stock, decimal? discount, int unitSold, Category category)
        {
            IdProduct = idProduct;
            Name = name;
            Price = price;
            Stock = stock;
            Discount = discount;
            UnitSold = unitSold;
            Category = category;
        }
        //Created by Hector Fonseca
        public Product(long idProduct, Product other)
            :this(idProduct, other.Name, other.Price, other.stock, other.Discount, other.UnitSold, other.Category)
        { }
        //Created by Hector Fonseca 
        public Product(string name, decimal price, int stock, decimal? discount, Category category)
            :this(0, name, price, stock, discount, 0, category)
        { }
        //Created by Hector Fonseca
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Created by Hector Fonseca

        public void AddStock(int quantity)
        {
            Stock += quantity;
        }
        //Created by Hector Fonseca

        public void Sale(int quantity)
        {
            UnitSold += quantity;
            Stock -= quantity;
        }
    }
}
