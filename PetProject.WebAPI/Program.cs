using NLog;
using NLog.Web;
using PetProject.Business;
using PetProject.Infacstructure;
using PetProject.WebAPI.Filters;
using PetProject.Utilities.Extensions;
using PetProject.WebAPI.Extensions;
using PetProject.WebAPI.Services;
using PetProject.WebAPI.Interfaces;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    logger.Debug("init main");

    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddCors();
    // Add services to the container.
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    });

    //Add Authentication and Authorization Service
    builder.Services.AddAuthenticationPetProject(builder.Configuration);
    builder.Services.AddAuthorizationPetProject();
    builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerPetProject();

    //Add Petproject Services
    builder.Services.AddInfacstructure(builder.Configuration.ConnectionDatabase());
    builder.Services.AddBusiness();
    builder.Services.AddHttpContextAccessor();


    // Config services in the container.
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/error-development");
    }

    // Configure the HTTP request pipeline.
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}