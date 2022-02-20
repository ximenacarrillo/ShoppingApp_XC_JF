using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Repositories;
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
        //TODO implement service methods
        public Result<User> LoginUser(string password, string userName)
        {
            User user = repository.GetPassword(userName);
           // if (user != null)
                return Result<User>.Success(user);

            //return Result<Users>.Error($"Username or password incorrect.");
        }
    }
}
