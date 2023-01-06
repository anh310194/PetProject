using PetProject.Domain.Common;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;

namespace PetProject.Repositories.Common;

public abstract class RepositoryFactory : IRepositoryFactory
{
    public RepositoryFactory(
        ICountryRepository countryRepository,
        IDateTimeFormatRepository dateTimeFormatRepository,
        IFeatureRepository featureRepository,
        IRoleFeatureRepository roleFeatureRepository,
        IRoleRepository roleRepository,
        ITimeZoneRepository timeZoneRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IStoreProcedureRepository storeProcedureRepository
        )
    {
        CountryRepository = countryRepository;
        DateTimeFormatRepository = dateTimeFormatRepository;
        FeatureRepository = featureRepository;
        RoleFeatureRepository = roleFeatureRepository;
        RoleRepository = roleRepository;
        TimeZoneRepository = timeZoneRepository;
        UserRepository = userRepository;
        UserRoleRepository = userRoleRepository;
        StoreProcedureRepository = storeProcedureRepository;
    }

    public ICountryRepository CountryRepository { get; }

    public IDateTimeFormatRepository DateTimeFormatRepository { get; }

    public IFeatureRepository FeatureRepository { get; }

    public IRoleFeatureRepository RoleFeatureRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public ITimeZoneRepository TimeZoneRepository { get; }

    public IUserRepository UserRepository { get; }

    public IUserRoleRepository UserRoleRepository { get; }

    public IStoreProcedureRepository StoreProcedureRepository { get; }
}
