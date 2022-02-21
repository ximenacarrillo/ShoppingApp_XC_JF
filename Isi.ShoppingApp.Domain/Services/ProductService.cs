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
        private readonly ProductRepository repository;
        public ProductService()
        {
            repository = new ProductRepository();   
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
            return repository.GetProductsByFirstName(filterText);
        }

        public Result RemoveProduct(Product product)
        {
            ThrowIfProductIsNull(product);

            if (!repository.ProductExist(product.IdProduct))
                return Result.Error($"No product exists with id {product.IdProduct}.");

            bool success = repository.DeleteProduct(product);
            if (!success)
                return Result.Error($"Failed to remove product with id {product.IdProduct}.");
            return Result.Success();
        }

        public Result<Product> AddProduct(Product product)
        {
            ThrowIfProductIsNull(product);

            product = repository.CreateProduct(product);
            if (product != null)
                return Result<Product>.Success(product);

            return Result<Product>.Error("Could not add new product.");
        }

        /// <exception cref="ArgumentNullException"/>
        public Result UpdateProduct(Product product)
        {
            ThrowIfProductIsNull(product);

            if (!repository.ProductExist(product.IdProduct))
                return Result.Error($"No product exists with id {product.IdProduct}.");

            bool updated = repository.UpdateProduct(product);
            if (updated)
                return Result.Success();

            return Result.Error($"Failed to update product with id {product.IdProduct}.");
        }

        private static void ThrowIfProductIsNull(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
        }
    }
}
