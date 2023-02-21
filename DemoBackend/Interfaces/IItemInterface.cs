using DemoBackend.Models;

namespace DemoBackend.Interfaces
{
    public interface IItemInterface
    {
        public ICollection<Item> GetAllItems();
        public Item GetItemId(int id);
        public bool AddItem(Item item);
        public bool ItemExist(string itemName);
        public bool ItemExist(int itemId);
        public int LastItem();
        public bool Save();
    }
}
