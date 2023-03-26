using ASP_Meeting_18.Data;
using ASP_Meeting_18.Extensions;
using ASP_Meeting_18.Models.Domain;
using ASP_Meeting_18.Models.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Meeting_18.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext context;

        public CartController(ShopDbContext context)
        {
            this.context = context;
        }
        //public IActionResult Index(string? returnUrl)
        public IActionResult Index(Cart cart, string? returnUrl)
        {
            //Cart cart = GetCart();
            if (returnUrl == null)
            {
                returnUrl = Url.Action("Index", "Home");
            }
            CartIndexViewModel vm = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddToCart(int id, string? returnUrl)
        public async Task<IActionResult> AddToCart(Cart cart, int id,int quantity, string? returnUrl)
        {
            //Cart cart = GetCart();
            Product? product = await context.Products.FindAsync(id);
            if (product != null)
            {
                cart.AddToCart(product, quantity);
                HttpContext.Session.Set("cart", cart.CartItems);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteFromCart(int id, string? returnUrl)
        public async Task<IActionResult> DeleteFromCart(Cart cart, int id, string? returnUrl)
        {
            //Cart cart = GetCart();
            Product? product = await context.Products.FindAsync(id);
            if (product != null)
            {
                cart.RemoveFromCart(product);
                HttpContext.Session.Set("cart", cart.CartItems);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {

            IEnumerable<CartItem>? cartItems = HttpContext
                .Session.Get<IEnumerable<CartItem>>("cart");
            Cart? cart = null;
            if (cartItems == null)
            {
                cart = new Cart();
            }
            else cart = new Cart(cartItems!);
            return cart;
        }
    }
}
