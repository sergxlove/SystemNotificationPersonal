using Microsoft.AspNetCore.Mvc;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.ConfigClientWeb.Endpoints
{
    public static class ConfigEndpoints
    {
        public static IEndpointRouteBuilder MapConfigEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/config", () =>
            {

            });

            app.MapPost("/api/config", ([FromBody] AppSettingClient settings) =>
            {

            });

            app.MapPost("/api/config/reset", () =>
            {

            });

            app.MapPost("/api/app/start", () =>
            {

            });

            app.MapPost("/api/app/start-admin", () =>
            {

            });


            return app;
        }
    }
}
