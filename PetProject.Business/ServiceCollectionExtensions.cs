using PetProject.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Interfaces.Business;

namespace PetProject.Business;

public static class ServiceCollectionExtensions
{            
    public static void AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IUserService, UserService>();
    }
}