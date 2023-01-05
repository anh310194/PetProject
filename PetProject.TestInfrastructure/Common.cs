using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Context;
using PetProject.Infacstructure.Reposibilities;

namespace PetProject.TestInfrastructure
{
    public static class Common
    {
        private static UnitOfWork? _unitOfWorkInstance;
        public static IUnitOfWork UnitOfWorkInstance(PetProjectContext context)
        {
            if (_unitOfWorkInstance != null)
            {
                return _unitOfWorkInstance;
            }
            ICountryRepository countryRepository = new CountryRepository(context);
            IDateTimeFormatRepository dateTimeFormatRepository = new DateTimeFormatRepository(context);
            IFeatureRepository featureRepository = new FeatureRepository(context);
            IRoleFeatureRepository roleFeatureRepository = new RoleFeatureRepository(context);
            IRoleRepository roleRepository = new RoleRepository(context);
            ITimeZoneRepository timeZoneRepository = new TimeZoneRepository(context);
            IUserRepository userRepository = new UserRepository(context);
            IUserRoleRepository userRoleRepository = new UserRoleRepository(context);

            _unitOfWorkInstance = new UnitOfWork(
                context,
                countryRepository,
                dateTimeFormatRepository,
                featureRepository,
                roleFeatureRepository,
                roleRepository,
                timeZoneRepository,
                userRepository,
                userRoleRepository
            );

            return _unitOfWorkInstance;
        }
    }
}
