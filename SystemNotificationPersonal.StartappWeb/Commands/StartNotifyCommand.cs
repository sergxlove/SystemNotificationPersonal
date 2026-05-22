using MediatR;

namespace SystemNotificationPersonal.StartappWeb.Commands
{
    public record class StartNotifyCommand(string Login, string Password, string AddressServer, 
        int VariableExit) : IRequest<IResult>;
}
