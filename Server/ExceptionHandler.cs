using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceDemo.Server;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        ProblemDetails problemDetails = exception switch
        {
            KeyNotFoundException => new()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
            },
            _ => new()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
            },
        };

        problemDetails.Detail = exception.InnerException?.Message ?? exception.Message;

        httpContext.Response.StatusCode = problemDetails.Status.GetValueOrDefault();
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
    //public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    //{
    //    try
    //    {
    //        await next(context);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        context.Response.StatusCode = StatusCodes.Status404NotFound;
    //        await context.Response.WriteAsync(ex.InnerException?.Message ?? ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        context.Response.StatusCode = StatusCodes.Status400BadRequest;
    //        await context.Response.WriteAsync(ex.InnerException?.Message ?? ex.Message);
    //    }
    //}
}
