namespace Library.Core
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using Library.Core;
    #endregion

    /// <summary>
    /// This model contains all necessary information
    /// about a user. It is supposed to be a return
    /// object from the repository.
    /// </summary>
    public class User
    {
        /// <summary>
        /// PersonalNumber of the user
        /// </summary>
        public string personalNumber { get; set; }

        /// <summary>
        /// Role ID
        /// </summary>
        public int roleID { get; set; }
        /// <summary>
        /// Role type string
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string lastName { get; set; }

    }
}
