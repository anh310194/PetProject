﻿using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Infacstructure.Reposibilities;
using PetProject.Interfaces.Repositories;

namespace PetProject.TestInfrastructure
{
    public static class Common
    {
        private static PetProjectContext _context;
        private static UnitOfWork? _unitOfWorkInstance;
        public static void ClearTracked()
        {
            _context.ChangeTracker.Clear();
        }
        public static IUnitOfWork UnitOfWorkInstance(PetProjectContext context)
        {
            _context = context;
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
            IStoreProcedureRepository storeProcedureRepository = new StoreProcedureRepository(context);

            _unitOfWorkInstance = new UnitOfWork(
                context,
                countryRepository,
                dateTimeFormatRepository,
                featureRepository,
                roleFeatureRepository,
                roleRepository,
                timeZoneRepository,
                userRepository,
                userRoleRepository,
                storeProcedureRepository
            );

            return _unitOfWorkInstance;
        }
    }
}
