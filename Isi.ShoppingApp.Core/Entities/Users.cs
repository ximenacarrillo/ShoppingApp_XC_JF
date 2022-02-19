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

        public Users(string username, string name, byte[] password, long idRole)
            :this(0, username, name, password, idRole)
        { }
        public Users(long idUser, Users other)
            :this(idUser, other.Username, other.Name, other.Password, other.IdRole)
        {   }
        public Users(long idUser, string username, string name, byte[] password, long idRole)
        {
            IdUser = idUser;
            Username = username;
            Name = name;
            Password = password; ///Check I dont want to have always the password here;
            IdRole = idRole;
        }
    }
}
