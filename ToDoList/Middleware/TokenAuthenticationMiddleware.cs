namespace ToDoList.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _token;

        public TokenAuthenticationMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _token = config["APIToken"];
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            if (path?.Contains("/todo") == true)
            {
                if (!context.Request.Headers.TryGetValue("APIToken", out var headerValue) || headerValue != _token)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized access.");
                    return;
                }
            }

            await _next(context);
        }
    }


}
