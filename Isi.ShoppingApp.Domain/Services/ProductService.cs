using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.Utility.Results;
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
        public Result<Product> GetProductById(long id)
        {
            Product product = repository.GetProductById(id);
            if (product != null)
                return Result<Product>.Success(product);
            return Result<Product>.Error($"No product found with id {id}.");
        }
        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public List<Product> GetProductsByFirstName(string filterText)
        {
            return null;
        }
    }
}
