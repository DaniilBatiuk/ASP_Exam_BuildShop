using ASP_Meeting_18.Data;

namespace ASP_Meeting_18.Models.Domain
{
    public class CartItem
    {
        public Product Product { get; set; } = default!;

        public int Count { get; set; }
    }
}
