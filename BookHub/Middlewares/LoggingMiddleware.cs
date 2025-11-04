using System.Text;

namespace WebAPI.Middlewares;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        string requestBody;
        using (var reader = new StreamReader(
                   context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        logger.LogInformation("Request: {Method} {Path} | Parameters: {@Parameters} | Query: {Query} | Body: {Body}",
            context.Request.Method,
            context.Request.Path,
            context.Request.RouteValues,
            context.Request.QueryString.Value,
            requestBody);

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        switch (context.Response.StatusCode)
        {
            case >= 500:
                logger.LogError("Response: {StatusCode} | Body: {Body}",
                    context.Response.StatusCode,
                    responseText);
                break;
            case >= 400:
                logger.LogWarning("Response: {StatusCode} | Body: {Body}",
                    context.Response.StatusCode,
                    responseText);
                break;
            default:
                logger.LogInformation("Response: {StatusCode} | Body: {Body}",
                    context.Response.StatusCode,
                    responseText);
                break;
        }

        await responseBody.CopyToAsync(originalBodyStream);
        context.Response.Body = originalBodyStream;
    }
}