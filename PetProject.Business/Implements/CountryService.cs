using Microsoft.EntityFrameworkCore;
using PetProject.Business.Common;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.Domain.Interfaces;
using PetProject.Domain.Entities;

namespace PetProject.Business.Implements
{
    public class CountryService :BaseService, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void DeleteCountryById(long id)
        {
            _unitOfWork.GenericRepository<Country>().Delete(id);
        }

        public Task<List<CountryModel>> GetCountries()
        {
            return _unitOfWork.GenericRepository<Country>().Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).ToListAsync();
        }

        public Task<CountryModel?> GetCountryById(long id)
        {
            return _unitOfWork.GenericRepository<Country>().Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<CountryModel?> UpsertCountryById(CountryModel model)
        {
            return _unitOfWork.GenericRepository<Country>().Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
        }
    }
}
