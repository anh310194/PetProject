using PetProject.Domain.Entities;
using PetProject.Models;

namespace PetProject.TestBusiness.MockData;
public static class MockCountry
{
    private static Country? _country;
    public static Country GetCountry()
    {
        if (_country != null)
            return _country;

        _country = new Country
        {
            CountryCode = "US",
            CountryName = "United States",
            Id = 1,
            CreatedBy = 1,
            CreatedTime = DateTime.UtcNow
        };
        return _country;
    }

    private static Country? _countryExpected;
    public static Country GetCountryExpected()
    {
        if (_countryExpected != null)
            return _countryExpected;

        _countryExpected = new Country() { CountryCode = "Test_CountryCode", CountryName = "Test_CountryName", Id = 2 };
        return _countryExpected;
    }


    private static CountryModel? _countryModel;
    public static CountryModel GetCountryModel()
    {
        if (_countryModel != null)
            return _countryModel;

        _countryModel = new CountryModel()
        {
            Id = 1,
            CountryCode = "Test_CountryCode",
            CountryName = "Test_CountryName",
        };
        return _countryModel;
    }
}
