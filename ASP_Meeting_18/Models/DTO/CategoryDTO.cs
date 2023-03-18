using ASP_Meeting_18.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = default!;
        [ForeignKey(nameof(ParentCategory))]
        [Display(Name = "ParentCategory")]
        public int? ParentCategoryId { get; set; }

        public CategoryDTO? ParentCategory { get; set; }

        public List<CategoryDTO>? ChildCategoies { get; set; }

        public List<ProductDTO>? Products { get; set; }
    }
}
