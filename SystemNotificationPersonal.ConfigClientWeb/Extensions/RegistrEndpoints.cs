using SystemNotificationPersonal.ConfigClientWeb.Endpoints;

namespace SystemNotificationPersonal.ConfigClientWeb.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapConfigEndpoints();
            app.MapPageEndpoints();

            return app;
        }
    }
}
