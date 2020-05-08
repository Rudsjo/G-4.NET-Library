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
            var Result = await Login.AttemptLogin("0000000000", ("123456").ToSecureString());

            // Check so that the result is false.
            Assert.IsFalse(Result);
        }
    }
}