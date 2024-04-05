using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_Stock.Filters
{
    public class JwtAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _config;
        public JwtAuthorizeFilter(IConfiguration config) => _config = config;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Lấy mã thông báo JWT từ header "Authorization" của yêu cầu HTTP và cắt chuỗi và chuỗi "Bearer " đi
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token == null)
            {
                // Nếu token null thì trả về một UnauthorizedResult cho client
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["jwt:SecretKey"] ?? "");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                if(jwtToken.ValidTo < DateTime.UtcNow)
                {
                    //Xử lý token hết hạn
                    context.Result = new UnauthorizedResult(); 
                    return;
                }
                var userId = int.Parse(jwtToken.Claims.First().Value);
                // Lưu ID người dụng vào dictionary Items của HttpContext
                context.HttpContext.Items["UserId"] = userId;
            }
            catch (Exception)
            {
                // Nếu xảy ra lỗi thì trả về một UnauthorizedResult cho client
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
