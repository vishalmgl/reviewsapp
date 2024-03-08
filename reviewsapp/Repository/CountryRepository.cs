using AutoMapper;
using reviewsapp.Data;
using reviewsapp.Interfaces;
using reviewsapp.models;

namespace reviewsapp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context,IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        

        public bool CountryExists(int id)
        {
            return _context.Country.Any(c =>c.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Country.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Country.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int OwnerId)
        {
            return _context.OwnerName.Where(o => o.Id == OwnerId).Select(c => c.Country).FirstOrDefault();

        }

        public ICollection<OwnerName> GetOwnerfromACountry(int countryId)
        {
            return _context.OwnerName.Where(c => c.Country.Id == countryId).ToList();
        }
    }
}
