using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncSubmit.Models
{
    public class FormViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
