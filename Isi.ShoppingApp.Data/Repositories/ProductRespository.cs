using Isi.ShoppingApp.Core.Entities;
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
        private List<Product> products;
        public ProductRespository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
            products = new List<Product>() {
                new Product(1, "Robot Vacuum", 239, 100, null, 0, 1),
                new Product(4, "Lego City Street", 32, 0, null, 0, 2),
                new Product(18, "Bike Specialized", 400, 50, 0, 0,8)
            };
        }

        public List<Product> GetAllProducts()
        {
            //TODO
            return products;
        }

        public List<Product> GetProductsByFirstName(string filterText)
        {
            //TODO
            List<Product> listToReturn = new List<Product>();
            if (filterText == "u")
            {
                listToReturn.Add(products[0]);
            }
            else if (filterText == "e"){
                listToReturn.Add(products[1]);
                listToReturn.Add(products[2]);
            }
            else if (filterText == "b"){
                listToReturn.Add(products[0]);
                listToReturn.Add(products[2]);
            }
            else
            {
                listToReturn = null;
            }


            return listToReturn;
        }
    }
}
