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

        public List<Cart_Products> CreateProductosFromCart(Cart cart)
        {
            return repository.CreateProductsFromCart(cart);
        }
        private static void ThrowIfCartProductsIsNull(Cart_Products cartProducts)
        {
            if (cartProducts == null)
                throw new ArgumentNullException(nameof(cartProducts));
        }
    }
}
