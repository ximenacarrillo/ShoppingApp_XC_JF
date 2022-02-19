using Isi.ShoppingApp.Core.Entities;
using Isi.ShoppingApp.Data.Data;
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
        public Result<Users> LoginUser(string password, string userName)
        {
            Users user = repository.LoginUser(password, userName);
           // if (user != null)
                return Result<Users>.Success(user);

            //return Result<Users>.Error($"Username or password incorrect.");
        }
    }
}
