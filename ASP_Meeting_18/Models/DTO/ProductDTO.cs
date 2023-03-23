using ASP_Meeting_18.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ASP_Meeting_18.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = default!;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = default!;
        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Count")]
        public int Count { get; set; }
        [ForeignKey(nameof(CategoryDTO))]
        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }
        [Display(Name = "Image")]
        public List<Photo>? Images { get; set; } = new List<Photo>();
    }
}
