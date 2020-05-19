namespace LoginTester
{
    #region Namespaces
    using Login;
    using NUnit.Framework;
    using Library.Core;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// Tests for the LoginFunction.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Tests to login with wrong credentials.
        /// </summary>
        [Test]
        public async Task TestLogin()
        {
            // Get the result from the function.
            var Result = await UserFunctions.AttemptLogin("0000000000", ("123456").ToSecureString());

            // Check so that the result is false.
            Assert.IsFalse(Result);
        }

        /// <summary>
        /// Tries to add a new user to the database.
        /// </summary>
        [Test]
        public async Task AddUser()
        {
            // Get the result from the function.
            var Result = await UserFunctions.CreateUser("9803147777", "Jimmy", "Sassila", 3, ("Pass123").ToSecureString());

            // Delete the user
            await UserFunctions.rep.DeleteUser("9803147777");

            // The user should be have been added successfully.
            Assert.IsTrue(Result);
        }

        /// <summary>
        /// Test the 'CanDeleteUser' function.
        /// </summary>
        [Test]
        public async Task CanUserBeDeleted()
        {
            var res = await UserFunctions.CanDeleteUser("9803148536");

            // Should always return true because this user is a 'dummy user' with no loaned articles
            Assert.IsTrue(res);
        }

        /// <summary>
        /// Test the 'LoanArticle function'
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LoanArticle()
        {
            // Get the result from the function
            var result = await UserFunctions.LoanArticle("2222222222", 86);

            // Returns true if the Article is available to loan
                // In this case the result will return False.
            Assert.IsFalse(result);
        }
    }
}