using ASP_Meeting_18.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Meeting_18.ViewComponents
{
    public class CategoriesMenu : ViewComponent
    {
        private readonly ShopDbContext context;

        public CategoriesMenu(ShopDbContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? currentCategory,string controller)
        {
            List<Category> categoryNames = await context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync();
            List<Category> ww = await context.Categories.Where(p => p.ChildCategoies != null).Distinct().ToListAsync();
            //List<string> innerCategory2 = await context.Products.Include(t => t.Category).Select(t => t.Category!.Title).Distinct().ToListAsync();
            List<string> innerCategory = await context.Categories.Where(p => p.ParentCategoryId != null).Select(t => t.Title).Distinct().ToListAsync();       
            return View(new Tuple<List<Category>, string?, List<string>,string>(categoryNames, currentCategory, innerCategory,controller));
        }
    }
}
