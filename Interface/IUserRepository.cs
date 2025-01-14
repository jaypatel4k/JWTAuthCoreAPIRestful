using JWTAuthCoreAPIRestful.Models;

namespace JWTAuthCoreAPIRestful.Interface
{
    public interface IUserRepository
    {
        UserModel GetUser(string username, string password);
    }
}
