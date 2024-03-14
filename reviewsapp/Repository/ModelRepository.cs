//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using reviewsapp.Data;
using reviewsapp.Interfaces;
using reviewsapp.models;

namespace reviewsapp.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly DataContext _context;

        public ModelRepository(DataContext context)
        {
            _context = context;
        }

        public bool Createmodel(int ownerId, int CategoryId, Model model)
        {
            var modelOwnerEntity=_context.OwnerName.Where(a=>a.Id==ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == CategoryId).FirstOrDefault();
            var modelOwner = new ModelOwner()
            {
                Owner = modelOwnerEntity,
                Model = model

            };
            _context.Add(modelOwner);
            var modelCategory = new ModelCategory()
            {
                Category = category,
                Model = model,

            };
            _context.Add(modelCategory);
            _context.Add(model);
            return Save();
        }

        public bool deleteModel(Model model)
        {
            _context.Remove(model);
            return Save();  
        }

        public Model GetModel(int Id)
        {
            return _context.Models.Where(p => p.Id == Id).FirstOrDefault();
        }

        public Model GetModel(string Name)
        {
            return _context.Models.Where(p => p.Name == Name).FirstOrDefault();
        }

        public decimal GetModelRating(int modId)
        {
            var review = _context.Reviews.Where(p => p.Model.Id == modId).FirstOrDefault();
            if(review.Count() <= 0)
            return 0;
            return ((decimal)review.Sum(r =>r.Rating) / review.Count());
        }

        public ICollection<Model> GetModels()
        {
            return _context.Models.OrderBy(p => p.Id).ToList();
        }

        public bool ModelExists(int modId)
        {
            return _context.Models.Any(p => p.Id == modId);
        }

        public bool Save()
        {
            var saved =_context.SaveChanges();
            return saved >0 ? true : false;
        }

        public bool updatemodel(int ownerId, int CategoryId, Model model)
        {
            _context.Update(model);
            return Save();
        }
    }

}