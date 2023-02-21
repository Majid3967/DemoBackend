namespace DemoBackend.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
