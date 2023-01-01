using PetProject.Business.Models;

namespace PetProject.Business.Interfaces;

public interface ICountryService
{
    Task<List<CountryModel>> GetCountries();
    Task<CountryModel?> GetCountryById(long id);
    Task<CountryModel?> UpsertCountryById(CountryModel model);
    void DeleteCountryById(long id);
}
