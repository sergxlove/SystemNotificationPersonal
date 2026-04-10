using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.Server.Abstractions;
using SystemNotificationPersonal.Server.Hubs;
using SystemNotificationPersonal.Server.Models;

namespace SystemNotificationPersonal.Server.Endpoints
{
    public static class NotifyEndpoints
    {
        public static IEndpointRouteBuilder MapNotifyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/start", async (HttpRequest request,
                [FromServices] IUsersService userService,
                [FromServices] ICodesService codesService,
                [FromServices] IHasherService hasherService,
                IHubContext<NotifyHub> hubContext,
                CancellationToken token) =>
            {
                try
                {
                    LoginRequest? req = await request.ReadFromJsonAsync<LoginRequest>();
                    if (req is null)
                    {
                        Log.Warning("Неверный данные для входа");
                        return Results.BadRequest("Неверные данные");
                    }
                    UsersEntity user = new()
                    {
                        Id = req.Id,
                        Login = req.Login,
                        Password = req.Password,
                    };
                    var isVerify = await userService.VerifyAsync(user, token);
                    if (isVerify)
                    {
                        var code = await codesService.GetCodeAsync(DateOnly.FromDateTime(DateTime.Now), token);
                        if (code == string.Empty) code = await codesService.GenerateAsync(token);
                        var message = new Message
                        {
                            TypeWork = "start",
                            CodeExit = hasherService.GetHash(code),
                            VariableExit = req.VariableExit
                        };
                        await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
                        Log.Information($"Оповещение успешно запущено с кодом: {code}");
                        return Results.Ok(new { Status = $"Оповещение запущено, код: {code} " });
                    }
                    else
                    {
                        Log.Warning("Неверный ввод логина и пароля");
                        return Results.BadRequest("Неверный логин или пароль");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Произошла ошибка: {ex.Message}");
                    return Results.Problem($"Произошла ошибка: {ex.Message}");
                }
            });

            app.MapGet("stop", async (IHubContext<NotifyHub> hubContext,
               [FromServices] ICodesService codesService,
               [FromServices] IHasherService hasherService,
               CancellationToken token) =>
            {
                try
                {
                    var code = await codesService.GetCodeAsync(DateOnly.FromDateTime(DateTime.Now), token);
                    if (code == string.Empty) code = await codesService.GenerateAsync(token);
                    var message = new Message
                    {
                        TypeWork = "stop",
                        CodeExit = hasherService.GetHash(code),
                        VariableExit = 1
                    };
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
                    Log.Information("Оповещение остановлено");
                    return Results.Ok(new { Status = "Оповещение остановлено" });
                }
                catch (Exception ex)
                {
                    Log.Error($"Произошла ошибка: {ex.Message}");
                    return Results.Problem($"Произошла ошибка: {ex.Message}");
                }
            });

            return app;
        }
    }
}
