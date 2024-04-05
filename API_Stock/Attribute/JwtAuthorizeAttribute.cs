using API_Stock.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API_Stock.Attribute
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute()
            : base(typeof(JwtAuthorizeFilter))
        {
        }


    }
}
