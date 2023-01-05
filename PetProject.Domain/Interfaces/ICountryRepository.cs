using PetProject.Domain.Entities;

namespace PetProject.Domain.Interfaces
{
    public interface ICountryRepository: IGenericRepository<Country>
    {
        Country GetByCountryCode(string? countryCode);
    }
}
