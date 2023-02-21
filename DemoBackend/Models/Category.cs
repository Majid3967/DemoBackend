namespace DemoBackend.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public ICollection<Item> items { get; set; }
    }
}
