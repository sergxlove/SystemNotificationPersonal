using SystemNotificationPersonal.StartappWeb.Extensions;

namespace SystemNotificationPersonal.StartappWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapAllEndpoints();

            app.Run();
        }
    }
}
