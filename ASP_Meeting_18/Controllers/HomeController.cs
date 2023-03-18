using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models;
using ASP_Meeting_18.Models.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_Meeting_18.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext context;

        public HomeController(ILogger<HomeController> logger, ShopDbContext context)
        {
            _logger = logger;
            this.context = context;
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