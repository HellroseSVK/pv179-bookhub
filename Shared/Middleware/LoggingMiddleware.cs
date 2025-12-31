using System.Text;
using System.Text.Json;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Shared.Middleware;

public class LoggingMiddleware(RequestDelegate next, IDbContextFactory<CustomDBContext> dbFactory, string prefix = "")
{
    private readonly string _prefix = prefix;

    public async Task InvokeAsync(HttpContext context)
    {
        // ----- Read Request Body -----
        context.Request.EnableBuffering();
        string requestBody;
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        // ----- Capture Response Body -----
        var originalResponseBodyStream = context.Response.Body;
        using var newResponseBodyStream = new MemoryStream();
        context.Response.Body = newResponseBodyStream;

        await next(context);

        // ----- Read Response Body -----
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        // ----- Build Log Entry -----
        var httpLog = new HttpLog
        {
            Timestamp = DateTime.UtcNow,
            RequestMethod = (string.IsNullOrEmpty(_prefix) ? "" : _prefix + " ") + context.Request.Method,
            RequestPath = (string.IsNullOrEmpty(_prefix) ? "" : _prefix + " ") + context.Request.Path,
            QueryString = context.Request.QueryString.Value ?? "",
            RequestHeaders = JsonSerializer.Serialize(context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())),
            RequestBody = requestBody ?? "",
            ResponseStatusCode = context.Response.StatusCode,
            ResponseHeaders = JsonSerializer.Serialize(context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())),
            ResponseBody = responseBody ?? ""
        };

        // ----- Save to database -----
        await using (var db = await dbFactory.CreateDbContextAsync())
        {
            db.HttpLogs.Add(httpLog);
            await db.SaveChangesAsync();
        }

        // ----- Restore original response -----
        await newResponseBodyStream.CopyToAsync(originalResponseBodyStream);
        context.Response.Body = originalResponseBodyStream;
    }
}