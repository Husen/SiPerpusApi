using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
namespace SiPerpusApi.Utils;

public class SwaggerBasicAuthMiddleware
{
    private readonly RequestDelegate next;
    private readonly SwaggerAuthOptions _swaggerAuthOptions;

    public SwaggerBasicAuthMiddleware(RequestDelegate next, IOptions<SwaggerAuthOptions> swaggerAuthOptions)
    {
        this.next = next;
        this._swaggerAuthOptions = swaggerAuthOptions.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger/v1"))
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the credentials from request header
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var inBytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                // validate credentials
                if (username.Equals(_swaggerAuthOptions.Username)
                    && password.Equals(_swaggerAuthOptions.Password))
                {
                    await next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }

            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}

public static class SwaggerAuthExtentions
{
    public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
    }
}

public class SwaggerAuthOptions
{
    public string Username { get; set; }
    public string Password { get; set; }
}