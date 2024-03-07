//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using reviewsapp.Data;
using reviewsapp.Interfaces;
using reviewsapp.models;

namespace reviewsapp.Repository
{
    public class ModelRepository : ImodelRepository
    {
        private readonly DataContext _context;

        public ModelRepository(DataContext context)
        {
            _context = context;
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
    }

}