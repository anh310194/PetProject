using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Domain.Interfaces;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    internal class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(PetProjectContext context): base(context)
        {

        }
        public Country GetByCountryCode(string? countryCode)
        {
            return _dbSet.AsQueryable().FirstOrDefault(p => p.CountryCode == countryCode);
        }
    }
}
