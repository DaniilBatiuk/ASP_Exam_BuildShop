using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.ViewModels.UserViewModel
{
    public class ChangePasswordViewModel
    {

        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; } = default!;

        [Required]
        [StringLength(100, ErrorMessage = $"The Password must be at least 6 and at max 100 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; } = default!;

    }
}
