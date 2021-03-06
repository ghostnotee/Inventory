using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Inventory.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Inventory.Application.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);

            await _next(context);

            watch.Stop();

            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " +
                      context.Response.StatusCode + " in" + " ms" + watch.Elapsed.TotalMilliseconds;
            Console.WriteLine(message);
        }
        catch (Exception e)
        {
            watch.Stop();
            await HandleException(context, e, watch);
        }
    }

    private Task HandleException(HttpContext context, Exception exception, Stopwatch watch)
    {
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        string message = "[Error]    HTTP " + context.Request.Method + " - " + context.Response.StatusCode +
                         " Error Message: " + exception.Message + " in " + watch.Elapsed.TotalMilliseconds;
        
        Console.WriteLine(message);

        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message
        });

        return context.Response.WriteAsync(result);
    }
}

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}