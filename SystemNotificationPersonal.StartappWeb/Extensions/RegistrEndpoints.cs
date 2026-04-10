namespace SystemNotificationPersonal.StartappWeb.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapNotifyEndpoints();

            return app;
        }
    }
}
