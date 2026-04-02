using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SystemNotificationPersonal.ConfigServerConsole.Cases;
using SystemNotificationPersonal.ConfigServerConsole.Interfaces;
using SystemNotificationPersonal.ConfigServerConsole.Services;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;

namespace SystemNotificationPersonal.ConfigServerConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppSettingServer appSetting = new AppSettingServer();
            appSetting.ReadConfig();
            if (appSetting.FirstStart)
            {
                appSetting.CreateConfig();
            }
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<SystemNotificationDbContext>(options =>
                options.UseSqlite(appSetting.ConnectionString));
            serviceCollection.AddSingleton<IUsersRepository, UsersRepository>();
            serviceCollection.AddSingleton<IUsersService, UsersService>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ExecuteCommandCore cmd = new ExecuteCommandCore();
            DataCore data = new DataCore(appSetting);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("")
                .CreateLogger();
            cmd.AddRange(ConsoleCases.UseConsoleCases());
            string commandLine = string.Empty;
            bool exit = false;
            while (!exit)
            {
                Console.Write($"user > ");
                commandLine = Console.ReadLine()!;
                if (commandLine == "exit") exit = true;
                cmd.ExecuteCommand(commandLine, data, serviceProvider);
            }
        }
    }
}
