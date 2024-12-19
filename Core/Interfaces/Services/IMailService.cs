namespace Core.Interfaces.Services;

public interface IMailService
{
    Task SendAgreementEmailAsync(string producerId, string producerUsername, string performerId, string performerEmail);
}
