using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PetProject.Utilities.Helper
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Generate a 128-bit salt using a sequence of
        /// cryptographically strong random bytes.
        /// </summary>
        /// <returns></returns>
        public static string CreateSaltPassword()
        {
            byte[] byteSalt = RandomNumberGenerator.GetBytes(128 / 8);// divide by 8 to convert bits to bytes
            return Convert.ToBase64String(byteSalt);
        }

        public static string GenerateSlug(int length = 9)
        {
            return shortid.ShortId.Generate(new shortid.Configuration.GenerationOptions(true, false, length));
        }

        public static bool VerifyHashedPassword(string? hashedPassword, string? plainPassword, string? saltPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(saltPassword))
            {
                return false;
            }
            var password = HashedPassword(plainPassword, saltPassword);
            return hashedPassword == password;
        }

        public static string HashedPassword(string password, string salt)
        {
            byte[] byteSalt = Convert.FromBase64String(salt);
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(password: password!,
                salt: byteSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
                );
        }

        public static string EncryptString(string secretKey, byte[] initializationVector, string plainText)
        {
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = initializationVector;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string secretKey, byte[] initializationVector, string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = initializationVector;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
