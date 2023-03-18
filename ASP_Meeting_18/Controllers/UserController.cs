using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Meeting_18.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var users = userManager.Users;
            //Use AutoMapper in Future!!!
            IEnumerable<UserDTO> usersDTO = mapper.Map<IEnumerable<UserDTO>>(await users.ToListAsync());
            return View(usersDTO);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDTO dto)
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
            EditUserDTO dto = mapper.Map<EditUserDTO>(user);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserDTO dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(dto.Id);
                if (user == null) return NotFound();
                user.UserName = dto.Login;
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
            ChangePasswordDTO dto = new ChangePasswordDTO
            {
                Id = user.Id,
                Email = user.Email
            };
            return View(dto);
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
