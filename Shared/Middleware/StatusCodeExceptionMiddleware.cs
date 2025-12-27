using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Shared.Core;
using Shared.Exception;

namespace Shared.Middleware;

public class StatusCodeExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (StatusCodeException ex)
        {
            var response = new ErrorResponse(ex.Message);

            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}