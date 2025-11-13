using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml;

namespace WebAPI.Middlewares;

public class JsonToXmlMiddleware
{
    private readonly RequestDelegate _next;

    public JsonToXmlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Check if the client requests XML via Accept header
        if (ShouldReturnXml(context.Request.Headers.Accept.ToString()))
        {
            // Capture the original response
            var originalBodyStream = context.Response.Body;

            try
            {
                // Create a new memory stream to store the modified response
                using var responseBody = new MemoryStream();
                // Replace the original response stream with the memory stream
                context.Response.Body = responseBody;

                // Call the next middleware in the pipeline
                await _next(context);

                // Rewind the memory stream to read the JSON response
                responseBody.Seek(0, SeekOrigin.Begin);

                // Read the JSON response
                var jsonResponse = await new StreamReader(responseBody).ReadToEndAsync();

                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    // Convert JSON to XML
                    var xmlResponse = ConvertJsonToXml(jsonResponse);

                    if (xmlResponse != jsonResponse)
                    {
                        // Set the Content-Type to XML
                        context.Response.ContentType = "application/xml";

                        // Write the XML response to the original response stream
                        var xmlBytes = Encoding.UTF8.GetBytes(xmlResponse);
                        await originalBodyStream.WriteAsync(xmlBytes, 0, xmlBytes.Length);
                        return;
                    }
                }

                // Write the JSON response to the original response stream
                var jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
                await originalBodyStream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
            }
            finally
            {
                // Restore the original response stream
                context.Response.Body = originalBodyStream;
            }
        }
        else
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

    private bool ShouldReturnXml(string acceptHeader)
    {
        if (string.IsNullOrWhiteSpace(acceptHeader))
            return false;

        var acceptTypes = acceptHeader.Split(',')
            .Select(h => h.Trim().Split(';')[0].Trim())
            .ToList();

        // Check for XML content types
        return acceptTypes.Any(type => 
            type.Equals("application/xml", StringComparison.OrdinalIgnoreCase) ||
            type.Equals("text/xml", StringComparison.OrdinalIgnoreCase));
    }

    private string ConvertJsonToXml(string jsonResponse)
    {
        // In order to use this method, we need Newtonsoft JSON
        XmlDocument? doc = JsonConvert.DeserializeXmlNode("{\"user\":" + jsonResponse + "}", "response");
        return doc?.OuterXml ?? jsonResponse;
    }
}

