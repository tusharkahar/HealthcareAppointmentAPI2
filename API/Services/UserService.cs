using API.Interface;

namespace API.Services
{
    public class UserService : IUserService
    {
        public string Authenticate(string Email, string Password)
        {
            //authenticateion code for JWT
        }
        public string GetUserByRefreshToken(string RefreshToken)
        {
            //implementation code
        }
    }
}
