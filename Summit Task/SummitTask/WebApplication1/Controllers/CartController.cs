using Microsoft.AspNetCore.Mvc;
using Summit_Task.Controllers;
using Summit_Task.Dtos.Cart;
using Summit_Task.HelperClasses.AutoMapperConfig;
using Summit_Task.Models.Cart;
using Summit_Task.Service.CartService;


public class CartController : SummitController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var cart = await _cartService.GetCartAsync();
        var Results = cart.MapTo<CartDto>();

        Results.TotalPrice = Results.Items.Sum(e => e.TotalItemPrice);
        return Ok(Results);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        try
        {
            var cart = await _cartService.AddToCartAsync(productId, quantity);

            var result = cart.MapTo<CartDto>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCartItem(int productId, int quantity)
    {
        try
        {
            var cart = await _cartService.UpdateCartItemAsync(productId, quantity);
            var result = cart.MapTo<CartDto>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remove/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        try
        {
            var cart = await _cartService.RemoveFromCartAsync(productId);
            var result = cart.MapTo<CartDto>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
        await _cartService.ClearCartAsync();
        return NoContent();
    }
}
