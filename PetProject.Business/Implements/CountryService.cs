using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.Core;
using PetProject.Core.Interfaces;
using PetProject.Entities;

namespace PetProject.Business.Implements
{
    public class CountryService : BaseService, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork, ILogger<CountryService> logger) : base(unitOfWork, logger) { }

        public void DeleteCountryById(long id)
        {
            _unitOfWork.Delete<Country>(id);
        }

        public Task<List<CountryModel>> GetCountries()
        {
            return _unitOfWork.AsQuery<Country>().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).ToListAsync();
        }

        public Task<CountryModel?> GetCountryById(long id)
        {
            return _unitOfWork.AsQuery<Country>().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<CountryModel?> UpsertCountryById(CountryModel model)
        {
            return _unitOfWork.AsQuery<Country>().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
        }
    }
}
