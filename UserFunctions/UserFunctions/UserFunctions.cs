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
    using System.Linq;
    #endregion

    /// <summary>
    /// Library that contains the login function.
    /// </summary>
    public class UserFunctions
    {
        /// <summary>
        /// The repository.
        /// </summary>
        public static ILibraryRepository rep => new MSSQL();

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

        /// <summary>
        /// Creates a user and adds it to the system
        /// </summary>
        /// <param name="PersonalNumber"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="RoleID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static async Task<bool> CreateUser(string PersonalNumber, 
                                                  string FirstName, 
                                                  string LastName, 
                                                  int RoleID, 
                                                  SecureString Password)
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
            using (RandomNumberGenerator RNG = RandomNumberGenerator.Create())
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
            Password = Convert.ToBase64String( Hasher.ComputeHash(
                                               Encoding.Default.GetBytes(
                                                                          Password.ToUnsecureString() + SaltBase64
                                                                         ))).ToSecureString();
            // Try to add the new user to the system.
            return await rep.AddUser(PersonalNumber, FirstName, LastName, RoleID, Password, SaltBase64);
        }

        /// <summary>
        /// Determines if a user can be deleted.
        /// </summary>
        /// <param name="PersonalNumber">The personal number.</param>
        /// <returns>
        ///   <c>true</c> if collection contains no articles; otherwise, <c>false</c>.
        /// </returns>
        public static async Task<bool> CanDeleteUser(string PersonalNumber)
        =>
        (await rep.GetUserLoans(PersonalNumber)).ToList().Count() == 0;

        /// <summary>
        /// Deletes the loan from user.
        /// </summary>
        /// <param name="ArticleID">The article identifier.</param>
        /// <returns></returns>
        public static async Task<bool> ReturnArticle(int ArticleID)
        =>
        await rep.ReturnArticle(ArticleID);
        

    }
}
