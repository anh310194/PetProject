using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Domain.Interfaces;
using PetProject.Repositories.Common;

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
    }
}
