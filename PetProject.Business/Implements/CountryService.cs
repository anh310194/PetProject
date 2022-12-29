using Microsoft.EntityFrameworkCore;
using PetProject.Business.Common;
using PetProject.Interfaces.Services;
using PetProject.Models;
using PetProject.Interfaces.Common;

namespace PetProject.Business.Implements
{
    public class CountryService :BaseService, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void DeleteCountryById(long id)
        {
            _unitOfWork.Country.Delete(id);
        }

        public Task<List<CountryModel>> GetCountries()
        {
            return _unitOfWork.Country.Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).ToListAsync();
        }

        public Task<CountryModel?> GetCountryById(long id)
        {
            return _unitOfWork.Country.Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<CountryModel?> UpsertCountryById(CountryModel model)
        {
            return _unitOfWork.Country.Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
        }
    }
}
