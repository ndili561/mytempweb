using Microsoft.AspNetCore.Builder;

namespace CRM.WebAPI.Middlewares.AuthenticatedUser
{
    public static class AuthenticatedUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticatedDatabaseAccess(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticatedUserMiddleware>();
        }
    }
}