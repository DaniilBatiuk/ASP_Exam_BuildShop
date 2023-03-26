using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;

namespace ASP_Meeting_18.Models.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; } = default!;

        public string? Category { get; set; }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public string? Search { get; set; }
    }
}
