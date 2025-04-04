using Microsoft.AspNetCore.Authorization;

namespace StudentManagmentSystem.WebApp.Configurations
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var allowedRoutes = context?.GetEndpoint().Metadata.GetMetadata<AllowAnonymousAttribute>();
            if (allowedRoutes != null)
            {
                await _next(context);
                return;
            }
            
            var loggedInUser = context.Session.GetString("LoggedInUser");
            if (string.IsNullOrWhiteSpace(loggedInUser) )
                context.Response.Redirect("/ApplicationUser/SignInForm");
            await _next(context);
        }

    }
}
