using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Infacstructure.Context;
using PetProject.Infacstructure.Interfaces;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure; 

public static class ServiceCollectionExtensions
{
    public static void AddInfacstructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PetProjectContext>(options =>
            options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure())
        );
        services.AddScoped<IDataContext, PetProjectContext>();

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
