using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;
using ASP_Meeting_18.Models.ViewModels.AccountViewModels;
using ASP_Meeting_18.Models.ViewModels.AdminViewModel;
using ASP_Meeting_18.Models.ViewModels.HomeViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace ASP_Meeting_18.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly IMapper mapper;
        public AdminController(ShopDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index(string? category, string? search, int page = 1)
        {
            IQueryable<Product> products = _context.Products
                .Include(t => t.Category)
                .Include(t => t.Images);
            if (category != null)
                products = products.Where(t => t.Category!.Title == category);
            if (search is not null)
                products = products.Where(t => t.Title.Contains(search));
            int itemsPerPage = 6;
            int pageCount = (int)Math.Ceiling((decimal)products.Count() / itemsPerPage);
            products = products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            IEnumerable<ProductDTO> temp = mapper.Map<IEnumerable<ProductDTO>>(await products.ToListAsync());
            IndexShopViewModel vm = new IndexShopViewModel
            {
                Products = temp,
                Category = category,
                Page = page,
                PageCount = pageCount
            };
            return View(vm);
        }
    }
}
