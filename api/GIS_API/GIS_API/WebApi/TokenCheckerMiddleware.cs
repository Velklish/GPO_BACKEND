using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GIS_API
{
    public class TokenCheckerMiddleware
    {
        private readonly RequestDelegate next;

        public TokenCheckerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["token"].ToString();

            if (string.IsNullOrEmpty(token) && !context.Request.Path.Value.Contains("/login"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
