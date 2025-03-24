
using HogwartsAPI.SharedKernel;
using Microsoft.AspNetCore.Diagnostics;

namespace HogwartsAPI.Web.Api.Extensions
{

    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var code = StatusCodes.Status501NotImplemented;
            var response = Error.NotImplemented(exception.Message);
            httpContext.Response.StatusCode = code;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}