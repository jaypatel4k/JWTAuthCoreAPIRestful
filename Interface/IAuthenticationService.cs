using JWTAuthCoreAPIRestful.Models;

namespace JWTAuthCoreAPIRestful.Interface
{
    public interface IAuthenticationService
    {
        Task<LoginModel> ValidateUser(LoginModel login);
        Task<string> GenerateJSONWebToken(UserModel userInfo);
    }
}
