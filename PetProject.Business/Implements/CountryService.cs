using Microsoft.EntityFrameworkCore;
using PetProject.Business.Common;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.Domain.Interfaces;
using PetProject.Domain.Entities;
using PetProject.Utilities.Exceptions;
using PetProject.Utilities;

namespace PetProject.Business.Implements;

public class CountryService : BaseService, ICountryService
{
    public CountryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public void DeleteCountryById(long id)
    {
        _unitOfWork.CountryRepository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    public Task<List<CountryModel>> GetCountries()
    {
        return _unitOfWork.CountryRepository.Queryable().Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).ToListAsync();
    }

    public Task<CountryModel?> GetCountryById(long id)
    {
        return _unitOfWork.CountryRepository.Queryable(p => p.Id == id)
            .Select(s => new CountryModel() { Id = s.Id, CountryCode = s.CountryCode, CountryName = s.CountryName }).FirstOrDefaultAsync();
    }

    public async Task<CountryModel?> UpdateCountryById(string? userName, CountryModel model)
    {
        var country = await _unitOfWork.CountryRepository.FindAsync(model.Id);
        if (country == null)
        {
            throw new PetProjectApplicationException(string.Format(PetProjectMessage.NOT_FOUND_COUNTRY_ID, model.Id));
        }

        country.CountryCode = model.CountryCode;
        country.CountryName = model.CountryName;

        long userId = GetUserId(userName);
        var result = _unitOfWork.CountryRepository.Update(country, userId);

        await _unitOfWork.SaveChangesAsync();

        return MappingCountryModel(result);
    }
    private CountryModel? MappingCountryModel(Country? country)
    {
        if (country == null)
        {
            return null;
        }
        return new CountryModel()
        {
            Id = country.Id,
            CountryCode = country.CountryCode,
            CountryName = country.CountryName,
        };
    }

    public async Task<CountryModel?> InsertCountryById(string? userName, CountryModel model)
    {
        var countryCodeExists = await _unitOfWork.CountryRepository.GetByCountryCodeAsync(model.CountryCode);
        if (countryCodeExists != null)
        {
            throw new PetProjectApplicationException(string.Format(PetProjectMessage.COUNTRY_CODE_EXISTS, model.CountryCode));
        }
        var country = new Country()
        {
            CountryCode = model.CountryCode,
            CountryName = model.CountryName,
        };

        long userId = GetUserId(userName);
        var countryResult = await _unitOfWork.CountryRepository.InsertAsync(country, userId);
        await _unitOfWork.SaveChangesAsync();

        return MappingCountryModel(countryResult);
    }
}
