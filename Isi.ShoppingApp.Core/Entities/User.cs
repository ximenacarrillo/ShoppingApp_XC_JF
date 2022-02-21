using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    public class User
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
        public Role Role
        {
            get;
        }
        private string name;
        private byte[] password;

        public User(string username, string name, Role role)
            :this(0, username, name, role)
        { }
        public User(long idUser, User other)
            :this(idUser, other.Username, other.Name, other.Role)
        {   }
        public User(long idUser, string username, string name, Role role)
        {
            IdUser = idUser;
            Username = username;
            Name = name;
            Role = role;
        }
    }
}
