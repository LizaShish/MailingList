namespace MailingList.Models
{
    public class EmailMessage
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public DateTime CreatedOn { get; set; } 

        public Guid ContactId {  get; set; }
        public Contact Contact { get; set; }
    }
}
