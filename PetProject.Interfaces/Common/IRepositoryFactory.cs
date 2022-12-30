using PetProject.Interfaces.Reponsitories;

namespace PetProject.Interfaces.Common
{
    public interface IRepositoryFactory
    {
        public IUserRepository UserRepository { get; }
        public IDateTimeFormatRepository DateTimeFormatRepository { get; }
        public IFeatureRepository FeatureRepository { get; }
        public IRoleFeatureRepository RoleFeatureRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ITimeZoneRepository TimeZoneRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public ICountryRepository CountryRepository { get; }
    }
}
