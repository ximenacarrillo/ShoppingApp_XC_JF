using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Domain.Services
{
    public class ProductService
    {
        private readonly ProductRespository repository;
        public ProductService()
        {
            repository = new ProductRespository();   
        }
        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public List<Product> GetProductsByFirstName(string filterText)
        {
            return repository.GetProductsByFirstName(filterText);
        }
    }
}
