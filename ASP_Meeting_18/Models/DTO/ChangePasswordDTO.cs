using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.DTO
{
    public class ChangePasswordDTO
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; } = default!;
    }
}
