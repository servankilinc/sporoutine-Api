namespace Domain.Models.Mail;

public class AzureMailServiceSettings
{
    public string ConnectionStringMailService { get; set; } = null!;
    public string DefaultMailSenderAddress { get; set; } = null!;
}
