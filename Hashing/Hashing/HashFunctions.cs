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
        ///     Hashes a password with a salt.
        ///     <usage>
        ///         This function is intended to be used when a new
        ///         user is created. A random salt is generated and
        ///         the provided password appends the salt and then
        ///         gets hashed with SHA256 before it is sent to the
        ///         database.
        ///     </usage>
        /// </summary>
        ///     <param name="Password">The <see cref="SecureString"/> that will be hashed.</param>
        /// <returns> 
        /// 
        ///    ( PASSWORD-HASH, SALT ) 
        ///    
        /// </returns>
        public static ( SecureString, string ) GenerateSaltAndHashPassword(SecureString Password)
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
                                                                                    Password.ToUnsecureString() + SaltBase64
                                                                                  ))).ToSecureString();
            // Return the password-hash and the salt.
            return (PasswordHash, SaltBase64);
        }

        /// <summary>
        ///     Hashes a password with a privided salt.
        ///     <usage>
        ///         This functin is intended to be used when a user 
        ///         attempts to login, the database will provide the 
        ///         provided users's salt (if it exists) and the 
        ///         client hashes the input password with that salt 
        ///         before it is sent of to the database for comparison.
        ///     </usage>
        /// </summary>
        /// <param name="InputPassword">Provided <see cref="SecureString"/></param>
        /// <param name="SaltBase64"> The salt that was provided from the database,
        ///                           Represented as a Base64 string.
        ///                           </param>
        /// <returns></returns>
        public static SecureString HashPasswordWithSpecificSalt(SecureString InputPassword, string SaltBase64)
        {
            // Create a SHA256 hasher.
            var Hasher = SHA256.Create();

            // - Append salt to the password.
            // - Compute hash.
            // - Return the bytes as a SecureString.
            return Convert.ToBase64String ( Hasher.ComputeHash (
                                            Encoding.Default.GetBytes (
                                                                        InputPassword.ToUnsecureString() + SaltBase64
                                                                     ))).ToSecureString();           
        }

    }

}
