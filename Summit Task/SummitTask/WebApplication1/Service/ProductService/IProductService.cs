using Summit_Task.Dtos.Product;
using Summit_Task.Models;

namespace Summit_Task.Service.ProductService
{
    public interface IProductService
    {
        Task<List<GetProductDto>> GetAllProductWithFilter(int pageSize, int PageNumber, int? categoryID = null);


        Task<Product?> CreateProduct(ProductCreateDto productCreateDto);

    }
}
