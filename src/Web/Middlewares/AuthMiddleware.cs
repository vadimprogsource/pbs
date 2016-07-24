using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Api;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using Web.Models;

namespace Web.Middlewares
{

    public class AuthMiddleware
    {

        
        private readonly RequestDelegate _next;
       
        public AuthMiddleware(RequestDelegate next )
        {
            _next     = next;
        }

        public Task Invoke(HttpContext httpContext,IAuthProvider provider,ISecurityContext context)
        {

            if (provider.IsMembersAccessOf(httpContext.Request.Path.Value
                                         .Split('/')
                                         .Where(x => !string.IsNullOrWhiteSpace(x))
                                         .Select(x => x.ToLowerInvariant())
                                         .FirstOrDefault()))
                try
                {
                    StringValues userSessionHeader = httpContext.Request.Headers["User-Session"];

                    if (userSessionHeader.Any())
                    {
                        IAuthSession securityToken = provider.Auth(Guid.Parse(userSessionHeader.First()));

                        if (securityToken.IsAuthenticated)
                        {

                            context.Session = securityToken;

                            httpContext.User = new UserPrincipalModel(securityToken);
                            httpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                            return _next(httpContext);

                        }

                    }

                    throw new NotSupportedException();
                }
                catch
                {
                    httpContext.Response.StatusCode = 404;
                    return Task.FromResult(false);
                }

            httpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
