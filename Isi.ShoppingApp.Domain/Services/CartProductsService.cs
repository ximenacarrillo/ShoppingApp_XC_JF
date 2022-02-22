using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Isi.ShoppingApp.Domain.Services
{
    public class CartProductsService
    {
        //Created by Hector Fonseca
        private readonly CartProductRepository repository;
        public CartProductsService()
        {
            repository = new CartProductRepository();
        }
        //Created by Hector Fonseca

        public List<Cart_Products> CreateProductosFromCart(Cart cart)
        {
            return repository.CreateProductsFromCart(cart);
        }
    }
}
