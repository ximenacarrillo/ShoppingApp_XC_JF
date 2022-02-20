using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetUser(1);
            Console.WriteLine(user.Name);
        }
    }
}
