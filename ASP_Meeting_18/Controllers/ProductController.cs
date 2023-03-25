using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models;
using ASP_Meeting_18.Models.DTO;
using ASP_Meeting_18.Models.ViewModels.AccountViewModels;
using ASP_Meeting_18.Models.ViewModels.AdminViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP_Meeting_18.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly IMapper mapper;
        public ProductController(ShopDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
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

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var questRoom = await _context.Products.Include(c => c.Category).Include(c => c.Images).FirstOrDefaultAsync(m => m.Id == id);
            if (questRoom != null)
            {
                _context.Products.Remove(questRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        public async Task<JsonResult> GetChildCategories(int parentId)
        {

            var childCategories = await _context.Categories.Where(p => p.ParentCategoryId == parentId).Distinct().ToListAsync();
            return Json(childCategories);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(c => c.Category)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            EditProductViewModel vM = new EditProductViewModel
            {
                CategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId != null).Distinct().ToListAsync(), "Id", "Title"),
                ParentCategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync(), "Id", "Title"),
                Product = mapper.Map<ProductDTO>(product)
            };
            return View(vM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel vM)
        {
            if (!ModelState.IsValid)
            {
                vM.CategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId != null).Distinct().ToListAsync(), "Id", "Title");
                vM.ParentCategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync(), "Id", "Title");
                return View(vM);
            }
            try
            {

                if (vM.Image is not null)
                {
                    using (BinaryReader br = new BinaryReader(vM.Image!.OpenReadStream()))
                    {
                        vM.Product.Images!.Add(new Photo { Image = br.ReadBytes((int)vM.Image.Length) });
                    }
                }
                Product product = mapper.Map<Product>(vM.Product);
                _context.Update(product);
                if (vM.Image is not null)
                {
                    var product2 = await _context.Products
                    .Include(c => c.Category)
                    .Include(c => c.Images)
                    .FirstOrDefaultAsync(m => m.Id == vM.Product.Id);
                    product2!.Images!.Clear();
                    product2.Images = vM.Product.Images;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(vM.Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }


        public async Task<IActionResult> CreateProduct()
        {
            CreateProductViewModel vM = new CreateProductViewModel
            {
                //CategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId != null).Distinct().ToListAsync(), "Id", "Title"),
                ParentCategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync(), "Id", "Title")
            };
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel vM)
        {
            if (!ModelState.IsValid || vM.Image is null)
            {
                vM.CategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId != null).Distinct().ToListAsync(), "Id", "Title");
                vM.ParentCategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync(), "Id", "Title");
                return View(vM);
            }
            using (BinaryReader br = new BinaryReader(vM.Image!.OpenReadStream()))
            {
                vM.Product.Images!.Add(new Photo { Image = br.ReadBytes((int)vM.Image.Length) });
            }
            Product createdProduct = mapper.Map<Product>(vM.Product);
            //createdProduct.Category.ParentCategoryId = vM.ParentCategoryId;
            _context.Add(createdProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> CreateCategory()
        {
            CreateCategoryViewModel vM = new CreateCategoryViewModel
            {
                ParentCategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync(), "Id", "Title")
            };
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel vM)
        {
            if (!ModelState.IsValid)
            {
                vM.ParentCategorySL = new SelectList(await _context.Categories.Where(p => p.ParentCategoryId == null).Distinct().ToListAsync(), "Id", "Title");
                return View(vM);
            }
            if (vM.ParentCategoryId != 0)
            {
                vM.Category.ParentCategoryId = vM.ParentCategoryId;
            }
            Category createdCategory = mapper.Map<Category>(vM.Category);
            _context.Add(createdCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
    }
}
