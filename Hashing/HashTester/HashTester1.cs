namespace HashTester
{
    /// <summary>
    /// Required namespaces.
    /// </summary>
    #region Namespaces
    using NUnit.Framework;
    using Library.Core;
    using System.Security;
    using static Hashing.HashFunctions;
    #endregion

    /// <summary>
    /// Tester class.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Creates the password and salt test.
        /// </summary>
        [Test]
        public void CreatePasswordAndSaltTest()
        {
            // Create a passwordhash.
            var result1 = CreatePasswordAndSalt("password123".ToSecureString());

            // Create a passwordhash with the same password.
            var result2 = CreatePasswordAndSalt("password123".ToSecureString());

            // Compare their values.
            // - They should not have the same hash.
            // - They should not have the same salt.
            Assert.IsTrue((result1.Item1.ToUnsecureString() != result2.Item1.ToUnsecureString()) &&
                           result1.Item2.CompareTo(result2.Item2) != 0);
        }
    }
}