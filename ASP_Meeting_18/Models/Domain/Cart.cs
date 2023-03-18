using ASP_Meeting_18.Data;

namespace ASP_Meeting_18.Models.Domain
{
    public class Cart
    {
        List<CartItem> cartItems;
        public IEnumerable<CartItem> CartItems => cartItems;


        public Cart()
        {
            cartItems = new List<CartItem>();
        }
        public Cart(IEnumerable<CartItem> cartItems)
        {
            this.cartItems = cartItems.ToList();
        }
        public void AddToCart(Product product, int count)
        {
            CartItem? item = cartItems.FirstOrDefault(t => t.Product.Id == product.Id);
            if (item != null)
            {
                item.Count += count;
            }
            else
                cartItems.Add(new CartItem { Product = product, Count = count });
        }

        public void RemoveFromCart(Product product)
        {
            cartItems.RemoveAll(t => t.Product.Id == product.Id);
        }

        public void Clear()
        {
            cartItems.Clear();
        }

        public double TotalPrice => cartItems.Sum(t => t.Product.Price * t.Count);
    }
}
