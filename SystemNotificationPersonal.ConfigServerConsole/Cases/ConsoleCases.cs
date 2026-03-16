using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using SystemNotificationPersonal.ConfigServerConsole.Interfaces;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.ConfigServerConsole.Cases
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
                new StopCommand(),
                new SettingCommand(),
                new ProfilesCommand(),
                new InfoCommand()
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
                    "exit - выход из программы\n" +
                    "help - вывод доступных команд и краткой информации о них\n" +
                    "version - вывод информации о версии программы\n" +
                    "start - запуск фоновой задачи системы оповещения персонала\n" +
                    "stop - остановка фоновой задачи системы оповещения персонала\n" +
                    "setting - конфигурация приложения\n" +
                    "profiles - отвечает за операции с профилями\n";
                Console.WriteLine(information);
            }
        }

        public class StartCommand : ICommand
        {
            public string Name => "start";

            public string Description => "\n" +
                "Структура: start \n" +
                "Отвечает за запуск сервера системы оповещения персонала \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                if (!File.Exists(data.Settings.PathExe))
                {
                    Console.WriteLine("Файл задачи не найден!");
                    return;
                }
                if (data.StartingProcess != null && !data.StartingProcess.HasExited)
                {
                    Console.WriteLine("Фоновая задача уже запущена!");
                    return;
                }
                ProcessStartInfo processInfo;
                if (args.Length != 0)
                {
                    switch (args[0])
                    {
                        case "-r":
                            processInfo = new ProcessStartInfo
                            {
                                FileName = data.Settings.PathExe,
                                UseShellExecute = true,
                                WindowStyle = ProcessWindowStyle.Normal,
                                Verb = "runas"
                            };
                            break;
                        default:
                            return;
                    }
                }
                else
                {
                    processInfo = new ProcessStartInfo
                    {
                        FileName = data.Settings.PathExe,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal
                    };
                }
                data.StartingProcess = Process.Start(processInfo);
            }
        }

        public class StopCommand : ICommand
        {
            public string Name => "stop";

            public string Description => "\n" +
                "Структура: stop \n" +
                "Отвечает за остановку сервера системы оповещения персонала \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                try
                {
                    if (data.StartingProcess == null) return;
                    if (!data.StartingProcess.HasExited)
                    {
                        data.StartingProcess.CloseMainWindow();
                        data.StartingProcess.WaitForExit(5000);
                        if (!data.StartingProcess.HasExited)
                        {
                            data.StartingProcess.Kill();
                        }
                    }
                    data.StartingProcess.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public class SettingCommand : ICommand
        {
            public string Name => "setting";

            public string Description => "\n" +
                "Структура: setting [Параметр] \n" +
                "Отвечает за конфигурацию приложения \n" +
                "Аргументы: \n" +
                "--port(-p) [Параметр] - указывает на порт сервера" +
                "--protocol(-pr) [Параметр] - указывает на используемый сетевой протокол" +
                "--cors(-c) [Параметр] - указывает на доступный диапозон адресов" +
                "--connectionString(-cs) [Параметр] - указывает на путь к базе данных";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввести аргументы");
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
                    foreach (var item in argsPairs)
                    {
                        switch (item.Key)
                        {
                            case "--protocol":
                            case "-pr":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                if (item.Value != "http" && item.Value != "https")
                                {
                                    Console.WriteLine("Доступные варианты протокола: http, https");
                                    return;
                                }
                                data.Settings.Protocol = item.Value;
                                data.Settings.CreateConfig();
                                break;
                            case "--cors":
                            case "-c":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                data.Settings.IPAddressCors = item.Value;
                                data.Settings.CreateConfig();
                                break;
                            case "--connectionString":
                            case "-cs":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                data.Settings.ConnectionString = "Data Source=" + item.Value;
                                data.Settings.CreateConfig();
                                break;
                            case "--port":
                            case "-p":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                data.Settings.Port = Convert.ToInt32(item.Value);
                                data.Settings.CreateConfig();
                                break;
                            default:
                                Console.WriteLine($"Неизвестный аргумент: {item.Key}");
                                break;
                        }
                    }
                }
            }
        }

        public class ProfilesCommand : ICommand
        {
            public string Name => "profiles";

            public string Description => "\n" +
                "Структура: profiles [Параметр] \n" +
                "Отвечает за настройку профилей \n" +
                "Аргументы: \n" +
                "--create(-c) - вызывает создание пользователя \n" +
                "--delete(-d) - вызывает удаление пользователя \n" +
                "--update(-u) - вызывает обновление пользоввателя \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                if (args.Length == 0)
                {
                    Console.WriteLine("Необходимо ввести аргументы");
                    return;
                }
                string login = string.Empty;
                string password = string.Empty;
                var userService = provider.GetService<IUsersService>();
                switch (args[0])
                {
                    case "--create":
                    case "-c":
                        Console.Write("Введите логин нового пользователя: ");
                        login = Console.ReadLine()!;
                        Console.Write("Введите пароль нового пользователя: ");
                        password = Console.ReadLine()!;
                        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                        {
                            Console.WriteLine("Логин и пароль не может иметь пустые значения");
                            return;
                        }
                        UsersEntity user = new UsersEntity()
                        {
                            Id = Guid.NewGuid(),
                            Login = login,
                            Password = password
                        };
                        await userService!.AddAsync(user);
                        Console.WriteLine("Пользователь создан");
                        break;
                    case "--update":
                    case "-u":
                        Console.Write("Введите логин пользователя");
                        login = Console.ReadLine()!;
                        Console.Write("Введите новый пароль");
                        password = Console.ReadLine()!;
                        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                        {
                            Console.WriteLine("Логин и пароль не может иметь пустые значения");
                            return;
                        }
                        user = new UsersEntity()
                        {
                            Id = Guid.NewGuid(),
                            Login = login,
                            Password = password
                        };
                        if (!await userService!.CheckAsync(login))
                        {
                            Console.WriteLine("Пользователь с таким логином не найден");
                            return;
                        }
                        await userService.UpdateAsync(user);
                        Console.WriteLine("Пароль пользователя обновлен");
                        break;
                    case "--delete":
                    case "-d":
                        Console.Write("Введите логин пользователя");
                        login = Console.ReadLine()!;
                        if (string.IsNullOrEmpty(login))
                        {
                            Console.WriteLine("Логин не может иметь пустое значение");
                            return;
                        }
                        if (!await userService!.CheckAsync(login))
                        {
                            Console.WriteLine("Пользователь с таким логином не найден");
                            return;
                        }
                        await userService.DeleteAsync(login);
                        Console.WriteLine("Пользователь удален");
                        break;
                    default:
                        Console.WriteLine($"Неизвестный аргумент: {args[0]}");
                        break;
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
                            cmd = new SettingCommand();
                            break;
                        case "profiles":
                            cmd = new ProfilesCommand();
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
