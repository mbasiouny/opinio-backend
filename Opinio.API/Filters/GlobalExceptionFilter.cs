using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Opinio.Core.Helpers;

namespace Opinio.API.Filters;

public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is UnauthorizedAccessException)
        {
            var unauthorizedResponse = OperationResult<object>.Unauthorized();
            context.Result = new ObjectResult(unauthorizedResponse) { StatusCode = StatusCodes.Status401Unauthorized };

            logger.LogError(context.Exception, "Unauthorized access error occurred.");
        }
        else
        {
            var errorResponse = OperationResult<object>.Failure();
            context.Result = new ObjectResult(errorResponse) { StatusCode = StatusCodes.Status500InternalServerError };

            logger.LogError(context.Exception, "An unexpected error occurred.");
        }

        context.ExceptionHandled = true;
    }
}
