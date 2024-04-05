using API_Stock.Models;
using API_Stock.Repositories;
using API_Stock.ViewModes;

namespace API_Stock.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            return await _userRepository.Login(loginViewModel);
        }
        public async Task<User?> Register(RegisterViewModel registerViewModel)
        {
            // Kiểm tra xem username đã tồn tại chưa
            // Tạo ra đối tượng user từ RegisterViewModel
            var existingUserByUsername = await _userRepository
                        .GetByUserName(registerViewModel.Username ?? "");
            if (existingUserByUsername != null) throw new ArgumentException("Username already exists");

            // Kiểm tra xem email đã tồn tại chưa
            var existingUserByEmail = await _userRepository
                        .GetByEmail(registerViewModel.Email);
            if (existingUserByEmail != null) throw new ArgumentException("Email already exists");

            // Thực hiện đăng ký người dùng mới
            return await _userRepository.Create(registerViewModel);
        }

        public async Task<User?> GetUserById(int userId)
        {
            User? user = await _userRepository.GetById(userId);
            return user;
        }



    }
}

