using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Tests
{

    [TestFixture]
    public class LoginHelpersTests
    {

        /// <summary>
        /// Test to login with non existing credentials
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AttemptLogin_NonExistingUserIsEntered_ReturnsNull()
        {
            // Get the result from the attempted login
            var result = await LoginHelpers.AttemptLogin("0", "0".ToSecureString());

            // Check that the result is null
            Assert.IsNull(result);
        }

        // Todo: this function needs to be public in order to test it here
        /*

        [Test]
        public async Task AttemptLogin_CorrectCredentials_ReturnsLoggedInUser()
        {
            // Create the user tp login as
            await IoC.CreateInstance<TableControlViewModel>().CreateUser("0000000000", "Test", "Test", 1, ("12345").ToSecureString());

            // Attempt the login
            var result = await LoginHelpers.AttemptLogin("0000000000", ("12345").ToSecureString());

            // Delete the user
            await IoC.CreateInstance<ApplicationViewModel>().rep.DeleteUser("0000000000");

            // Check the result
            Assert.That((result as IUser).personalNumber, Is.EqualTo("0000000000"));
        }

        */
    }
}
