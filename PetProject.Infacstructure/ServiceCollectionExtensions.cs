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

        services.AddTransient<ICountryRepository, CountryRepository>();
        services.AddTransient<IDateTimeFormatRepository, DateTimeFormatRepository>();
        services.AddTransient<IFeatureRepository, FeatureRepository>();
        services.AddTransient<IRoleFeatureRepository, RoleFeatureRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<ITimeZoneRepository, TimeZoneRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserRoleRepository, UserRoleRepository>();
        services.AddTransient<IStoreProcedureRepository, StoreProcedureRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

    }
}
