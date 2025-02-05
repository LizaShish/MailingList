using MailingList.Models;
using System.Runtime.CompilerServices;

namespace MailingList.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Contact> FilterBySearchString(this IQueryable<Contact> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(contact => contact.Name.Contains(searchString) ||
                 contact.Email.Contains(searchString));
            }
            return query;
        }

        public static IQueryable<EmailMessage> FilterBySearchString(this IQueryable<EmailMessage> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(email => email.Subject.Contains(searchString) ||
                  email.Body.Contains(searchString));
            }
            return query;
        }
    }
}
