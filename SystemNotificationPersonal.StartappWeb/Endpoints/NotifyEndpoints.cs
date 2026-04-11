using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text;
using System.Text.Json;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.StartappWeb.Requests;

namespace SystemNotificationPersonal.StartappWeb.Endpoints
{
    public static class NotifyEndpoints
    {
        public static IEndpointRouteBuilder MapNotifyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/notify/start", async (HttpContext context, 
                [FromBody] StartNotifyRequest request,
                CancellationToken token) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
                    {
                        Log.Warning("Неправильный ввод логина или пароля");
                        return Results.BadRequest("Логин и пароль не могут быть пустыми");
                    }
                    if (string.IsNullOrEmpty(request.AddressServer))
                    {
                        Log.Warning("Неправильный ввод адреса сервера");
                        return Results.BadRequest("Адрес сервера не может быть пустым");
                    }
                    if (request.VariableExit <= 0 || request.VariableExit >= 6)
                    {
                        Log.Warning("Неправильный ввод типа маршрута");
                        return Results.BadRequest("Тип маршрута может быть только от 1 до 5");
                    }
                    var user = new UsersEntity
                    {
                        Login = request.Login,
                        Password = request.Password
                    };
                    LoginRequest loginRequest = new()
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Password = user.Password,
                        VariableExit = request.VariableExit,
                    };
                    string apiUrl = $"http://{request.AddressServer}/start";
                    using (var httpClient = new HttpClient())
                    {
                        string json = JsonSerializer.Serialize(loginRequest);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            Log.Information($"Оповещение запущено от {loginRequest.Login}");
                            return Results.Ok(responseBody);
                        }
                        else
                        {
                            string error = $"Ошибка: {response.StatusCode} - {response.ReasonPhrase}";
                            Log.Error(error);
                            return Results.StatusCode((int)response.StatusCode);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Log.Error(ex.Message);
                    return Results.InternalServerError(ex.Message);
                }
            });

            app.MapPost("/notify/stop", async (HttpContext context,
                [FromBody] AddressRequest request,
                CancellationToken token) =>
            {
                try 
                {
                    Log.Information("Получен запрос на остановку оповещения");
                    if (string.IsNullOrEmpty(request.AddressServer))
                    {
                        Log.Warning("Адрес сервера не указан");
                        return Results.BadRequest("Адрес сервера не указан");
                    }
                    string apiUrl = $"http://{request.AddressServer}/stop";
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            Log.Information("Оповещение остановлено");
                            return Results.Ok(responseBody);
                        }
                        else
                        {
                            string error = $"Ошибка: {response.StatusCode} - {response.ReasonPhrase}";
                            Log.Error(error);
                            return Results.StatusCode((int)response.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.InternalServerError(ex.Message);
                }
            });

            return app;
        }
    }
}
