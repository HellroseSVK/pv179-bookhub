namespace Shared.Exception;

public class StatusCodeException(int statusCode, string message) : System.Exception(message)
{
    public int StatusCode { get; } = statusCode;
}