namespace PetProject.Interfaces.Repositories;

public interface IRepositoryFactory
{
    ICountryRepository CountryRepository { get; }
    IDateTimeFormatRepository DateTimeFormatRepository { get; }
    IFeatureRepository FeatureRepository { get; }
    IRoleFeatureRepository RoleFeatureRepository { get; }
    IRoleRepository RoleRepository { get; }
    ITimeZoneRepository TimeZoneRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IStoreProcedureRepository StoreProcedureRepository { get; }
}
