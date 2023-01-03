using Microsoft.Extensions.Configuration;
using PetProject.Utilities.Exceptions;

namespace PetProject.Utilities.Extensions
{
    public static class ConfigurationExtension
    {
        public static string JwtAudience(this IConfiguration configuration)
        {
            var result = configuration["Jwt:Audience"];
            if (result == null)
            {
                throw new PetProjectException($"The configuration file could not be found value that key is Jwt:Audience");
            }
            return result;
        }

        public static string JwtIssuer(this IConfiguration configuration)
        {
            var result = configuration["Jwt:Issuer"];
            if (result == null)
            {
                throw new PetProjectException($"The configuration file could not be found value that key is Jwt:Issuer");
            }
            return result;
        }

        public static string JwtKey(this IConfiguration configuration)
        {
            var result = configuration["Jwt:Key"];
            if (result == null)
            {
                throw new PetProjectException($"The configuration file could not be found value that key is Jwt:Key");
            }
            return result;
        }

        public static string ConnectionDatabase(this IConfiguration configuration)
        {
            var result = configuration.GetConnectionString("SQLConnection");
            if (result == null)
            {
                throw new PetProjectException($"The configuration file could not be found value that key is SQLConnection");
            }
            return result;
        }
    }
}
