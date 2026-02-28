using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;
using SystemNotificationPersonal.Server.Abstractions;
using SystemNotificationPersonal.Server.Hubs;
using SystemNotificationPersonal.Server.Models;
using SystemNotificationPersonal.Server.Services;

namespace SystemNotificationPersonal.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AppSettingServer settings = new AppSettingServer();
            settings.ReadConfig();
            if (settings.FirstStart)
            {
                settings.CreateConfig();
            }
            builder.WebHost.UseUrls($"{settings.Protocol}://*:{settings.Port}");
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<SystemNotificationDbContext>(options =>
                options.UseSqlite(settings.ConnectionString));
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICodesExitRepository, CodesExitRepository>();
            builder.Services.AddScoped<ICodesService, CodesService>();
            builder.Services.AddScoped<IHasherService, HasherService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalNetwork", policy =>
                {
                    policy.SetIsOriginAllowed(origin =>
                    {
                        var host = new Uri(origin).Host;
                        return host.StartsWith($"{settings.IPAddressCors}") ||
                               host == "localhost" ||
                               host == "127.0.0.1";
                    })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            var app = builder.Build();

            app.MapPost("/start", async (HttpRequest request,
                IUsersService userService,
                ICodesService codesService,
                IHasherService hasherService,
                IHubContext<NotifyHub> hubContext) =>
            {
                try
                {
                    Request? req = await request.ReadFromJsonAsync<Request>();
                    if (req is null) return Results.BadRequest("Неверные данные");
                    UsersEntity user = new()
                    {
                        Id = req.Id,
                        Login = req.Login,
                        Password = req.Password,
                    };
                    var isVerify = await userService.VerifyAsync(user);
                    if (isVerify)
                    {
                        var code = await codesService.GetCodeAsync(DateOnly.FromDateTime(DateTime.Now));
                        if (code == string.Empty) code = await codesService.GenerateAsync();
                        var message = new Message
                        {
                            TypeWork = "start",
                            CodeExit = hasherService.GetHash(code),
                            VariableExit = req.VariableExit
                        };
                        await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
                        return Results.Ok(new { Status = $"Оповещение запущено, код: {code} " });
                    }
                    else
                    {
                        return Results.BadRequest("Неверный логин или пароль");
                    }
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Произошла ошибка: {ex.Message}");
                }
            });

            app.MapGet("stop", async (IHubContext<NotifyHub> hubContext,
               ICodesService codesService,
               IHasherService hasherService) =>
            {
                var code = await codesService.GetCodeAsync(DateOnly.FromDateTime(DateTime.Now));
                if (code == string.Empty) code = await codesService.GenerateAsync();
                var message = new Message
                {
                    TypeWork = "stop",
                    CodeExit = hasherService.GetHash(code),
                    VariableExit = 1
                };
                await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
                return Results.Ok(new { Status = "Оповещение остановлено" });
            });

            app.MapHub<NotifyHub>("/notify");

            app.Run();
        }
    }
}
