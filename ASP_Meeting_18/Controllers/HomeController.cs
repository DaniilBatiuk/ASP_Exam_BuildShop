using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models;
using ASP_Meeting_18.Models.DTO;
using ASP_Meeting_18.Models.ViewModels.AdminViewModel;
using ASP_Meeting_18.Models.ViewModels.HomeViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_Meeting_18.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext context;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, ShopDbContext context, IMapper mapper)
        {
            _logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string? category, int page = 1)
        {
            IQueryable<Product> products = context.Products
                .Include(t => t.Category)
                .Include(t => t.Images);
            if (category != null)
                products = products.Where(t => t.Category!.Title == category);

            int itemsPerPage = 4;
            int pageCount = (int)Math.Ceiling((decimal)products.Count() / itemsPerPage);
            products = products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Products = await products.ToListAsync(),
                Category = category,
                Page = page,
                PageCount = pageCount
            };
            return View(vm);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products
                .Include(c => c.Category)
                .Include(c => c.Category!.ParentCategory)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            DetailsProductViewModel vM = new DetailsProductViewModel
            {
                Product = mapper.Map<ProductDTO>(product)
            };
            return View(vM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}