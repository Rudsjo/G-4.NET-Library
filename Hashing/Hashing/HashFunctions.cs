namespace Hashing
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using System;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using Library.Core;
    #endregion

    /// <summary>
    /// Functions that will be moved to the Core library
    /// </summary>
    public class HashFunctions
    {
        /// <summary>
        ///     
        ///     This function is intended to be used when a new
        ///     user is created. A random salt is generated and
        ///     the provided password appends the salt and then
        ///     gets hashed with SHA256 before it is sent to the
        ///     database.
        /// 
        /// </summary>
        ///     <param name="Password">The password that will be hashed.</param>
        /// <returns> 
        /// 
        ///    ( PASSWORD-HASH, SALT ) 
        ///    
        /// </returns>
        public static ( SecureString, string ) CreatePasswordAndSalt(SecureString Password)
        {
            // Check if provided string is null.
            if (Password == null)
                // If null, throw an exception.
                throw new Exception("password can not be null");

            // Create a SHA256 hasher.
            var Hasher = SHA256.Create();

            // Where the salt bytes will be.
            var SaltBytes = new byte[16];

            // Create a crypyographic pseudo-random generator.
            using(RandomNumberGenerator RNG = RandomNumberGenerator.Create())
            {
                // Get 16 random bytes and fill the salt byte array.
                RNG.GetBytes(SaltBytes);
            }

            var SaltBase64 = Convert.ToBase64String(SaltBytes);

            /* - Add the salt to the end of the password.
             * - Get the bytes from the salted password.
             * - Hash the password.
             * - Convert it to a Base64 string.
             */ 
            var PasswordHash = Convert.ToBase64String ( Hasher.ComputeHash ( 
                                                        Encoding.Default.GetBytes ( 
                                                                                    Password + SaltBase64
                                                                                  ))).ToSecureString();
            // Return the password-hash and the salt.
            return (PasswordHash, SaltBase64);
        }

    }

}
