using PetProject.Business.Interfaces;
using PetProject.Business.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace PetProject.Business
{
    public static class ServiceCollectionExtensions
    {            
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
        }
    }
}