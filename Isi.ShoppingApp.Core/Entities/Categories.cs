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

        public Categories(long id, string name)
        {
            IdCategory = id;
            Name = name;
        }
        public Categories(string name)
            :this(id: 0, name: name)
        {

        }
    }
}
