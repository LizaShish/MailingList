using System.ComponentModel.DataAnnotations;

namespace MailingList.Models
{
    public class CreateEmailMessageDTO
    {
       
        public Guid ?ContactId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        
    }
}
