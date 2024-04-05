using API_Stock.Models;
using API_Stock.ViewModes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace API_Stock.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StockAppContext _context;
        private readonly IConfiguration _config;

        public UserRepository(StockAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> Create(RegisterViewModel registerViewModel)
        {
            string sql = "EXECUTE dbo.RegisterUser " +
                        "@username," +
                        " @password," +
                        " @email," +
                        " @phone," +
                        " @fullname," +
                        " @dateOfBirth," +
                        " @country";
            // Gọi 1 procedure trong SQLServer
            IEnumerable<User> result = await _context.Users.FromSqlRaw(
                sql,
                new SqlParameter("@username", registerViewModel.Username ?? ""),
                new SqlParameter("@password", registerViewModel.Password ),
                new SqlParameter("@email", registerViewModel.Email),
                new SqlParameter("@phone", registerViewModel.Phone ?? ""),
                new SqlParameter("@fullname", registerViewModel.Fullname ?? ""),
                new SqlParameter("@dateOfBirth", registerViewModel.DateOfBirth),
                new SqlParameter("@country", registerViewModel.Country)).ToListAsync();
            User? user = result.FirstOrDefault();
            return user;
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            string sql = "EXECUTE dbo.CheckLogin @email, @password";
            IEnumerable<User> result = await _context.Users.FromSqlRaw(sql,
                            new SqlParameter("@email", loginViewModel.Email),
                            new SqlParameter("@password", loginViewModel.Password))
                .ToListAsync();

            User? user = result.FirstOrDefault();
            if(user != null)
            {
                //Tạo ra JWT cho client
                var tokenHandler = new JwtSecurityTokenHandler();
                //Lấy Jwt ở appsettings.json
                var key = Encoding.UTF8.GetBytes(_config["jwt:SecretKey"] ?? "");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    }),
                    // Cấp cho token thời hạn 30 ngày
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
            else
            {
                throw new Exception("Wrong email or password");
            }
            
        }
    }
}
