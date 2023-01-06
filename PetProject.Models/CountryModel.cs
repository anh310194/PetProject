
namespace PetProject.Models;

public class CountryModel
{
    private long _id;
    private string? _countryCode;
    private string? _countryName;

    public long Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string? CountryCode
    {
        get { return _countryCode; }
        set { _countryCode = value; }
    }

    public string? CountryName
    {
        get { return _countryName; }
        set { _countryName = value; }
    }
}
