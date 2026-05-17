using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.ConfigServerWeb.Endpoints
{
    public static class ConfigEndpoints
    {
        public static IEndpointRouteBuilder MapConfigEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/server/config", ([FromServices] AppSettingServer settings) =>
            {
                try
                {
                    Log.Information("Конфигурация сервера получена");
                    return Results.Ok(new
                    {
                        settings.Port,
                        settings.ConnectionString,
                        settings.IPAddressCors,
                        settings.Protocol,
                        settings.PathExe
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem("Ошибка при получении конфигурации");
                }
            });

            app.MapGet("/api/server/config/{field}", (string field,
                [FromServices] AppSettingServer settings) =>
            {
                try
                {
                    object? value;

                    switch (field.ToLower())
                    {
                        case "port":
                            value = settings.Port;
                            break;
                        case "connectionstring":
                            value = settings.ConnectionString;
                            break;
                        case "ipaddresscors":
                            value = settings.IPAddressCors;
                            break;
                        case "protocol":
                            value = settings.Protocol;
                            break;
                        case "pathexe":
                            value = settings.PathExe;
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

            app.MapPut("/api/server/config", async (HttpContext httpContext,
                [FromServices] AppSettingServer settings) =>
            {
                try
                {
                    var body = await httpContext.Request.ReadFromJsonAsync<Dictionary<string, object>>();

                    if (body == null)
                        return Results.BadRequest("Данные не предоставлены");

                    if (body.ContainsKey("port") && body["port"] != null)
                        settings.Port = Convert.ToInt32(body["port"]);

                    if (body.ContainsKey("connectionString") && body["connectionString"] != null)
                        settings.ConnectionString = "Data Source=" + body["connectionString"].ToString();

                    if (body.ContainsKey("ipAddressCors") && body["ipAddressCors"] != null)
                        settings.IPAddressCors = body["ipAddressCors"].ToString()!;

                    if (body.ContainsKey("protocol") && body["protocol"] != null)
                        settings.Protocol = body["protocol"].ToString()!;

                    settings.CreateConfig();

                    Log.Information($"Конфигурация обновлена: Port={settings.Port}, " +
                        $"ConnectionString={settings.ConnectionString}, " +
                        $"IPAddressCors={settings.IPAddressCors}, Protocol={settings.Protocol}");

                    return Results.Ok(new { message = "Конфигурация успешно обновлена" });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/api/server/config/reset", ([FromServices] AppSettingServer settings) =>
            {
                try
                {
                    AppSettingServer defaultSettings = new();
                    settings.Copy(defaultSettings);
                    settings.CreateConfig();

                    Log.Information("Конфигурация сброшена до стандартных");
                    return Results.Ok(new { message = "Конфигурация успешно сброшена" });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/api/server/info", ([FromServices] AppSettingServer settings) =>
            {
                try
                {
                    var info = settings.ToString();
                    Log.Information("Информация о конфигурации сервера запрошена");
                    return Results.Ok(new { info });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/api/users", async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                try
                {
                    var body = await httpContext.Request.ReadFromJsonAsync<Dictionary<string, string>>();

                    if (body == null || !body.ContainsKey("login") || !body.ContainsKey("password"))
                        return Results.BadRequest("Необходимо ввести логин и пароль");

                    string login = body["login"];
                    string password = body["password"];

                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(TimeSpan.FromSeconds(15));

                    var userRepo = httpContext.RequestServices.GetRequiredService<IUsersRepository>();
                    var users = new UsersEntity
                    {
                        Id = Guid.NewGuid(),
                        Login = login,
                        Password = password
                    };

                    await userRepo.AddAsync(users, cts.Token);

                    Log.Information($"Профиль добавлен: {users.Login}");
                    return Results.Ok(new { message = "Профиль успешно добавлен", login = users.Login });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/api/users/password", async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                try
                {
                    var body = await httpContext.Request.ReadFromJsonAsync<Dictionary<string, string>>();

                    if (body == null || !body.ContainsKey("login") || !body.ContainsKey("oldPassword") || !body.ContainsKey("newPassword"))
                        return Results.BadRequest("Необходимо ввести логин, старый и новый пароль");

                    string login = body["login"];
                    string oldPassword = body["oldPassword"];
                    string newPassword = body["newPassword"];

                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(TimeSpan.FromSeconds(5));

                    var userRepo = httpContext.RequestServices.GetRequiredService<IUsersRepository>();
                    var users = new UsersEntity
                    {
                        Id = Guid.NewGuid(),
                        Login = login,
                        Password = oldPassword
                    };

                    if (!await userRepo.VerifyAsync(users, cts.Token))
                        return Results.BadRequest("Неверный логин или пароль");

                    users.Password = newPassword;
                    await userRepo.UpdatePasswordAsync(users, cts.Token);

                    Log.Information($"Пароль профиля {login} обновлен");
                    return Results.Ok(new { message = $"Пароль профиля {login} успешно обновлен" });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/api/users/{login}", async (string login, HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                try
                {
                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(TimeSpan.FromSeconds(5));

                    var userRepo = httpContext.RequestServices.GetRequiredService<IUsersRepository>();
                    await userRepo.DeleteAsync(login, cts.Token);

                    Log.Information($"Профиль {login} удален");
                    return Results.Ok(new { message = $"Профиль {login} удален" });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/api/users/verify", async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                try
                {
                    var body = await httpContext.Request.ReadFromJsonAsync<Dictionary<string, string>>();

                    if (body == null || !body.ContainsKey("login") || !body.ContainsKey("password"))
                        return Results.BadRequest("Необходимо ввести логин и пароль");

                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(TimeSpan.FromSeconds(5));

                    var userRepo = httpContext.RequestServices.GetRequiredService<IUsersRepository>();
                    var users = new UsersEntity
                    {
                        Id = Guid.NewGuid(),
                        Login = body["login"],
                        Password = body["password"]
                    };

                    var isValid = await userRepo.VerifyAsync(users, cts.Token);

                    if (isValid)
                    {
                        Log.Information($"Пользователь {body["login"]} верифицирован");
                        return Results.Ok(new { valid = true, message = "Пользователь существует" });
                    }
                    else
                    {
                        Log.Warning($"Неудачная попытка верификации для {body["login"]}");
                        return Results.Ok(new { valid = false, message = "Неверный логин или пароль" });
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/api/server/start", (bool runAsAdmin,
                [FromServices] AppSettingServer settings) =>
            {
                try
                {
                    string appPath = settings.PathExe;

                    if (string.IsNullOrEmpty(appPath))
                        return Results.BadRequest("Путь к серверу не указан в конфигурации");

                    if (!File.Exists(appPath))
                        return Results.NotFound($"Файл сервера не найден по пути: {appPath}");

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
                        ? "Сервер запущен с правами администратора"
                        : "Сервер запущен";

                    Log.Information(message);
                    return Results.Ok(new { message });
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
