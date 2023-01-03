using Microsoft.EntityFrameworkCore;
using PetProject.Business.Common;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.Infacstructure.Interfaces;
using PetProject.Domain.Entities;
using PetProject.Utilities.Exceptions;

namespace PetProject.Business.Implements;

public class CountryService : BaseService, ICountryService
{
    private IGenericRepository<Country> countryRepository;
    public CountryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        countryRepository = _unitOfWork.GenericRepository<Country>();
    }

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

    public async Task<CountryModel?> UpdateCountryById(string? userName, CountryModel model)
    {
        var country = await countryRepository.FindAsync(model.Id);
        if (country == null)
        {
            throw new PetProjectException($"The Country {model.Id} could not be found");
        }

        country.CountryCode = model.CountryCode;
        country.CountryName = model.CountryName;

        long userId = GetUserId(userName);
        countryRepository.Update(country, userId);

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
        var result = await countryRepository.InsertAsync(country, userId);

        await _unitOfWork.SaveChangesAsync();

        return MappingCountryModel(result.Entity);
    }

}
