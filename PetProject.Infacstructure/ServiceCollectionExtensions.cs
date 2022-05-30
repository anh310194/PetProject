using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Core.Interfaces;
using PetProject.Infacstructure.Database;

namespace PetProject.Infacstructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfacstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PetProjectContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("PetProjectConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddScoped<IUnitOfWork, PetProjectUnitOfWork>();
        }
    }
}
