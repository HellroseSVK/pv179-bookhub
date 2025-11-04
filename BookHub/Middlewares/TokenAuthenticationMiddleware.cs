namespace WebAPI.Middlewares
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenAuthenticationMiddleware> _logger;
        private const string ApiToken = "very_secret_token";

        public TokenAuthenticationMiddleware(RequestDelegate next, ILogger<TokenAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var reqToken = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(reqToken) && reqToken == ApiToken)
            {
                await _next(context);
            }
            else
            {
                _logger.LogWarning("Unauthorized access attempted.");
                context.Response.StatusCode = 401;
            }
        }
    }
}
