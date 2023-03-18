using System.ComponentModel.DataAnnotations;

namespace ASP_Meeting_18.Models.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Login")]
        public string Login { get; set; } = default!;
        [Required]
        [Display(Name ="Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required]
        [Display(Name ="Year of birth")]
        public int YearOfBirth { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The passwords must match!")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
