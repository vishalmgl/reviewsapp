using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using reviewsapp.Data;
using reviewsapp.Interfaces;
using reviewsapp.models;
using System.Diagnostics.Metrics;

namespace reviewsapp.Repository
{
    public class OwnerNameRepository : IOwnerNamesRepository
    {
        private readonly DataContext _context;

        public OwnerNameRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(OwnerName ownerName)
        {
            _context.Add(ownerName);
            return Save();
        }

        public bool DeleteOwner(OwnerName ownerName)
        {
            _context.Remove(ownerName); 
            return Save();
        }

        public ICollection<Model> GetModelByOwnerNames(int ownerId)
        {
            return _context.ModelOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Model).ToList();
        }

        public OwnerName GetOwner(int OwnerId)
        {
            return _context.OwnerName.Where(o => o.Id == OwnerId).FirstOrDefault();
 
        }

        public ICollection<OwnerName> GetOwnerNames()
        {
           return _context.OwnerName.ToList();
        }

        public ICollection<OwnerName> GetOwnerofAModel(int modId)
        {
            return _context.ModelOwners.Where(p  => p.Model.Id == modId).Select(o =>o.Owner).ToList();
        }

        public bool OwnersExist(int ownerId)
        {
            return _context.OwnerName.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwnerNames(OwnerName ownerName)
        {
            _context.Update(ownerName);
            return Save();
        }
    }
}
