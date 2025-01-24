using MailingList.Models;
using MailingList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MailingList.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
           
        }

        [HttpGet]
        public  async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 10)
        {
            var readContactsDTO = await _contactService.GetContactsAsync(searchString, page, pageSize);
            return View(readContactsDTO); 
            // достать контакты в сервисе 

            //var contactList = await _contactService.GetContactEmailByIdAsync( contactId);
            //return View(contactList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateContact() 
        {
            return View();
            
        }

            [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDTO createContactDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createContactDTO);  
            }

            await _contactService.CreateContactAsync(createContactDTO);
            
            return RedirectToAction("Index");
        }
    }
}
