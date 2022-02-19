using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Data.Repositories
{
    public class RoleRepository
    {
        private readonly string connectionString;

        public RoleRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingApp"].ConnectionString;
        }
    }
}
