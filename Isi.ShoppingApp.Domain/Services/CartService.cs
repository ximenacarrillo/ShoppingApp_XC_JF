using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.Utility.Results;
using System;

namespace Isi.ShoppingApp.Domain.Services
{
    public class CartService
    {

        private readonly CartRepository repository;
        public CartService()
        {
            repository = new CartRepository();
        }
        public Result<Cart> CreateCart(Cart cart)
        {
            ThrowIfCartIsNull(cart);

            cart = repository.CreateCart(cart);
            if (cart != null)
                return Result<Cart>.Success(cart);

            return Result<Cart>.Error("Could not add new cart.");
        }

        private static void ThrowIfCartIsNull(Cart cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));
        }
    }
}
