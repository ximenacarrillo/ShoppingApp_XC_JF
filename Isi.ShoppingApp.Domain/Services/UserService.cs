using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
using Isi.Utility.Authentication;
using Isi.Utility.Results;

namespace Isi.ShoppingApp.Domain.Services
{
    public class UserService
    {
        private readonly UserRepository repository;
        public UserService()
        {
            repository = new UserRepository();
        }
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
    }
}
