using Microsoft.Extensions.DependencyInjection;
using PetProject.Specification.Common;
using PetProject.Specification.Interfaces;

namespace PetProject.Specification
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSpecification(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
