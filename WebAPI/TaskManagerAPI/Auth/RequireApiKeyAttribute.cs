using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagerAPI.Auth;

/// <summary>
///     Requires a matching <c>X-Api-Key</c> header on the decorated action.
///
///     Fails closed: if no <c>ApiKey</c> is configured, every protected call is
///     rejected, so a missing environment variable can never leave writes open.
///     Reads stay public. The key travels in a header, not a cookie, so a
///     browser never attaches it on its own and cross-site request forgery does
///     not apply.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class RequireApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public const string HeaderName = "X-Api-Key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var expected = context.HttpContext.RequestServices
            .GetRequiredService<IConfiguration>()["ApiKey"];

        var supplied = context.HttpContext.Request.Headers[HeaderName].ToString();

        if (string.IsNullOrEmpty(expected) || !KeysMatch(expected, supplied))
        {
            context.Result = new ObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "A valid API key is required for this operation.",
                Detail = $"Send it in the {HeaderName} header. Reads (GET) are public."
            })
            { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        await next();
    }

    // Hash both sides first so the comparison is fixed-length and constant-time.
    private static bool KeysMatch(string expected, string supplied) =>
        CryptographicOperations.FixedTimeEquals(
            SHA256.HashData(Encoding.UTF8.GetBytes(expected)),
            SHA256.HashData(Encoding.UTF8.GetBytes(supplied)));
}
