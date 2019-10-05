using Microsoft.AspNetCore.Http;

namespace CRM.Web.Service
{
    public class LoginUserService : ILoginUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public LoginUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public System.Security.Claims.ClaimsPrincipal GetCurrentUser()
        {
           return _httpContextAccessor.HttpContext.User;
            
        }
    }

    public interface ILoginUserService
    {
        System.Security.Claims.ClaimsPrincipal GetCurrentUser();
    }
}
