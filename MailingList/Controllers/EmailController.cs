using MailingList.Dtos;
using MailingList.Interface;
using MailingList.Models;
using MailingList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MailingList.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly AppDBContext _appDBContext;
        private readonly IContactService _contactService;

        public EmailController(IEmailService emailService, IContactService contactService)
        {
            _emailService = emailService;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 10)
        {
            var readEmailDTO = await _emailService.GetSentEmailsAsync(searchString, page, pageSize);
            return View(readEmailDTO);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEmail()    
        {
            var сontacts = await _contactService.GetContactsForEmailCreating();

            var getCreateEmailDTO = new GetCreateEmailDTO
            {
                Contacts = сontacts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} ({c.Email})"
                }).ToList()
            };
            return View(getCreateEmailDTO);
        }  

        [HttpPost]
        public async Task<IActionResult> CreateEmail(CreateEmailMessageDTO createEmailMessageDTO)
        {
            if(!ModelState.IsValid)
            {
                return View("createEmailMessageDTO");
            }

            try
            {
                var email = await _contactService.GetContactEmailByIdAsync(createEmailMessageDTO.ContactId);

                await _emailService.SentEmailAsync(email, createEmailMessageDTO.Subject,
                    createEmailMessageDTO.Body);

                await _emailService.SaveEmailMessageAsync(createEmailMessageDTO);
                var сontacts = await _contactService.GetContactsForEmailCreating();

                var getCreateEmailDTO = new GetCreateEmailDTO
                {
                    Contacts = сontacts.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = $"{c.Name} ({c.Email})"
                    }).ToList()
                };
                return View("CreateEmail", getCreateEmailDTO);
            }

            catch (Exception ex)
            {
                ViewBag.Message = $"Ошибка при отправке письма: {ex.Message}";
            }

            return View("createEmailMessageDTO");
        }
    }
}
