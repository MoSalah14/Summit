using Summit_Task.Dtos.Product;

namespace Summit_Task.Dtos.Cart
{
    public class CartItemsDto
    {
        public int Quantity { get; set; }

        public GetProductDto Product { get; set; }
        public decimal? TotalItemPrice
        {
            get
            {
                return Quantity * (Product?.Price ?? 0); // Use null Check for Safety
            }
        }
    }
}