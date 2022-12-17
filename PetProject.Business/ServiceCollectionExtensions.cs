using PetProject.Business.Interfaces;
using PetProject.Business.Implements;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Specification;

namespace PetProject.Business
{
    public static class ServiceCollectionExtensions
    {            
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddSpecification();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}