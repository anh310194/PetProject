using Microsoft.EntityFrameworkCore;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.Core.Interfaces;
using PetProject.Entities;

namespace PetProject.Business.Implements
{
    public class CountryService : ICountryService
    {

        private readonly IUnitOfWork _unitOfWork;
        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteCountryById(long id)
        {
            _unitOfWork.Delete<Country>(id);
        }

        public Task<List<CountryModel>> GetCountries()
        {
            return _unitOfWork.AsQuery<Country>().Select(s=> new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName}).ToListAsync();
        }

        public Task<CountryModel> GetCountryById(long id)
        {
            return _unitOfWork.AsQuery<Country>().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync(p=> p.Id == id);
        }

        public Task<CountryModel> UpsertCountryById(CountryModel model)
        {
            return _unitOfWork.AsQuery<Country>().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
        }
    }
}
