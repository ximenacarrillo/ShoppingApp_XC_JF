using Isi.ShoppingApp.Core.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Data.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingApp"].ConnectionString;
        }

        public User GetEmployee(long id)
        {
            return null;
        }


        
        private User ReadNextUser(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string name = reader.GetString(1);
            string username = reader.GetString(2);
            byte[] password = null; //TODO = (byte[])reader.GetValues(3);
            long idRole = reader.GetInt64(4);

            return new User(id, username, name, password, idRole);
        }
    }
}
