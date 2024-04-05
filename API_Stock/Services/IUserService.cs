using API_Stock.Models;
using API_Stock.ViewModes;

namespace API_Stock.Services
{
    public interface IUserService
    {
        Task<User?> Register(RegisterViewModel registerViewModel);
        // JWT string
        Task<string> Login(LoginViewModel loginViewModel);

        Task<User?> GetUserById(int userId);

    }
}
