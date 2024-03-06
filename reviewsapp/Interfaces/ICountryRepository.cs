using reviewsapp.models;

namespace reviewsapp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country>GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int OnwerId);
        ICollection<OwnerName>GetOwnerfromACountry(int countryId);
        bool CountryExists(int id); 
    }
}
