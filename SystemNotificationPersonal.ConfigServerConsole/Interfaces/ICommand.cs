using Microsoft.Extensions.DependencyInjection;

namespace SystemNotificationPersonal.ConfigServerConsole.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Task Execute(string[] args, DataCore data, ServiceProvider provider);
    }
}
