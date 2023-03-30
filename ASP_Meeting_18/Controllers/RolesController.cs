using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ASP_Meeting_18.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index() => View(await roleManager.Roles.ToListAsync());

        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityRole newRole = new IdentityRole(name);
                IdentityResult result = await roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            else
                ModelState.AddModelError(string.Empty, "Role name can not be empty!");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
                return NotFound();
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
                await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> UserList()
        {
            var users = await userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ChangeUserRoles(string? id)
        {
            if (id == null)
                return NotFound();
            User user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            List<IdentityRole> allRoles = await roleManager.Roles.ToListAsync();
            ChangeRolesViewModel vm = new ChangeRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserRoles(string? id, List<string> roles)
        {
            if (id == null) return NotFound();
            User user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            List<string> allRoles = await roleManager.Roles.Select(t => t.Name).ToListAsync();
            IEnumerable<string> addedRoles = roles.Except(userRoles);
            IEnumerable<string> deletedRoles = userRoles.Except(roles);
            await userManager.AddToRolesAsync(user, addedRoles);
            await userManager.RemoveFromRolesAsync(user, deletedRoles);
            return RedirectToAction("UserList");
        }


    }
    
}
