using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagmentSystem.WebApi.Controllers.Attributes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace StudentManagmentSystem.WebApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            
            var endpoint = context.GetEndpoint();
            var isAnonymousAllowed = endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null;
            var isAuthorized = endpoint?.Metadata?.GetMetadata<RoleAdminAttribute>() != null;

            if (isAnonymousAllowed)
            {
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                await RespondUnauthorized(context, "Missing or invalid Authorization header.");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            //var role = GetUserRoleFromToken(token);

            var principal = ValidateToken(token);
            if (principal == null)
            {
                await RespondUnauthorized(context, "Unauthorized: Invalid or expired token.");
                return;
            }

            
            context.User = principal;

            //var ok = (isAuthorized && role == "1")|| (isAuthorized && role != "1");
            if (isAuthorized)
            {
                var role = GetUserRoleFromToken(token);
                if (role != "1")
                {
                    await RespondUnauthorized(context, "Unauthorized: Admin permission required.");
                    return;
                }
            }

            await _next(context);
            
        }


        

        private ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true
                };

                return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);
            }
            catch
            {
                return null;
            }

           
        }

        private static async Task RespondUnauthorized(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                success = false,
                message
            });

            await context.Response.WriteAsync(result);
        }
        




        private static string? GetUserRoleFromToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();
            var role = claims.Find(i => i.Type == ClaimTypes.Role)?.Value;
            return role;
        }

    }
}
