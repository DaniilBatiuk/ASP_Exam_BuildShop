using ASP_Meeting_18.Infrastructure.ModelBinders;
using ASP_Meeting_18.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP_Meeting_18.Infrastructure.ModelBinderProviders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Cart) ? new CartModelBinder()
                : null;
        }
    }
}
