using AutoMapper;
using Summit_Task.Dtos.Cart;
using Summit_Task.Dtos.Category;
using Summit_Task.Dtos.Product;
using Summit_Task.Models;
using Summit_Task.Models.Cart;

namespace Summit_Task.HelperClasses.AutoMapperConfig
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //CreateMap<Product, GetProductDto>().ReverseMap();

            //CreateMap<ProductCreateDto, Product>();
            //// .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.OtherProperty));

            //CreateMap<Category, CategoryDto>().ReverseMap();

            //CreateMap<Cart, CartDto>()
            //    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Items.Sum(item => item.Quantity * item.Product.Price)))
            //    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            //    .ReverseMap();


            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Category, CategoryDto>().ReverseMap();

            // Map Cart to CartDto
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Items.Sum(item => item.Quantity * (item.Product != null ? item.Product.Price : 0m))))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            // Map CartItems to CartItemsDto
            CreateMap<CartItems, CartItemsDto>()
                .ForMember(dest => dest.TotalItemPrice, opt => opt.MapFrom(src => src.Quantity * (src.Product != null ? src.Product.Price : 0m)))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();








        }
    }
}
