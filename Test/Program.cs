using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.ShoppingApp.Domain.Services;
using Isi.Utility.Results;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CartService cartService = new();
            foreach (CartSold cart in cartService.GetAllCarts())
            {
                Console.WriteLine(cart.SoldDate);
            }
        }
    }
}
