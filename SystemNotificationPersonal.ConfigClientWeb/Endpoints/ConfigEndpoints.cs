using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.ConfigClientWeb.Endpoints
{
    public static class ConfigEndpoints
    {
        public static IEndpointRouteBuilder MapConfigEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/config", ([FromServices] AppSettingClient settings) =>
            {
                try
                {
                    Log.Information("Конфигурация получена");
                    return Results.Ok(new
                    {
                        settings.AddressServer,
                        settings.PathExe,
                        settings.Theme,
                        settings.Header,
                        settings.TimeBeforeOffPC
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem("Ошибка при получении конфигурации");
                }
            });

            app.MapGet("/api/config/{field}", (string field,
                [FromServices] AppSettingClient settings) =>
            {
                try
                {
                    object? value;

                    switch (field.ToLower())
                    {
                        case "addressserver":
                            value = settings.AddressServer;
                            break;
                        case "pathexe":
                            value = settings.PathExe;
                            break;
                        case "theme":
                            value = settings.Theme;
                            break;
                        case "header":
                            value = settings.Header;
                            break;
                        case "timebeforeoffpc":
                            value = settings.TimeBeforeOffPC;
                            break;
                        default:
                            return Results.NotFound($"Поле '{field}' не найдено");
                    }

                    return Results.Ok(new { field, value });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/api/config", async (HttpContext httpContext,
                [FromServices] AppSettingClient settings) =>
            {
                try
                {
                    var body = await httpContext.Request.ReadFromJsonAsync<Dictionary<string, object>>();

                    if (body == null)
                        return Results.BadRequest("Данные не предоставлены");

                    if (body.ContainsKey("addressServer") && body["addressServer"] != null)
                        settings.AddressServer = body["addressServer"].ToString()!;

                    if (body.ContainsKey("pathExe") && body["pathExe"] != null)
                        settings.PathExe = body["pathExe"].ToString()!;

                    if (body.ContainsKey("theme") && body["theme"] != null)
                        settings.Theme = body["theme"].ToString()!;

                    if (body.ContainsKey("header") && body["header"] != null)
                        settings.Header = body["header"].ToString()!;

                    if (body.ContainsKey("timeBeforeOffPC") && body["timeBeforeOffPC"] != null)
                        settings.TimeBeforeOffPC = Convert.ToInt32(body["timeBeforeOffPC"]);

                    settings.CreateConfig();

                    Log.Information("Конфигурация обновлена");
                    return Results.Ok(new { message = "Конфигурация успешно обновлена" });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/api/config/reset", ([FromServices] AppSettingClient settings) =>
            {
                try
                {
                    AppSettingClient defaultSettings = new();
                    settings.Copy(defaultSettings);
                    settings.CreateConfig();

                    Log.Information("Конфигурация сброшена");
                    return Results.Ok(new { message = "Конфигурация успешно сброшена" });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/api/background/start", (bool runAsAdmin = false) =>
            {
                try
                {
                    string appPath = Path.Combine(Directory.GetCurrentDirectory(), "SystemNotificationPeopleBackTask.exe");

                    if (!File.Exists(appPath))
                        return Results.NotFound("Файл фоновой задачи не найден");

                    var processInfo = new ProcessStartInfo
                    {
                        FileName = appPath,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal
                    };

                    if (runAsAdmin)
                    {
                        processInfo.Verb = "runas";
                    }

                    Process.Start(processInfo);

                    var message = runAsAdmin
                        ? "Фоновая задача запущена с правами администратора"
                        : "Фоновая задача запущена";

                    Log.Information(message);
                    return Results.Ok(new { message });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/api/info", ([FromServices] AppSettingClient settings) =>
            {
                try
                {
                    var info = settings.ToString();
                    Log.Information("Информация о конфигурации запрошена");
                    return Results.Ok(new { info });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });


            return app;
        }
    }
}
