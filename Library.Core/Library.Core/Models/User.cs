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
        /// The user's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user type <see cref="UserTypes"/>
        /// </summary>
        public UserTypes Type { get; set; }
    }
}
