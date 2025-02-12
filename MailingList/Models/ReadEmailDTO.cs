namespace MailingList.Models
{
    public class ReadEmailDTO
    {
        public required List<EmailMessage> Emails { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public required string SearchTerm { get; set; }

        public int TotalPages { get; set; }
    }
}
