// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetProject.Business;
using PetProject.Business.Interfaces;
using PetProject.Infacstructure;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        AddServices(context, services);
    }).Build();

var countrySerice = host.Services.GetRequiredService<ICountryService>();
var countries = await countrySerice.GetCountries();

// Application code should start here.
await host.RunAsync();

static void AddServices(HostBuilderContext context, IServiceCollection services)
{
    IConfiguration configuration = context.Configuration;
    services.AddInfacstructure(configuration);
    services.AddBusiness();
}