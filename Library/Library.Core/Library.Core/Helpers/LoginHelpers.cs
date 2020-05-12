using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core
{
    public static class LoginHelpers
    {

        /// <summary>
        /// Attempts the login.
        /// </summary>
        /// <returns>True if login was successful</returns>
        public static async Task<User> AttemptLogin(string PersonalNumber, SecureString Password)
        {
            // Check if password is null or if the personalnumber is null.
            if (String.IsNullOrEmpty(PersonalNumber) || Password == null)
                return null;

            // Get the user's salt.
            var SaltBase64 = await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserByID(PersonalNumber);

            // If the result is null, then we know that the username is incorrect.
            if (SaltBase64 == null)
                return null;

            // Create a SHA256 hasher.
            var Hasher = SHA256.Create();

            // - Append salt to the password.
            // - Compute hash.
            // - Return the bytes as a SecureString.
            Password = Convert.ToBase64String(Hasher.ComputeHash(
                                            Encoding.Default.GetBytes(
                                                                        Password.ToUnsecureString() + SaltBase64
                                                                     ))).ToSecureString();

            // Attempt to login with the provided password.
            var newUser = await IoC.CreateInstance<ApplicationViewModel>().rep.AttemptLogin(PersonalNumber, Password);

            // Dispose of the password.
            Password.Dispose();

            return newUser;
        }

    }
}
