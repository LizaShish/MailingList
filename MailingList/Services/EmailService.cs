using MailingList.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace MailingList.Services
{
    public class EmailService
    {
        private readonly AppDBContext _appDBContext;
        private readonly IConfiguration _configuration;

        public EmailService(AppDBContext appDBContext, IConfiguration configuration)
        {
            _appDBContext = appDBContext;
            _configuration = configuration;
            
        }
        public async Task<ReadEmailDTO> GetSentEmailsAsync(string searchString, int page = 1, int pageSize = 10)
        {
            if (page < 1)
            {
                page = 1;
            }

            var EmailsQuery = _appDBContext.EmailMessages.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                EmailsQuery = EmailsQuery.Where
                (email =>
                 //email.Email.Contains(searchString) ||
                 email.Subject.Contains(searchString) ||
                 email.Body.Contains(searchString)
                );
            }

            var totalItems = await EmailsQuery.CountAsync();

            var paginatedEmails = await EmailsQuery
                .OrderByDescending(email => email.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int startEmal = (page - 1) * pageSize + 1;
            int endEmal = Math.Min(startEmal + pageSize - 1, totalItems);

            var modelEmail = new ReadEmailDTO
            {
                Emails = paginatedEmails,
                CurrentPage = page,
                TotalItems = totalItems,
                PageSize = pageSize,
                SearchTerm = searchString
            };


            return modelEmail;
        }

        public async Task SentEmailAsync(string recipient, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress(" ", recipient)); // брать с фронта
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using(var client  = new SmtpClient())
            {
                try
                {
                    var smtpServer = emailSettings["SmtpServer"];
                    await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), MailKit.Security.SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(emailSettings["UserName"], emailSettings["Password"]);
                    var result = await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки: {ex.Message}");

                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }

        public async Task SaveEmailMessageAsync(CreateEmailMessageDTO emailMessageDTO)
        {
            if (Guid.TryParse(emailMessageDTO.ContactId, out var contactGuid))
            {
                var emailMessage = new EmailMessage
                {
                    Id = Guid.NewGuid(),
                    //3 Сохранить контактайди
                    ContactId = contactGuid,
                    Subject = emailMessageDTO.Subject,
                    Body = emailMessageDTO.Body,
                    CreatedOn = DateTime.UtcNow

                };
                _appDBContext.EmailMessages.Add(emailMessage);
                await _appDBContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("ContactId должен быть допустимым Guid.");
            }
           
        }
    }
}
