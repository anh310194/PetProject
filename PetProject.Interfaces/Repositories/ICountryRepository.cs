using PetProject.Domain.Entities;
using PetProject.Models;

namespace PetProject.Interfaces.Repositories;

public interface ICountryRepository: IGenericRepository<Country>
{
    Task<Country?> GetByCountryCodeAsync(string? countryCode);
    Task<ICollection<CountryModel>> GetCountryModels();
    Task<CountryModel?> GetCountryModelById(long id);
}
