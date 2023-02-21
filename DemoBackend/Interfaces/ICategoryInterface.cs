using DemoBackend.Models;

namespace DemoBackend.Interfaces
{
    public interface ICategoryInterface
    {
        public ICollection<Category> GetAllCategories();
        public Category GetCategory(int id);
        public bool AddCategory(Category category);
        public int LastCategory();
        public bool CategoryExist(string categoryName);
        public bool CategoryExist(int categoryId);
        public bool Save();
    }
}
