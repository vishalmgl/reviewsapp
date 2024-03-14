

using reviewsapp.models;
namespace reviewsapp.Interfaces
{
    public interface IModelRepository
    {
        ICollection<Model> GetModels();
        Model GetModel(int Id);
        Model GetModel(string Name);
        decimal GetModelRating(int modId);
        bool ModelExists(int modId);
        bool Createmodel(int ownerId,int CategoryId,Model model);
        bool updatemodel(int ownerId,int CategoryId,Model model);
        bool deleteModel(Model model);
        bool Save(); 
    }
}
