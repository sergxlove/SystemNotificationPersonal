using MediatR;
using Microsoft.AspNetCore.Mvc;
using SystemNotificationPersonal.StartappWeb.Commands;

namespace SystemNotificationPersonal.StartappWeb.Endpoints
{
    public static class NotifyEndpoints
    {
        public static IEndpointRouteBuilder MapNotifyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/notify/start", async (HttpContext context, 
                [FromBody] StartNotifyCommand command,
                IMediator mediator,
                CancellationToken token) =>
            {
                return await mediator.Send(command, token);
            });

            app.MapPost("/notify/stop", async (HttpContext context,
                [FromBody] StopNotifyCommand command,
                IMediator mediator,
                CancellationToken token) =>
            {
                return await mediator.Send(command, token);
            });

            return app;
        }
    }
}
