using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.ShoppingApp.Domain.Services;
using Isi.Utility.Authentication;
using Isi.Utility.Results;
using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userRepository = new ();

            byte[] hashPassword = PasswordHasher.HashPassword("@dm1n").Hash;

            Result<User> result = userRepository.CreateUser(new User("admin", "admin", hashPassword, new Role(1, "Admin")));
            Console.WriteLine($"{result.Data}");
            
        }
    }
}
