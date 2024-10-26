using Microsoft.EntityFrameworkCore;
using Summit_Task.Context;
using Summit_Task.Dtos.Product;
using Summit_Task.HelperClasses;
using Summit_Task.HelperClasses.AutoMapperConfig;
using Summit_Task.Models;

namespace Summit_Task.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _Context;

        public ProductService(ApplicationContext context)
        {
            _Context = context;
        }

        public async Task<Product?> CreateProduct(ProductCreateDto productCreateDto)
        {
            try
            {
                var product = productCreateDto.MapTo<Product>();
                if (productCreateDto.ImageUrl != null)
                {
                    product.ImageUrl = await PhotoUploadHelper.SavePhotoAsync(productCreateDto.ImageUrl, "Products");
                }

                    await _Context.Products.AddAsync(product);
                    await _Context.SaveChangesAsync();

                return product;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
        }

        public async Task<List<GetProductDto>> GetAllProductWithFilter(int pageIndex, int PageSize, int? categoryID = null)
        {
            var query = _Context.Products.AsQueryable();

            if (categoryID.HasValue)
                query = query.Where(p => p.CategoryId == categoryID);


            var totalItems = await query.CountAsync();
            var products = await query.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToListAsync();

            var Result = products.MapTo<List<GetProductDto>>();

            return Result;
        }


    }
}
