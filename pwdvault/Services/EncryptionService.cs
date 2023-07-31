﻿using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Serilog;

namespace pwdvault.Services
{
    internal class EncryptionService
    {
        private const int SALT_SIZE = 16; // 128 bit
        private const int HASH_SIZE = 16; // 128 bit
        private const int ITERATIONS = 4; // Recommanded minimum value
        private const int MEMORY_SIZE = 512000; // 512 MB

        /// <summary>
        /// <para>
        /// This method take the new password, the encryption key, the initialization vector and returns the encrypted password using AES256 encryption algorithm.
        /// The initialization vector is randomly generated by AES, and is returned.
        /// </para>
        /// </summary>
        /// <param name="password"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] EncryptPassword(string password, byte[] key, out byte[] iv)
        {
            try
            {
                Log.Logger.Information("Encrypting the password...");
                if (String.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("The password is empty.");
                }
                if (key == null || key.Length == 0)
                {
                    throw new ArgumentException("The encryption key is either null or empty.");
                }


                using var aes = Aes.Create();
                aes.Key = key;
                aes.GenerateIV();
                iv = aes.IV;

                var memoryStream = new MemoryStream();
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    var passwordBytes = Encoding.UTF8.GetBytes(password);
                    cryptoStream.Write(passwordBytes, 0, passwordBytes.Length);
                }

                /*using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write);
                var passwordBytes = Encoding.UTF32.GetBytes(password);
                cryptoStream.Write(passwordBytes, 0, passwordBytes.Length);
                */
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Source : " + ex.Source + ", Message : " + ex.Message + "\n" + ex.StackTrace);
                throw new Exception(ex.Message, ex);
            }
            
        }

        /// <summary>
        /// <para>
        /// This method take the encrypted password, the decryption key, the initialization vector and returns the decrypted password using AES256 decryption algorithm.
        /// </para>
        /// </summary>
        /// <param name="encryptedPassword"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string DecryptPassword(byte[] encryptedPassword, byte[] key, byte[] iv)
        {
            try
            {
                Log.Logger.Information("Decrypting password...");
                if (encryptedPassword == null || encryptedPassword.Length == 0)
                {
                    throw new ArgumentException("The encrypted password is either null or empty.");
                }
                if (key == null || key.Length == 0)
                {
                    throw new ArgumentException("The decryption key is either null or empty.");
                }

                using Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;

                using var memoryStream = new MemoryStream(encryptedPassword);
                using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read);
                using var msPlain = new MemoryStream();
                cryptoStream.CopyTo(msPlain);
                return Encoding.UTF8.GetString(msPlain.ToArray());

                /*using var aes = Aes.Create();
                aes.Key = key;

                using var memoryStream = new MemoryStream(encryptedPassword);
                var iv = new byte[IV_SIZE];
                memoryStream.Read(iv, 0, IV_SIZE);
                aes.IV = iv;

                using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
                var decryptedPasswordByte = new byte[encryptedPassword.Length];
                var byteCountPassword = cryptoStream.Read(decryptedPasswordByte, 0, encryptedPassword.Length);

                return Encoding.UTF32.GetString(decryptedPasswordByte, 0, byteCountPassword);*/
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Source : " + ex.Source + ", Message : " + ex.Message + "\n" + ex.StackTrace);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// <para>
        /// This method takes the app's password and generate a new key for encrypting the password. The key generation is derived from the password.
        /// </para>
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte[] GenerateKey(string password)
        {
            Log.Logger.Information("Generating encryption key...");
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("The password is empty.");
            }
            return GenerateHash(password);
        }

        /// <summary>
        /// Generates hash for user's password using Argon2id algorithm to minimize brute force and side channel attacks.
        /// The parameters affecting security and performance of Argon2id hash algorithm are number of parallelism, iterations and memory size.
        /// Degree of parallelism is equal to number of CPU Cores * 2, which is the specification of Argon2id.
        /// To know what iterations and memory size to use, benchmarking and testing needs to be done to get the amount of time used to compute the hash.
        /// The time used for the hashing should not be lower than 0,5 seconds and not greater than 5 seconds.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static byte[] GenerateHash(string password)
        {
            using var argon2id = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2id.Salt = GenerateSalt();
            // Number of CPU Cores x2
            argon2id.DegreeOfParallelism = Environment.ProcessorCount * 2;
            argon2id.Iterations = ITERATIONS;
            argon2id.MemorySize = MEMORY_SIZE;
            return argon2id.GetBytes(HASH_SIZE);
        }

        /// <summary>
        /// Generates 16 bytes salt for user's password.
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateSalt()
        {
            var salt = new byte[SALT_SIZE];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);
            return salt;
        }
    }
}
