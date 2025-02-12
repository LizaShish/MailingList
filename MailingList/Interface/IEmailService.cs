using MailingList.Models;

namespace MailingList.Interface
{
    public interface IEmailService
    {
        Task<ReadEmailDTO> GetSentEmailsAsync(string searchString, int page = 1, int pageSize = 10);
        Task SentEmailAsync(string recipient, string subject, string body);
        Task SaveEmailMessageAsync(CreateEmailMessageDTO emailMessageDTO);
    }
}
