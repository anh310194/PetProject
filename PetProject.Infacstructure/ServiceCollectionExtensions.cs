using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Core.Data;
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
                    configuration.GetConnectionString("SQLConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddScoped<IDbContext, PetProjectContext>();
        }
    }
}
