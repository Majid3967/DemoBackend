using DemoBackend.Data;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBackend.Repositories
{
    public class ItemRepository : IItemInterface
    {
        private DataContext _context;
        public ItemRepository(DataContext context) { 
            _context = context;
        }
        public bool AddItem(Item item)
        {
            _context.Items.Add(item);
            return Save();
        }

        public ICollection<Item> GetAllItems()
        {
            return _context.Items.OrderBy(i=>i.ItemId).ToList();
        }

        public Item GetItemId(int id)
        {
            return _context.Items.Where(i=>i.ItemId==id).FirstOrDefault();
        }

        public bool ItemExist(string itemName)
        {
            return _context.Items.Any(i=>i.ItemName == itemName);
        }

        public bool ItemExist(int itemId)
        {
            return _context.Items.Any(i => i.ItemId == itemId);
        }

        public int LastItem()
        {
            Item? item = _context.Items.OrderBy(u => u.ItemId).LastOrDefault();
            if (item == null)
                return 0;
            return item.ItemId;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
