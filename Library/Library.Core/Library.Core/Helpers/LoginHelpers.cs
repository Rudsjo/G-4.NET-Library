﻿using System;
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
        /// Attempts to login.
        /// </summary>
        /// <returns>True if login was successful</returns>
        public static async Task<User> AttemptLogin(string PersonalNumber, SecureString Password)
        {
            // Check if password is null or if the personalnumber is null.
            if (String.IsNullOrEmpty(PersonalNumber) || Password == null)
                return null;

            // Get the user's salt.
            var SaltBase64 = await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserSalt(PersonalNumber);

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

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="personalNumber">The personal number.</param>
        /// <param name="oldPass">The old password.</param>
        /// <param name="newPass">The new password.</param>
        /// <returns></returns>
        public static async Task<bool> UpdatePassword(string personalNumber, SecureString oldPass, SecureString newPass)
        {
            // Create a haser
            var Hasher = SHA256.Create();

            // Where the salt bytes will be places
            var SaltBytes = new byte[16];

            // Create a secure cryptographic random number generator
            using (RandomNumberGenerator RNG = RandomNumberGenerator.Create())
            {
                // Generate a new random salt
                RNG.GetBytes(SaltBytes);
            }

            // Convert the bytes into a Base64 string
            var Salt64 = Convert.ToBase64String(SaltBytes);
            // Get the old salt from the user
            var OldSalt = await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserSalt(personalNumber);

            // compute the hash from the new password
            newPass = Convert.ToBase64String(Hasher.ComputeHash(Encoding.Default.GetBytes(newPass.ToUnsecureString() + Salt64))).ToSecureString();

            // Compute the hash from the old password
            oldPass = Convert.ToBase64String(Hasher.ComputeHash(Encoding.Default.GetBytes(oldPass.ToUnsecureString() + OldSalt))).ToSecureString();

            // Return the result
            return (await IoC.CreateInstance<ApplicationViewModel>().rep.UpdatePassword(personalNumber, oldPass, newPass, Salt64));
        }

        /// <summary>
        /// Sets a new temp password for the user so they can login and change their password
        /// </summary>
        /// <param name="PersonalNumber"></param>
        /// <returns></returns>
        public static async Task<bool> ResetUserPassword(string PersonalNumber)
        {
            // Create the new password
            string Password = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.Default.GetBytes("12345")));

            // Update the users password
            return await IoC.CreateInstance<ApplicationViewModel>().rep.ResetUserPassword(PersonalNumber, Password);
        }

    }
}
