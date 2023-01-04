using Microsoft.EntityFrameworkCore;
using PetProject.Business.Common;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.Infacstructure.Interfaces;
using PetProject.Domain.Entities;
using PetProject.Utilities.Exceptions;
using PetProject.Domain;

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
            throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_COUNTRY_ID, model.Id));
        }

        country.CountryCode = model.CountryCode;
        country.CountryName = model.CountryName;

        long userId = GetUserId(userName);
        _unitOfWork.CountryRepository.Update(country, userId);

        await _unitOfWork.SaveChangesAsync();

        return MappingCountryModel(country);
    }
    private CountryModel MappingCountryModel(Country country)
    {
        return new CountryModel()
        {
            Id = country.Id,
            CountryCode = country.CountryCode,
            CountryName = country.CountryName,
        };
    }

    public async Task<CountryModel?> InsertCountryById(string? userName, CountryModel model)
    {
        var country = new Country()
        {
            Id = model.Id,
            CountryCode = model.CountryCode,
            CountryName = model.CountryName,
        };

        long userId = GetUserId(userName);
        var result = await _unitOfWork.CountryRepository.InsertAsync(country, userId);

        await _unitOfWork.SaveChangesAsync();

        return MappingCountryModel(result.Entity);
    }
}
