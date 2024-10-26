using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Summit_Task.Context;
using Summit_Task.Dtos.Category;
using Summit_Task.Dtos.Product;
using Summit_Task.HelperClasses;
using Summit_Task.HelperClasses.AutoMapperConfig;
using Summit_Task.Models;
using Summit_Task.Service.ProductService;



namespace Summit_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext context;

        public CategoryController(ApplicationContext _context)
        {
            context = _context;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var AllCategory = await context.Category.ToListAsync();

            var Result = AllCategory.MapTo<List<CategoryDto>>();


            if (Result is null)
                return BadRequest(new SummitResponse<string>
                {
                    Data = null,// Just To Read The Value Not Must Write it
                    IsError = true,
                    Message = " Sorrry Faield Retrive Data"
                });


            return Ok(new SummitResponse<List<CategoryDto>>
            {
                Data = Result,
                IsError = false,
                Message = "Success",
            });
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct(CategoryDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            try
            {

                var oCategory = productDto.MapTo<Category>();

                var Result = await context.Category.AddAsync(oCategory);
                var x = await context.SaveChangesAsync();
                if (Result is null)
                {
                    return BadRequest(new SummitResponse<string>
                    {
                        Data = null,// Just To Read The Value Not Must Write it
                        IsError = true,
                        Message = " Sorry Failed Create Product"
                    });
                }


                return Ok(new SummitResponse<int>
                {
                    Data = oCategory.Id,
                    IsError = false,
                    Message = "Created Succefully"
                });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
