using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class Users
    {
        public long IdUser { get; }
        public string Username { get; }
        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
            }
        }
        public byte[] Password
        {
            get => password;
            set
            {
                //TODO
            }
        }
        public long IdRole
        {
            get => idRole;
            set
            {
                //TODO
            }
        }
        private string name;
        private byte[] password;
        private long idRole;

        public Users()
        {

        }
    }
}
