using ASP_Meeting_18.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.ViewModels.AccountViewModels
{
    public class IndexShopViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; } = default!;

        public SelectList CategorySL { get; set; } = default!;

        public SelectList ParentCategorySL { get; set; } = default!;
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "ParentCategory")]
        public int ParentCategoryId { get; set; }
    }
}
