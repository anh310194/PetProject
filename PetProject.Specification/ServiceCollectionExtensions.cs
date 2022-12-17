using Microsoft.Extensions.DependencyInjection;
using PetProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Specification
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSpecification(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, PetProjectUnitOfWork>();
        }
    }
}
