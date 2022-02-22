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
            HashedPassword hashPassword = PasswordHasher.HashPassword("JaredC");

            Result<User> result = userRepository.CreateUser(new User("JaredC", "Jared", hashPassword.Hash, hashPassword.Salt, new Role(2, "Admin")));
            Console.WriteLine($"{result.Data}");
        }
    }
}
