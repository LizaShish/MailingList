using MailingList.Dtos;
using MailingList.Extensions;
using MailingList.Interface;
using MailingList.Models;
using Microsoft.EntityFrameworkCore;

namespace MailingList.Services
{
    public class ContactService : IContactService
    {
        public readonly AppDBContext _appDBContext;

        public ContactService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<List<ContactDTO>> GetContactsForEmailCreating()
        {
            var contacts = await _appDBContext.Contacts.Select(c => new ContactDTO
            {
                Email = c.Email,
                Name = c.Name,
                Id = c.Id
            }) .ToListAsync();

            return contacts;
        }

        public async Task<string> GetEmailByContactIdAsync(Guid contactId)
        {
           
            var contact = await _appDBContext.Contacts
                .Where(c => c.Id == contactId)
                .FirstOrDefaultAsync();

            if (contact != null)
            {
                return contact.Email;
            }
            else
            {
                return null;
            }
        }

        public async Task<ReadContactDTO> GetContactsAsync(string searchString = null, int page = 1, int pageSize = 10)
        {
            if (page < 1)
            {
                page = 1;
            }

            var contactsQuery = _appDBContext.Contacts
                .SearchContact(searchString);

            var contactsCount = await contactsQuery.CountAsync();

            var paginatedContacts = await contactsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int startContact = (page - 1) * pageSize + 1;
            int endContact = Math.Min(startContact + pageSize - 1, contactsCount);
            var modelContact = new ReadContactDTO
            {
                Contacts = paginatedContacts,
                CurrentPage = page,
                TotalItems = contactsCount,
                PageSize = pageSize,
                SearchTerm = searchString
            };

            return modelContact;
        }
        public async Task CreateContactAsync (CreateContactDTO createContactDTO)
        {
            var contact = new Contact
            {
                Name = createContactDTO.Name,
                Email = createContactDTO.Email
            };

            _appDBContext.Contacts.Add(contact);
            await _appDBContext.SaveChangesAsync();
        }
    }
}
