using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;
using ASP_Meeting_18.Models.ViewModels.UserViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ASP_Meeting_18.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly ShopDbContext _context;
        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, IMapper mapper ,ShopDbContext context, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this._context = context;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = userManager.Users;
            IEnumerable<UserDTO> usersDTO = mapper.Map<IEnumerable<UserDTO>>(await users.ToListAsync());
            return View(usersDTO);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel dto)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = dto.Email,
                    UserName = dto.Login,
                    YearOfBirth = dto.YearOfBirth
                };
                var result = await userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            ////AutoMapper!!!
            //EditUserDTO dto = new EditUserDTO
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    Login = user.UserName,
            //    YearOfBirth = user.YearOfBirth
            //};
            EditUserViewModel dto = mapper.Map<EditUserViewModel>(user);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(dto.Id);
                if (user == null) return NotFound();
                user.UserName = dto.UserName;
                user.YearOfBirth = dto.YearOfBirth;
                user.Email = dto.Email;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(dto);
        }

        public async Task<IActionResult> ChangePassword(string? id)
        {
            if (id == null)
                return NotFound();

            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            ChangePasswordViewModel dto = new ChangePasswordViewModel
            {
                Id = user.Id,
                Email = user.Email
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return View(model);
            }

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            await _context.SaveChangesAsync();
            await signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string? id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

    }
}
