using reviewsapp.Data;
using reviewsapp.Interfaces;
using reviewsapp.models;

namespace reviewsapp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;//initializing context feild if type datacontext
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            _context.SaveChanges(); 
             return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Model> GetModelsByCategory(int catagoryId)
        {
            return _context.ModelCategories.Where(e => e.categoryId == catagoryId).Select(c => c.Model).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
