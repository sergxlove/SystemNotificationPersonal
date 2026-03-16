using Microsoft.Extensions.DependencyInjection;
using SystemNotificationPersonal.ConfigClientConsole.Cases;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.ConfigClientConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ExecuteCommandCore cmd = new ExecuteCommandCore();
            AppSettingClient appSetting = new AppSettingClient();
            appSetting.PathAppsettings = "D:\\documents\\SystemNotificationPeople\\ClientApp\\Windows\\appsettings.json";
            appSetting.CreateConfig();
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
