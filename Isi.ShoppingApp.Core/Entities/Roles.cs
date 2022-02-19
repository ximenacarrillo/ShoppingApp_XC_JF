using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Roles
    {
        public long IdRole { get; }
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
        public Roles (long id, string name)
        {
            IdRole = id;
            Name = name;
        }

        public Roles(long id, Roles other)
            :this(id, other.Name)
        { }

        public Roles(string name)
            :this(id: 0, name: name)
        { }


    }
}
