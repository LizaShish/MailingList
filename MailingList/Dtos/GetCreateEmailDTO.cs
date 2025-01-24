using MailingList.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MailingList.Dtos
{
    public class GetCreateEmailDTO
    {
        public List<SelectListItem> Contacts { get; set; }

        public string SelectedContactEmail { get; set; }
       public string Subject { get; set; }
       public string Body { get; set; }  // add contact lists     1

        public List<Contact> ContactList { get; set; }
    }
}
