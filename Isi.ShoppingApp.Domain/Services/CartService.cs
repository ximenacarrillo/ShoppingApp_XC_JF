using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.Utility.Results;
using System;
using System.Collections.Generic;

namespace Isi.ShoppingApp.Domain.Services
{
    public class CartService
    {

        private readonly CartRepository cartRepository;
        private readonly CartProductsService cartProductServices;
        private readonly ProductService productService;
        public CartService()
        {
            cartRepository = new CartRepository();
            cartProductServices = new CartProductsService();
            productService = new ProductService();

        }
        //Created by Hector Fonseca and edited by Ximena Carrillo
        public Result<Cart> CreateCart(Cart cart)
        {
            ThrowIfCartIsNull(cart);

            cart = cartRepository.CreateCart(cart);
            List<Cart_Products> products= cartProductServices.CreateProductosFromCart(cart);
            foreach (Cart_Products product in products)
            {
                Product singleProduct = product.ProductObject;
                singleProduct.Sale(product.Quantity);
                productService.UpdateProduct(singleProduct);
            }
            if (cart != null && products.Count == cart.Products.Count) 
                return Result<Cart>.Success(cart);

            return Result<Cart>.Error("Sorry, the purchase could not be completed.");
        }
        //Created by Hector Fonseca and edited by Ximena Carrillo
        public List<CartSold> GetAllCarts()
        {
            return cartRepository.GetAllCarts();
        }
        public List<CartSold> GetAllCartsOfClient(User user)
        {
            return cartRepository.GetAllCartsOfClient(user);
        }
        //Created by Hector Fonseca
        public decimal getTotalSales()
        {
            return cartRepository.GetDataStore();
        }

        //Created by Ximena Carrillo
        private static void ThrowIfCartIsNull(Cart cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));
        }
    }
}
