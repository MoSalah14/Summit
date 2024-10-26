namespace Summit_Task.Dtos.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<CartItemsDto> Items { get; set; }
    }
}
