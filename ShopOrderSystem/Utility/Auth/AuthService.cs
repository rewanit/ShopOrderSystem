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

            var user = users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, "Basic");
                var principal = new ClaimsPrincipal(identity);
                return user;
            }

            return null;
        }
    }
}
