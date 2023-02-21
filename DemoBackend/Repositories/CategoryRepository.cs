using DemoBackend.Data;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBackend.Repositories
{
    public class CategoryRepository : ICategoryInterface
    {
        private DataContext _context;

        public CategoryRepository(DataContext context) { 
            _context = context;
        }
        public bool AddCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public ICollection<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c=>c.categoryId).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.categoryId == id).FirstOrDefault();
        }
        public int LastCategory()
        {
            Category? category = _context.Categories.OrderBy(u => u.categoryId).LastOrDefault();
            if (category == null)
                return 0;
            return category.categoryId;
        }
        public bool CategoryExist(string categoryName)
        {
            return _context.Categories.Any(c=>c.categoryName == categoryName);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CategoryExist(int categoryId)
        {
            return _context.Categories.Any(c => c.categoryId == categoryId);
        }
    }
}
