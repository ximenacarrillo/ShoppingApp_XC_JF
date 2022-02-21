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
    public class CartProductsService
    {

        private readonly CartProductRepository repository;
        public CartProductsService()
        {
            repository = new CartProductRepository();
        }
        public Result<Cart_Products> CreateCartProducts(Cart_Products cartProducts)
        {
            ThrowIfCartProductsIsNull(cartProducts);

            cartProducts = repository.CreateCartProducts(cartProducts);
            if (cartProducts != null)
                return Result<Cart_Products>.Success(cartProducts);

            return Result<Cart_Products>.Error("Could not add new cart.");
        }

        private static void ThrowIfCartProductsIsNull(Cart_Products cartProducts)
        {
            if (cartProducts == null)
                throw new ArgumentNullException(nameof(cartProducts));
        }
    }
}
