using ASP_Meeting_18.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Meeting_18.Models.ViewModels.AdminViewModel
{
    public class EditProductViewModel
    {
        public ProductDTO Product { get; set; } = default!;
        public IFormFile? Image { get; set; } = default!;
        public SelectList? ParentCategorySL { get; set; } = default!;
        public SelectList? CategorySL { get; set; } = default!;
        public int ParentCategoryId { get; set; }
    }
}
