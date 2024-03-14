using reviewsapp.models;

namespace reviewsapp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Model> GetModelsByCategory(int catagoryId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);//creating method signature
        bool DeleteCategory(Category category);
        bool Save();

    }
}
