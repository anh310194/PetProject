using PetProject.Domain.Entities;

namespace PetProject.Domain.Interfaces;

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
}
