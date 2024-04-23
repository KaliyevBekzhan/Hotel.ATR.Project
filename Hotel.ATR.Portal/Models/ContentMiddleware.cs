namespace Hotel.ATR.Portal.Models
{
    public class ContentMiddleware
    {
        private RequestDelegate nextDelegate;
        public ContentMiddleware(RequestDelegate next)
        {
            this.nextDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString() == "/middleware")
            {
                await context.Response.WriteAsync("Это содержание с middleware");
            }
            else
            {
                context.Request.Headers["User-Agent"] = "new-value";

                await nextDelegate.Invoke(context);
            }
        }
    }
    public static class ContentMiddlewareExtensions
    {
        public static IApplicationBuilder UseContentMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ContentMiddleware>();
        }
    }
}
