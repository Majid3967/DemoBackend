namespace DemoBackend.Dtos
{
    public class ItemDto
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string price { get; set; }
        public int categoryId { get; set; }
    }
}
