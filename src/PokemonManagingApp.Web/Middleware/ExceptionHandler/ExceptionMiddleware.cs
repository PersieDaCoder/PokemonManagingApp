using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PokemonManagingApp.Web.Middleware.ExceptionHandler;

public class ExceptionMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is Exception)
        {
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Type = exception.GetType().Name,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            });
            return true;
        }
        return false;
    }
}