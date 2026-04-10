using SystemNotificationPersonal.Server.Endpoints;

namespace SystemNotificationPersonal.Server.Extensions
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
