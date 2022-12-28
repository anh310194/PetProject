﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Database;
using PetProject.Infacstructure.Interfaces;

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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
