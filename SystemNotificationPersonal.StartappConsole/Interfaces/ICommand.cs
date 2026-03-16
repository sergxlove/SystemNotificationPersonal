using Microsoft.Extensions.DependencyInjection;

namespace SystemNotificationPersonal.StartappConsole.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Task Execute(string[] args, DataCore data, ServiceProvider provider);
    }
}
