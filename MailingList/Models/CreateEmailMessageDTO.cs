using System.ComponentModel.DataAnnotations;

namespace MailingList.Models
{
    public class CreateEmailMessageDTO
    {
       
        public string ContactId { get; set; }
        //public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        
    }
}
