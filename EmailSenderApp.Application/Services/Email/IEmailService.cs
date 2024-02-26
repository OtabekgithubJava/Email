using System.Threading.Tasks;
using EmailSenderApp.Domain.Entities.Models;

namespace EmailSenderApp.Application.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel model);
        Task<bool> CheckEmailAsync(string code);
        Task SetPassword(User user);
        Task<bool> IsUserRegistered(string email);
        Task<bool> VerifyCredentials(User user);
    }
}
