using Microsoft.EntityFrameworkCore;
using Serilog;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;
using SystemNotificationPersonal.Server.Abstractions;
using SystemNotificationPersonal.Server.Extensions;
using SystemNotificationPersonal.Server.Hubs;
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
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("D:\\documents\\logsSNP\\logServer.txt")
                .CreateLogger();
            var app = builder.Build();

            app.MapAllEndpoints();
            app.MapHub<NotifyHub>("/notify");

            app.Run();
        }
    }
}
