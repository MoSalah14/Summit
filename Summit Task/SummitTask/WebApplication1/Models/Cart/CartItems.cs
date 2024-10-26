namespace Summit_Task.Models.Cart
{
    public class CartItems : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
