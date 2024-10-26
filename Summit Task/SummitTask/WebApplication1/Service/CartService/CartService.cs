using Microsoft.EntityFrameworkCore;
using Summit_Task.Context;
using Summit_Task.Dtos.Cart;
using Summit_Task.HelperClasses.AutoMapperConfig;
using Summit_Task.Models.Cart;

namespace Summit_Task.Service.CartService
{
    public class CartService : ICartService
    {
        private readonly ApplicationContext _context;

        public CartService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartAsync()
        {
            var cart = await _context.Cart
                .Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart();
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
            }
            return cart;
        }


        public async Task<Cart> AddToCartAsync(int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            var oCart = await _context.Cart.Include(c => c.Items)
                                            .ThenInclude(i => i.Product)
                                            .FirstOrDefaultAsync();

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception("Product not found");
            

            if (oCart == null)
            {
                oCart = new Cart();
                _context.Cart.Add(oCart);
            }

            var cartItem = oCart.Items?.FirstOrDefault(e => e.ProductId == productId);

            if (cartItem != null)
            {
                // Update the quantity of the existing cart item
                cartItem.Quantity += quantity;
            }
            else
            {
                // Add a new cart item if it doesn't already exist
                oCart.Items.Add(new CartItems { ProductId = productId, Quantity = quantity });
            }

            await _context.SaveChangesAsync();

            return oCart;
        }

        public async Task<Cart> UpdateCartItemAsync(int productId, int quantity)
        {
            var cart = await GetCartAsync();
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
                throw new Exception("Product not found in cart");

            cartItem.Quantity = quantity;

            await _context.SaveChangesAsync();
            return cart;
        }

        //// Remove item from the cart
        public async Task<Cart> RemoveFromCartAsync(int productId)
        {
            var cart = await GetCartAsync();
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
                throw new Exception("Product not found in cart");

            cart.Items.Remove(cartItem);

            await _context.SaveChangesAsync();
            return cart;
        }


        //// Clear the cart
        public async Task ClearCartAsync()
        {
            var cart = await GetCartAsync();
            cart.Items.Clear();
            await _context.SaveChangesAsync();
        }

    }
}
