using Azure.Core;
using Azure;

namespace ShopOrderSystem.Utility.Auth
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
