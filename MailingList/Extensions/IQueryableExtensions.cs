using MailingList.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MailingList.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Contact> SearchContact(this IQueryable<Contact> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(contact => contact.Name.Contains(searchString) ||
                 contact.Email.Contains(searchString));
            }
            return query;
        }

        public static IQueryable<EmailMessage> SearchEmailMessage(this IQueryable<EmailMessage> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Include(email => email.Contact)
                    .Where(email => email.Subject.Contains(searchString) ||
                  email.Body.Contains(searchString) ||
                  email.Contact.Email.Contains(searchString));// select чтобы проверить все связанные контакты, из контакта достать поле email
            }
            return query;
        }
    }
}
