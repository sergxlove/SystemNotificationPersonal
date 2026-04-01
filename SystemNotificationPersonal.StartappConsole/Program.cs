using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.StartappConsole.Cases;

namespace SystemNotificationPersonal.StartappConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ExecuteCommandCore cmd = new ExecuteCommandCore();
            AppSettingStartApp appSetting = new();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("")
                .CreateLogger();
            appSetting.ReadConfig();
            if (appSetting.FirstStart)
            {
                appSetting.CreateConfig();
            }
            DataCore data = new DataCore(appSetting);
            cmd.AddRange(ConsoleCases.UseConsoleCases());
            string commandLine = string.Empty;
            bool exit = false;
            Console.WriteLine("Используя данное программное обеспечение, вы принимаете все условия лицензии. См LISENCE.txt");
            Log.Information("Пользователь принял лицензию на данное программное обеспечени");
            Log.Information("Приложение запущено");
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
