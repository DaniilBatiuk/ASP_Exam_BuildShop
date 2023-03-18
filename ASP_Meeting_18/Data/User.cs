using Microsoft.AspNetCore.Identity;

namespace ASP_Meeting_18.Data
{
    public class User : IdentityUser
    {
        public int YearOfBirth { get; set; }
    }
}
