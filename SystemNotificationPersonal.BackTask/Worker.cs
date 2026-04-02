using Microsoft.AspNetCore.SignalR.Client;
using Serilog;
using System.Diagnostics;
using SystemNotificationPersonal.BackTask.Models;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.BackTask
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HubConnection _hubConnection;
        private AppSettingClient _defaultConfig;
        private Process? _process;

        public Worker(ILogger<Worker> logger)
        {
            _defaultConfig = new();
            _defaultConfig.ReadConfig();
            _logger = logger;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{_defaultConfig.AddressServer}/notify")
                .WithAutomaticReconnect()
                .Build();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("")
                .CreateLogger();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _hubConnection.On<Message>("ReceiveMessage", async (message) =>
            {
                await Task.CompletedTask;
                _logger.LogInformation($"Received message: {message.TypeWork}");
                _defaultConfig.CodeHash = message.CodeExit;
                _defaultConfig.VariableExit = message.VariableExit;
                _defaultConfig.CreateConfig();
                switch (message.TypeWork)
                {
                    case "start":
                        _process = OpenExe();
                        Log.Information("Оповещение запущено");
                        break;
                    case "stop":
                        if (_process == null) return;
                        if (!_process.HasExited)
                        {
                            _process.CloseMainWindow();
                            _process.WaitForExit(5000);
                            if (!_process.HasExited)
                            {
                                _process.Kill();
                            }
                        }
                        _process.Dispose();
                        Log.Information("Оповещение остановлено");
                        break;
                    default:
                        break;
                }
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_hubConnection!.State == HubConnectionState.Disconnected ||
                        _hubConnection.State == HubConnectionState.Reconnecting)
                    {
                        await _hubConnection.StartAsync(stoppingToken);
                        _logger.LogInformation("Connected to SignalR Hub");

                        while (!stoppingToken.IsCancellationRequested)
                        {
                            await Task.Delay(1000, stoppingToken);
                            if (_hubConnection.State == HubConnectionState.Reconnecting)
                            {
                                await _hubConnection.StopAsync(stoppingToken);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "SignalR connection error");
                    Log.Information(ex.Message);
                    await Task.Delay(5000);
                }
            }
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        private Process? OpenExe()
        {
            try
            {
                string appPath = _defaultConfig.PathExe;
                var processInfo = new ProcessStartInfo
                {
                    FileName = appPath,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                return Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
