namespace LoginTester
{
    using Library.Core;
    #region Namespaces
    using Login;
    using NUnit.Framework;
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
    }
}