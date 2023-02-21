namespace DemoBackend.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserEmail { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
