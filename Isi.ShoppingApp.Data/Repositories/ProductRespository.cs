using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Isi.ShoppingApp.Data.Repositories
{
    public class ProductRespository
    {
        private readonly string connectionString; 
        public ProductRespository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingApp"].ConnectionString;
        }
     }
}
