using PetProject.Domain.Entities;

namespace PetProject.Domain.Interfaces
{
    public interface ICountryRepository: IGenericRepository<Country>
    {
        Task<Country?> GetByCountryCodeAsync(string? countryCode);
    }
}
