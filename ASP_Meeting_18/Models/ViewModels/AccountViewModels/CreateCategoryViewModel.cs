using ASP_Meeting_18.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Meeting_18.Models.ViewModels.AccountViewModels
{
    public class CreateCategoryViewModel
    {
        public CategoryDTO Category { get; set; } = default!;

        public SelectList? ParentCategorySL { get; set; } = default!;

        public int ParentCategoryId { get; set; }
    }
}
