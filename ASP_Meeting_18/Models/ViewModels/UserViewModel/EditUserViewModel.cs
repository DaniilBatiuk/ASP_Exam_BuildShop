﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.ViewModels.UserViewModel
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; } = default!;

        [Required]
        [Display(Name = "Login")]
        [MinLength(6)]
        public string UserName { get; set; } = default!;
        [Required]
        [Display(Name = "Date of Birth")]
        public int YearOfBirth { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
    }
}
