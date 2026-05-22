using MediatR;

namespace SystemNotificationPersonal.StartappWeb.Commands
{
    public record class StopNotifyCommand(string AddressServer) : IRequest<IResult>;
}
