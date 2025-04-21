using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.EmailService;
using Azure.Communication.Email;
using Azure;
using Domain.Models.Mail;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailClient _emailClient;
    private readonly AzureMailServiceSettings _azureMailServiceSettings;
    private readonly ILogger<EmailService> _loger;

    public EmailService(EmailClient emailClient, AzureMailServiceSettings azureMailServiceSettings, ILogger<EmailService> loger)
    {
        _emailClient = emailClient;
        _azureMailServiceSettings = azureMailServiceSettings;
        _loger = loger;
    }


    public async Task SendMailAsync(MailSendModel mailSendModel, CancellationToken cancellationToken = default)
    {
        EmailMessage message = MessageGenerator(mailSendModel);

        EmailSendOperation emailSendOperation = await _emailClient.SendAsync(WaitUntil.Completed, message, cancellationToken);

        EmailSendResult sendResult = emailSendOperation.Value;

        if (sendResult.Status == EmailSendStatus.Succeeded)
        {
            _loger.LogInformation("Email Sent Successfully");
        }
        else
        {
            throw new BusinessException($"Failed to send email. Status: {sendResult.Status} detail : {JsonConvert.SerializeObject(message)}");
        }
    }

    private EmailMessage MessageGenerator(MailSendModel mailSendModel)
    {
        if (mailSendModel.RecipientEmailList == null || mailSendModel.RecipientEmailList.Count == 0) throw new ArgumentNullException("RecipientEmailList required!");

        string senderAddress = string.IsNullOrWhiteSpace(mailSendModel.SenderEmailAddress) == false ? mailSendModel.SenderEmailAddress : _azureMailServiceSettings.DefaultMailSenderAddress;

        var content = new EmailContent(mailSendModel.Subject);

        if (!string.IsNullOrEmpty(mailSendModel.HtmlContent))
            content.Html = mailSendModel.HtmlContent;

        List<EmailAddress> emailAddresses = mailSendModel.RecipientEmailList.Select(e => new EmailAddress(e)).ToList();

        var recipients = new EmailRecipients(emailAddresses);

        return new EmailMessage(
            senderAddress: senderAddress,
            content: content,
            recipients: recipients
        );
    }
}
