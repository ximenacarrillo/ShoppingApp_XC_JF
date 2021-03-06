using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.Utility.Authentication;
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

        //Created by Ximena Carrillo and edited by Hector Fonseca
        public Result<User> LoginUser(string password, string username)
        {
            HashedPassword storedPassword = repository.GetPassword(username);

            if (storedPassword != null)
                if(PasswordHasher.CheckPassword(password, storedPassword) == PasswordResult.Correct)
                {
                    User user = repository.GetUserByUsername(username);
                    return Result<User>.Success(user);
                }

            return Result<User>.Error($"Username or password incorrect.");
        }

        //Created by Hector Fonseca
        public Result<User> CreateUser(User user)
        {
            ThrowIfUserIsNull(user);
            user = repository.CreateUser(user);

            if (user != null)
                return Result<User>.Success(user);

            return Result<User>.Error("Could not be created a new user.");
        }

        //Created by Ximena Carrillo
        private static void ThrowIfUserIsNull(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
        }

    }
}
