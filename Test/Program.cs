using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductRespository userRepository = new ();
            List<Product> user = userRepository.GetAllProducts();

            foreach (Product product in user)
            {
                Console.WriteLine(product.Name);

            }
        }
    }
}
