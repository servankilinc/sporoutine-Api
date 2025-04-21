using Domain.Models.Mail;

namespace Application.Services.EmailService;

public interface IEmailService
{
    Task SendMailAsync(MailSendModel mailSendModel, CancellationToken cancellationToken = default);
}
