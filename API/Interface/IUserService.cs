namespace API.Interface
{
    public interface IUserService
    {
        public string Authenticate(string Email, string Password);
        public string GetUserByRefreshToken(string RefreshToken);
    }
}
