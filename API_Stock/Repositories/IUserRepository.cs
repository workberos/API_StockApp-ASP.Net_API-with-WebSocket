using API_Stock.Models;
using API_Stock.ViewModes;

namespace API_Stock.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByUserName(string username);
        Task<User?> GetByEmail(string email);
        Task<User?> Create(RegisterViewModel registerViewModel);
        // JWT string
        Task<string> Login(LoginViewModel loginViewModel);
    }

}

