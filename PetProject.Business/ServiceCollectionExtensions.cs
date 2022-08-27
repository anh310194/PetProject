using PetProject.Business.Interfaces;
using PetProject.Business.Implements;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Core.Interfaces;
using PetProject.Core.Data;

namespace PetProject.Business
{
    public static class ServiceCollectionExtensions
    {            
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}