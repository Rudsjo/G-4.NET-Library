namespace Login
{
    /// <summary>
    /// Required namespaces.
    /// </summary>
    #region namespaces
    using System;
    using System.Security;
    using Repository;
    using Library.Core;
    using System.Threading.Tasks;
    using System.Security.Cryptography;
    using System.Text;
    #endregion

    /// <summary>
    /// Library that contains the login function.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private static ILibraryRepository rep => new MSSQL();

        /// <summary>
        /// Attempts the login.
        /// </summary>
        /// <returns>True if login was successful</returns>
        public static async Task<bool> AttemptLogin(string PersonalNumber, SecureString Password)
        {
            // Check if password is null or if the personalnumber is null.
            if (String.IsNullOrEmpty(PersonalNumber) || Password == null)
                return false;

            // Get the user's salt.
            var SaltBase64 = await rep.GetUserByID(PersonalNumber);

            // If the result is null, then we know that the username is incorrect.
            if (SaltBase64 == null)
                return false;

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
            var newUser = await rep.AttemptLogin(PersonalNumber, Password);

            // Dispose of the password.
            Password.Dispose();

            // Check if login was successful.
            if (newUser != null)
            {
                // -- Set CurrentUser to the newUser

                // Login was successful.
                return true;
            }
            // Login failed.
            else
                return false;
        }

    }
}
