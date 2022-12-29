using Microsoft.Extensions.DependencyInjection;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;
using PetProject.Repositories.Entities;

namespace PetProject.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IDateTimeFormatRepository, DateTimeFormatRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();
            services.AddTransient<IRoleFeatureRepository, RoleFeatureRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ITimeZoneRepository, TimeZoneRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
