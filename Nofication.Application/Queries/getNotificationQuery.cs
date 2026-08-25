using MediatR;
using Nofication.Application.Interfaces;


namespace Nofication.Application.Queries
{
    public record getNotificationQuery(string recipient, string message) : IRequest<Unit>;

    public class getNotificationQueryHandler(IEmailService repository) : IRequestHandler<getNotificationQuery, Unit>
    {
        public async Task<Unit> Handle(getNotificationQuery request, CancellationToken cancellationToken)
        {
            bool result = await repository.SendEmail(request.recipient, request.message);

            if (!result)
            {
                throw new Exception("Email was not sent.");
            }

            return Unit.Value;
        }
    }
}
