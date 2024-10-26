using Microsoft.EntityFrameworkCore;
using Summit_Task.Dtos.Cart;
using Summit_Task.Models.Cart;

namespace Summit_Task.Service.CartService
{
    public interface ICartService
    {
        public Task<Cart> GetCartAsync();

        public Task<Cart> AddToCartAsync(int productId, int quantity);

        public Task<Cart> RemoveFromCartAsync(int productId);
        public Task<Cart> UpdateCartItemAsync(int productId, int quantity);

        public Task ClearCartAsync();
    }
}
