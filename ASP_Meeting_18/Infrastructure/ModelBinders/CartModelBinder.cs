using ASP_Meeting_18.Extensions;
using ASP_Meeting_18.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP_Meeting_18.Infrastructure.ModelBinders
{
    public class CartModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException();
            string sessionKey = "cart";
            IEnumerable<CartItem>? cartItems = null;
            if (bindingContext.HttpContext.Session != null)
            {
                cartItems = bindingContext.HttpContext
                .Session.Get<IEnumerable<CartItem>>(sessionKey);
            }
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                bindingContext.HttpContext.Session!.Set(sessionKey, cartItems!);
            }
            Cart cart = new Cart(cartItems);
            bindingContext.Result = ModelBindingResult.Success(cart);
            return Task.CompletedTask;
        }
    }
}
