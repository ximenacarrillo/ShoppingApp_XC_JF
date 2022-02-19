using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Categories
    {
        public long IdCategory { get; }
        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;                
            }
        }
        private string name;

        public Categories(long idCategory, string name)
        {
            IdCategory = idCategory;
            Name = name;
        }
        public Categories(string name)
            :this(idCategory: 0, name: name)
        {   }

        public Categories(long idCategory, Categories other)
            :this(idCategory, other.Name)
        { }
    }
}
