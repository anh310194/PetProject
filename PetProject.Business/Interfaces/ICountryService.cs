using PetProject.Business.Models;

namespace PetProject.Business.Interfaces;

public interface ICountryService
{
    Task<List<CountryModel>> GetCountries();
    Task<CountryModel?> GetCountryById(long id);
    Task<CountryModel?> UpdateCountryById(string? userName, CountryModel model); 
    Task<CountryModel?> InsertCountryById(string? userName, CountryModel model);
    void DeleteCountryById(long id);
}
