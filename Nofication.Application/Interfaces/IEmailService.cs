using MediatR;

namespace Nofication.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string recipient, string message);
    }
}


