using System.ComponentModel.DataAnnotations;

namespace MailingList.Models
    
{
    public class CreateContactDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

    }
}
