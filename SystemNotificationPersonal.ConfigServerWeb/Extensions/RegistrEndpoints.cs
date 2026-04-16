using SystemNotificationPersonal.ConfigServerWeb.Endpoints;

namespace SystemNotificationPersonal.ConfigServerWeb.Extensions
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
