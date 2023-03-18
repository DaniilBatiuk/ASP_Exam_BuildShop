using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.DTO
{
    public class CreateUserDTO
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; } = default!;
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Required]
        [Display(Name = "Date of Birth")]
        public int YearOfBirth { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
    }
}
