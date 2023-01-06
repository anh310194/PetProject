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
        public Task<List<CountryModel>> GetCountryModels()
        {
            return Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).ToListAsync();
        }

        public Task<CountryModel?> GetCountryModelById(long id)
        {
            return Queryable(p => p.Id == id).Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
        }

    }
}
