using PetProject.Models;

namespace PetProject.Interfaces.Business;

public interface ICountryService
{
    Task<ICollection<CountryModel>> GetCountries();
    Task<CountryModel?> GetCountryById(long id);
    Task<CountryModel?> UpdateCountryById(string? userName, CountryModel model); 
    Task<CountryModel?> InsertCountryById(string? userName, CountryModel model);
    void DeleteCountryById(long id);
}
