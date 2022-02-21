using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.Utility.Results;
using System;

namespace Isi.ShoppingApp.Domain.Services
{
    public class UserService
    {
        private readonly UserRepository repository;
        public UserService()
        {
            repository = new UserRepository();
        }
        //TODO implement service methods
        public Result<User> LoginUser(string password, string userName)
        {
            User user = repository.GetPassword(userName);
            if (user != null)
                return Result<User>.Success(user);

            return Result<User>.Error($"Username or password incorrect.");
        }

        public Result<User> CreateUser(User user)
        {
            ThrowIfUserIsNull(user);
            user = repository.CreateUser(user);

            if (user != null)
                return Result<User>.Success(user);

            return Result<User>.Error("Could not be created a new user.");
        }
        
        private static void ThrowIfUserIsNull(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
        }

    }
}
