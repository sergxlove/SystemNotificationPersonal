using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net;
using System.Text;
using System.Text.Json;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.StartappConsole.Interfaces;
using SystemNotificationPersonal.StartappConsole.Models;

namespace SystemNotificationPersonal.StartappConsole.Cases
{
    public class ConsoleCases
    {
        public static ICommand[] UseConsoleCases()
        {
            ICommand[] commands =
            {
                new VersionCommand(),
                new HelpCommand(),
                new StartCommand(),
                new SettingsCommand(),
                new InfoCommand(),
                new StopCommand(),
            };
            return commands;
        }

        public class VersionCommand : ICommand
        {
            public string Name => "version";

            public string Description => "\n" +
                "Структура: version \n" +
                "Отвечает за вывод текущей версии приложения\n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                Console.WriteLine("\n" +
                    "Версия 1.0.0, developer sergxlove, 2025\n" +
                    "Все права защищены\n");
            }
        }

        public class HelpCommand : ICommand
        {
            public string Name => "help";

            public string Description => "\n" +
                "Структура: help \n" +
                "Отвечает за вывод информации о доступных командах \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                string information = "Доступные команды: \n" +
                    "? - вывод подробной информации о команде\n" +
                    "help - вывод доступных команд и краткой информации о них\n" +
                    "version - вывод информации о версии программы\n" +
                    "start - запуск оповещения персонала\n" +
                    "stop - остановка оповещения персонала\n" +
                    "setting - конфигурация приложения\n";
                Console.WriteLine(information);
            }
        }

        public class StartCommand : ICommand
        {
            public string Name => "start";

            public string Description => "\n" +
                "Структура: start \n" +
                "Отвечает за запуск оповещения об эвакуации";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                try
                {
                    await Task.CompletedTask;
                    string apiUrl = $"http://{data.Settings.AddressServer}/start";
                    Console.Write($"Введите логин:");
                    string login = Console.ReadLine()!;
                    Console.Write($"Введите пароль:");
                    string password = Console.ReadLine()!;
                    Console.WriteLine($"Введите тип маршрута:");
                    int variableExit = Convert.ToInt32(Console.ReadLine()!);
                    if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    {
                        Console.WriteLine("Логин и пароль не может иметь пустые значения");
                        Log.Warning("Ошибка при вводе логина или пароля");
                        return;
                    }
                    if (variableExit <= 0 || variableExit >= 6)
                    {
                        Console.WriteLine("Тип маршрута может быть только от 1 до 5");
                        Log.Warning("Ошибка при выборе типа маршрута");
                        return;
                    }
                    UsersEntity user = new()
                    {
                        Login = login,
                        Password = password
                    };
                    Request req = new Request()
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Password = user.Password,
                        VariableExit = variableExit,
                    };
                    using (var httpClient = new HttpClient())
                    {
                        string json = JsonSerializer.Serialize(req);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, content);
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                string responseBody = await response.Content.ReadAsStringAsync();
                                Console.WriteLine(responseBody);
                                Log.Information("Оповещение запущено");
                                break;
                            default:
                                Console.WriteLine($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                Log.Warning($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                    Log.Error($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        public class StopCommand : ICommand
        {
            public string Name => "stop";

            public string Description => "\n" +
                "Структура: start \n" +
                "Отвечает за остановку оповещения об эвакуации";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                string apiUrl = $"http://{data.Settings.AddressServer}/stop";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(apiUrl);
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                string responseBody = await response.Content.ReadAsStringAsync();
                                Console.WriteLine($"Успешно!");
                                Log.Information("Оповещение успешно запущено");
                                break;
                            default:
                                Console.WriteLine($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                Log.Warning($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                    Log.Error($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        public class SettingsCommand : ICommand
        {
            public string Name => "setting";

            public string Description => "\n" +
                "Структура: setting [Аргумент] \n" +
                "Отвечает за конфигурацию приложения \n" +
                "Аргументы: \n" +
                "--address(-a) [Параметр] - задает адрес сервера \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввести аргументы");
                    Log.Warning("Ошибка при вводе аргумента");
                    return;
                }
                Dictionary<string, string> argsPairs = new Dictionary<string, string>();
                bool isKey = true;
                string keyCash = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    if (isKey)
                    {
                        argsPairs.Add(args[i], string.Empty);
                        keyCash = args[i];
                        isKey = false;
                    }
                    else
                    {
                        argsPairs[keyCash] = args[i];
                        isKey = true;
                    }
                }
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "--address":
                        case "-a":
                            if (item.Value is null)
                            {
                                Console.WriteLine("Необходимо ввести параметр");
                                Log.Warning("Ошибка ввода параметра");
                                return;
                            }
                            data.Settings.AddressServer = item.Value;
                            data.Settings.CreateConfig();
                            Log.Information($"Адрес изменен на {data.Settings.AddressServer}");
                            break;
                        default:
                            Console.WriteLine($"Неизвестный аргумент: {item.Key}");
                            Log.Warning($"Неизвестный аргумент: {item.Key}");
                            break;
                    }
                }
            }
        }

        public class InfoCommand : ICommand
        {
            public string Name => "?";

            public string Description => "\n" +
                "Структура: ? [Параметр] \n" +
                "Отвечает за предоставление подробной информации о командах \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine(Description);
                }
                else
                {
                    ICommand cmd;
                    switch (args[0])
                    {
                        case "start":
                            cmd = new StartCommand();
                            break;
                        case "stop":
                            cmd = new StopCommand();
                            break;
                        case "version":
                            cmd = new VersionCommand();
                            break;
                        case "help":
                            cmd = new HelpCommand();
                            break;
                        case "setting":
                            cmd = new SettingsCommand();
                            break;
                        default:
                            Console.WriteLine($"Неизвестный аргумент: {args[0]}");
                            return;
                    }
                    Console.WriteLine(cmd.Description);
                }
            }
        }
    }
}
