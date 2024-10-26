namespace Summit_Task.Models.Cart
{
    public class Cart : BaseEntity
    {
        public ICollection<CartItems> Items { get; set; } = new List<CartItems>();
    }
}
