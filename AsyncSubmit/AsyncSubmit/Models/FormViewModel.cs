using System.ComponentModel.DataAnnotations;

namespace AsyncSubmit.Models
{
    public class FormViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
