using System.Threading.Tasks;
using CRM.DAL.Repository;
using Microsoft.AspNetCore.Http;

namespace CRM.WebAPI.Middlewares.AuthenticatedUser
{
    public class AuthenticatedUserMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticatedUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRepository repository)
        {
            // TODO Get userId from request (cookie?) and set it here.
            var userId = httpContext.Request.Headers["UserId"] ;
            if (string.IsNullOrWhiteSpace(userId))
            {
                userId = "ef37d97c-9bbf-4a2c-b722-ae86bd4150c3";
            }
            repository.UpdateCurrentLoggedUserId(userId, true);
            await _next(httpContext);
        }
    }
}
