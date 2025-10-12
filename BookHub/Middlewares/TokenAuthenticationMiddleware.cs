namespace WebAPI.Middlewares
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenAuthenticationMiddleware> _logger;
        private const string _API_TOKEN = "very_secret_token";

        public TokenAuthenticationMiddleware(RequestDelegate next, ILogger<TokenAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var req_token = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(req_token) && req_token == _API_TOKEN)
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
