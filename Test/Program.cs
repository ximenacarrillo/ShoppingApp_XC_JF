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
            ProductService userRepository = new ();
            Product product = userRepository.GetProductById(23).Data;
            product.Name = "Ipad";
            userRepository.UpdateProduct(product);
        }
    }
}
