using Microsoft.EntityFrameworkCore;

using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using PetProject.Models;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(PetProjectContext context) : base(context)
        {

        }
        public Task<Country?> GetByCountryCodeAsync(string? countryCode)
        {
            return _dbSet.AsQueryable().FirstOrDefaultAsync(p => p.CountryCode == countryCode);
        }
        public async Task<ICollection<CountryModel>> GetCountryModels()
        {
            var result = await Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).ToListAsync();
            return result;
        }

        public Task<CountryModel?> GetCountryModelById(long id)
        {
            return Queryable(p => p.Id == id).Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
        }

    }
}
