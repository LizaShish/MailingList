using MailingList.Dtos;
using MailingList.Models;

namespace MailingList.Interface
{
    public interface IContactService
    {
        Task <List<ContactDTO>> GetContactsForEmailCreating();
        Task<string> GetContactEmailByIdAsync(string contactId);
        Task<ReadContactDTO> GetContactsAsync(string searchString = null, int page = 1, int pageSize = 10);
        Task CreateContactAsync(CreateContactDTO createContactDTO);
    }
}
