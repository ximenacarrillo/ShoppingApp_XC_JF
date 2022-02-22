using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Role
    {
        public const int ADMIN_ROLE = 1;
        public const int CLIENT_ROLE = 2;
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
        public Role (long id, string name)
        {
            IdRole = id;
            Name = name;
        }

        public Role(long id, Role other)
            :this(id, other.Name)
        { }

        public Role(string name)
            :this(id: 0, name: name)
        { }


    }
}
