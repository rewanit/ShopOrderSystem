using System.Security.Claims;

namespace ShopOrderSystem.Utility.Auth
{
    public class AuthService : IAuthService
    {

        public AuthService()
        {
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var users = new List<User>
            {
                new User { Username = "Admin", Password = "Admin", Role = "Admin" },
                new User { Username = "User", Password = "User", Role = "User" }
            };

            return await Task.FromResult(users.SingleOrDefault(x => x.Username == username && x.Password == password));

        }
    }
}
