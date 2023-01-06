using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Infacstructure.Context;
using PetProject.Infacstructure.Reposibilities;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure; 

public static class ServiceCollectionExtensions
{
    public static void AddInfacstructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PetProjectContext>(options =>
            options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure())
        );

        services.AddTransient(typeof(ICountryRepository), typeof(CountryRepository));
        services.AddTransient(typeof(IDateTimeFormatRepository), typeof(DateTimeFormatRepository));
        services.AddTransient(typeof(IFeatureRepository), typeof(FeatureRepository));
        services.AddTransient(typeof(IRoleFeatureRepository), typeof(RoleFeatureRepository));
        services.AddTransient(typeof(IRoleRepository), typeof(RoleRepository));
        services.AddTransient(typeof(ITimeZoneRepository), typeof(TimeZoneRepository));
        services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
        services.AddTransient(typeof(IUserRoleRepository), typeof(UserRoleRepository));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IStoreProcedureRepository, StoreProcedureRepository>();
    }
}
