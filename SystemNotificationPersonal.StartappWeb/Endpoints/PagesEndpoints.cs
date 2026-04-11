using Serilog;

namespace SystemNotificationPersonal.StartappWeb.Endpoints
{
    public static class PagesEndpoints
    {
        public static IEndpointRouteBuilder MapPagesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/index.html");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }

            }).RequireRateLimiting("GeneralPolicy");

            app.MapGet("/index", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/index.html");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }

            }).RequireRateLimiting("GeneralPolicy");

            return app;
        }
    }
}
