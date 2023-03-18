using Microsoft.AspNetCore.Identity;

namespace ASP_Meeting_18.Models.ViewModels.AccountViewModels
{
    public class ChangeRolesViewModel
    {
        public string UserId { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public IList<string> UserRoles { get; set; } = default!;
        public List<IdentityRole> AllRoles { get; set; } = default!;
    }
}
