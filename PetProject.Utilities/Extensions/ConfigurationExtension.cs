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
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, "Jwt:Audience"));
            }
            return result;
        }

        public static string JwtIssuer(this IConfiguration configuration)
        {
            var result = configuration["Jwt:Issuer"];
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, "Jwt:Issuer"));
            }
            return result;
        }

        public static string JwtKey(this IConfiguration configuration)
        {
            var result = configuration["Jwt:Key"];
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, "Jwt:Key"));
            }
            return result;
        }

        public static string ConnectionDatabase(this IConfiguration configuration)
        {
            var result = configuration.GetConnectionString("SQLConnection");
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, "SQLConnection"));
            }
            return result;
        }

        public static double JwtExpiredTime(this IConfiguration configuration)
        {
            var expiredTime = configuration["Jwt:ExpiredTime"];
            double value = 0;
            if (double.TryParse(expiredTime, out value))
            {
                return value;
            }
            else
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, "Jwt:ExpiredTime"));
            }
        }
    }
}
