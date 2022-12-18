﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Infacstructure.Database;
using PetProject.Shared.Interfaces;

namespace PetProject.Infacstructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfacstructure(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<PetProjectContext>(options =>
                options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure())
            );
            services.AddScoped<IDataContext, PetProjectContext>();
        }
    }
}
