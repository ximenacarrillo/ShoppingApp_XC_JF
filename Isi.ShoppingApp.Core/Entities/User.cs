using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Core.Entities
{
    //Created by Hector Fonseca
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
        public byte[] PasswordHash
        {
            get => passwordHash;
            set
            {
                passwordHash = value;
            }
        }
        public byte[] PasswordSalt
        {
            get => passwordSalt;
            set
            {
                passwordSalt = value;
            }
        }
        public Role Role
        {
            get;
        }
        private string name;
        private byte[] passwordHash;
        private byte[] passwordSalt;

        public User(string username, string name, byte[] passwordHash, byte[] passwordSalt, Role role)
            :this(0, username, name, passwordHash, passwordSalt, role)
        { }
        public User(long idUser, User other)
            :this(idUser, other.Username, other.Name, other.passwordHash, other.passwordSalt, other.Role)
        {   }
        public User(long idUser, string username, string name, byte[] passwordHash, byte[] passwordSalt, Role role)
        {
            IdUser = idUser;
            Username = username;
            Name = name;
            Role = role;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
