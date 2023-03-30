using ASP_Meeting_18.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP_Meeting_18.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClaimsController : Controller
    {
        private readonly UserManager<User> userManager;

        public ClaimsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(User?.Claims);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create (string claimType,string claimValue)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null) {
                Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
                var result = await userManager.AddClaimAsync(user, claim);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else { Errors(result); }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public void Errors(IdentityResult identityResult)
        {
            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string claimsInfo)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string[] info = claimsInfo.Split(';');
            IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);
            Claim? claimForDelete = claims.FirstOrDefault(t => t.Type == info[0] && t.Value == info[1] && t.ValueType == info[2]);
            await userManager.RemoveClaimAsync(user, claimForDelete);
            return RedirectToAction("Index");
        }
        //[Authorize(Roles = "admin,manager")]
        [Authorize(Policy = "FrameworkPolicy")]
        public IActionResult TestPolicy1()
        {
            return View("Index",User.Claims);
        }
    }
}
