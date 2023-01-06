using Microsoft.Extensions.Configuration;
using PetProject.Utilities.Constants;
using PetProject.Utilities.Exceptions;

namespace PetProject.Utilities.Extensions
{
    public static class ConfigurationExtension
    {
        public static string JwtAudience(this IConfiguration configuration)
        {
            var result = configuration[ConfigurationConst.JWT_AUDIENCE];
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, ConfigurationConst.JWT_AUDIENCE));
            }
            return result;
        }

        public static string JwtIssuer(this IConfiguration configuration)
        {
            var result = configuration[ConfigurationConst.JWT_ISSUER];
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, ConfigurationConst.JWT_ISSUER));
            }
            return result;
        }

        public static string JwtKey(this IConfiguration configuration)
        {
            var result = configuration[ConfigurationConst.JWT_KEY];
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, ConfigurationConst.JWT_KEY));
            }
            return result;
        }

        public static string ConnectionDatabase(this IConfiguration configuration)
        {
            var result = configuration.GetConnectionString(ConfigurationConst.DATABASE_CONNECTION_STRING);
            if (result == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, ConfigurationConst.DATABASE_CONNECTION_STRING));
            }
            return result;
        }

        public static double JwtExpiredTime(this IConfiguration configuration)
        {
            var expiredTime = configuration[ConfigurationConst.JWT_EXPIREDTIME];
            double value = 0;
            if (double.TryParse(expiredTime, out value))
            {
                return value;
            }
            else
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_KEY_CONFIGURATION, ConfigurationConst.JWT_EXPIREDTIME));
            }
        }
    }
}
