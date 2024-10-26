using Summit_Task.Service.CartService;
using Summit_Task.Service.ProductService;

namespace Summit_Task.Exetenstion
{
    public static class ServiceExtension
    {
        public static void SummitServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
        }
    }
}
