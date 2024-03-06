//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using reviewsapp.Data;
using reviewsapp.models;

namespace reviewsapp.Repository
{
    public class ModelRepository
    {
        private readonly DataContext _context;

        public ModelRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Model> GetModels()
        {
            return _context.Models.OrderBy(p => p.Id).ToList();
        }
    }

}