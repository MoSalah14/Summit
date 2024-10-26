using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Summit_Task.Context;
using Summit_Task.Dtos.Product;
using Summit_Task.HelperClasses;
using Summit_Task.Models;
using Summit_Task.Service.ProductService;

namespace Summit_Task.Controllers
{
    public class ProductsController : SummitController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int pageIndex = 1, int pageSize = 10, int? categoryId = null)
        {
            var Result = await productService.GetAllProductWithFilter(pageIndex, pageSize, categoryId);

            if (Result is null)
                return BadRequest(new PaginationRespons<string>
                {
                    Data = null ,// Just To Read The Value Not Must Write it
                    IsError = true,
                    TotalCount = 0,
                    Message = " Sorrry Faield Retrive Data"
                });


            return Ok(new PaginationRespons<List<GetProductDto>>
            {
                Data = Result,
                IsError = false,
                pageIndex = pageIndex,
                pageSize = pageSize,
                TotalCount = Result.Count,
                Message = "Success"
            });
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct([FromForm] ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            try
            {
                var Result = await productService.CreateProduct(productDto);
                if (Result is null)
                {
                    return BadRequest(new PaginationRespons<string>
                    {
                        Data = null,// Just To Read The Value Not Must Write it
                        IsError = true,
                        TotalCount = 0,
                        Message = " Sorry Faield Create Product"
                    });
                }


                return Ok(Result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }



    }
}
