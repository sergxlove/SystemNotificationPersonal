using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics;
using SystemNotificationPersonal.ConfigClientConsole.Interfaces;

namespace SystemNotificationPersonal.ConfigClientConsole.Cases
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
                    "Все права защищены.\n");
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
                    "start - запуск фоновой задачи системы оповещения персонала\n" +
                    "stop - остановка фоновой задачи системы оповещения персонала\n" +
                    "setting - конфигурация приложения\n";
                Console.WriteLine(information);
            }
        }

        public class StartCommand : ICommand
        {
            public string Name => "start";

            public string Description => "\n" +
                "Структура: start \n" +
                "Отвечает за запуск фоновой задачи системы оповещения персонала \n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                try
                {
                    if (!File.Exists(data.Settings.PathExe))
                    {
                        Console.WriteLine("Файл задачи не найден");
                        return;
                    }
                    if (data.StartingProcess != null && !data.StartingProcess.HasExited)
                    {
                        Console.WriteLine("Фоновая задача уже запущена");
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
                                    FileName = Directory.GetCurrentDirectory() + "\\SystemNotificationPeopleBackTask.exe",
                                    UseShellExecute = true,
                                    WindowStyle = ProcessWindowStyle.Normal,
                                    Verb = "runas"
                                };
                                Log.Information("Фоновая задача запущена с правами администратора");
                                break;
                            default:
                                Console.WriteLine($"Неизвестный аргумент: {args[0]}");
                                return;
                        }
                    }
                    else
                    {
                        processInfo = new ProcessStartInfo
                        {
                            FileName = Directory.GetCurrentDirectory() + "\\SystemNotificationPeopleBackTask.exe",
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Normal
                        };
                        Log.Information("Фоновая задача запущена");
                    }
                    data.StartingProcess = Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Error(ex.Message);
                }
            }
        }

        public class StopCommand : ICommand
        {
            public string Name => "stop";

            public string Description => "\n" +
                "Структура: stop \n" +
                "Отвечает за остановку фоновой задачи системы оповещения персонала \n";

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
                    Log.Information("Фоновая задача остановлена");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Error(ex.Message);
                }
            }
        }

        public class SettingCommand : ICommand
        {
            public string Name => "setting";

            public string Description => "\n" +
                "Структура: setting [Аргумент] \n" +
                "Отвечает за конфигурацию приложения \n" +
                "Аргументы: \n" +
                "--address(-a) [Параметр] - задает адрес сервера \n" +
                "--exe(-e) [Параметр] - задает путь к исполняемому файлу\n" +
                "--header(-h) [Параметр] - задает заголовок формы оповещения\n" +
                "--theme(-t) [Параметр] - задает тему страницы оповещения\n" +
                "--time(-tm) [Параметр] - задает время до выключения компьютера в секундах\n";

            public async Task Execute(string[] args, DataCore data, ServiceProvider provider)
            {
                await Task.CompletedTask;
                try
                {
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
                                    return;
                                }
                                data.Settings.AddressServer = item.Value;
                                data.Settings.CreateConfig();
                                Log.Information($"Параметр адрес изменен на {data.Settings.AddressServer}");
                                break;
                            case "--exe":
                            case "-e":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                if (!File.Exists(item.Value))
                                {
                                    Console.WriteLine("Данный файл не существует");
                                    return;
                                }
                                if (!item.Value.EndsWith(".exe"))
                                {
                                    Console.WriteLine("Файл должен быть типа .exe");
                                    return;
                                }
                                data.Settings.PathExe = item.Value;
                                data.Settings.CreateConfig();
                                Log.Information($"Параметр путь исполняемого файла изменен на {data.Settings.PathExe}");
                                break;
                            case "--header":
                            case "-h":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                data.Settings.Header = item.Value;
                                data.Settings.CreateConfig();
                                Log.Information($"Параметр заголовки изменен на {data.Settings.Header}");
                                break;
                            case "--theme":
                            case "-t":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                if (item.Value != "dark" && item.Value != "light")
                                {
                                    Console.WriteLine("Тема может быть темной(dark) или светлой(light)");
                                    return;
                                }
                                data.Settings.Theme = item.Value;
                                data.Settings.CreateConfig();
                                Log.Information($"Параметр тема изменен на {data.Settings.Theme}");
                                break;
                            case "--time":
                            case "-tm":
                                if (item.Value is null)
                                {
                                    Console.WriteLine("Необходимо ввести параметр");
                                    return;
                                }
                                try
                                {
                                    int time = Convert.ToInt32(item.Value);
                                    if (time <= 0)
                                    {
                                        Console.WriteLine("Время должно быть больше 0");
                                        return;
                                    }
                                    data.Settings.TimeBeforeOffPC = time;
                                    data.Settings.CreateConfig();
                                    Log.Information($"Параметр адрес изменен на {data.Settings.AddressServer}");
                                }
                                catch
                                {
                                    Console.WriteLine("Произошла ошибка");
                                    return;
                                }
                                break;
                            default:
                                Console.WriteLine($"Неизвестный аргумент: {item.Key}");
                                break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Произошла ошибка");
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
