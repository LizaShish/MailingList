using MailingList.Models;
using Microsoft.EntityFrameworkCore;

namespace MailingList
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
