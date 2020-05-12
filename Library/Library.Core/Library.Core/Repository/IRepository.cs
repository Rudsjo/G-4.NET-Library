namespace Repository
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using Library.Core;
    using System.Collections.Generic;
    using System.Security;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// A non generic repository designed 
    /// just for this library system.
    /// </summary>
    public interface ILibraryRepository
    {
        #region 'Add' declerations

        /// <summary>
        /// Add a new user to the system
        /// </summary>
        /// <returns>True if user could be added</returns>
        abstract Task<bool> AddUser(string PersonalNumber, string FirstName, string LastName, int RoleID, SecureString Password, string SaltBase64);

        /// <summary>
        /// Add a new category to the system
        /// </summary>
        /// <returns>True if category could be added</returns>
        /// <param name="_categoryName">The name that will be given to the new category</param>
        abstract Task<bool> AddCategory(string _categoryName);

        /// <summary>
        /// Add a new article to the system
        /// </summary>
        /// <returns>Returns true if article could be added</returns>
        /// <param name="_article">Article to add to the system</param>
        abstract Task<bool> AddArticle(Article _article);

        #endregion

        #region 'Get' declerations

        /// <summary>
        /// Gets all atrticles from the system with a specific status
        /// </summary>
        /// <returns>An IEnumerable with all found articles</returns>
        /// <param name="_status">Search articles by this status</param>
        abstract Task<IEnumerable<Article>> GetArticlesByStatus(int _status);

        /// <summary>
        /// Gets the user by their identifier.
        /// </summary>
        /// <param name="_personalNumber">The personalnumber.</param>
        /// <returns>The salt from that user</returns>
        abstract Task<string> GetUserByID(string _personalNumber);

        /// <summary>
        /// Gets all the roles
        /// </summary>
        /// <returns></returns>
        abstract Task<IEnumerable<Role>> GetAllRoles();

        /// <summary>
        /// Gets the user loans.
        /// </summary>
        /// <param name="_personalNumber">The user's personal number.</param>
        /// <returns></returns>
        abstract Task<IEnumerable<Article>> GetUserLoans(string _personalNumber);
        #endregion

        #region 'Update' declerations

        /// <summary>
        /// Preform changes to an existing article
        /// </summary>
        /// <returns>True if article exists and was updated successfuly</returns>
        /// <param name="_articleID">ID of the article to be updated</param>
        /// <param name="newArticle">The new article</param>
        abstract Task<bool> UpdateArticle(int _articleID, Article newArticle);

        /// <summary>
        /// Preform changes to an existing category
        /// </summary>
        /// <returns>True if category exists and was updated successfuly</returns>
        /// <param name="_categoryID">The ID of the category to be updated</param>
        /// <param name="_newCategory">The new category name</param>
        abstract Task<bool> UpdateCategory(int _categoryID, string _newCategory);

        /// <summary>
        /// Preform changes to an existing user
        /// </summary>
        /// <returns>True if user exists and was updated successfuly</returns>
        /// <param name="_UserID">The ID of the user to be updated</param>
        /// <param name="_newUser">The new user info</param>
        abstract Task<bool> UpdateUser(string _UserID, User _newUser);

        #endregion

        #region 'Delete' declerations

        /// <summary>
        /// Removes an existing user from the system
        /// </summary>
        /// <returns>True if user exists and could be deteted</returns>
        /// <param name="_userID">The ID of the user to be deleted</param>
        abstract Task<bool> DeleteUser(string _userID);

        /// <summary>
        /// Removes and existing article from the system
        /// </summary>
        /// <returns>True if article exists and could be deleted</returns>
        /// <param name="_articleID">The ID of the article to be deleted</param>
        abstract Task<bool> DeleteArticle(int _articleID);

        /// <summary>
        /// Removes an existing category from the system
        /// </summary>
        /// <returns>True if category exists and could be deleted</returns>
        /// <param name="_categoryID">The ID of the caregory to be deleted</param>
        abstract Task<bool> DeleteCategory(int _categoryID);

        #endregion

        #region 'Check' declerations

        /// <summary>
        /// Checks if the user is blocked or not
        /// </summary>
        /// <returns>True if user exists and is blocked</returns>
        /// <param name="_userID">The ID of the user to check</param>
        abstract Task<bool> IsUserBlocked(string _userID);

        /// <summary>
        /// Checks if a specific article is reserved or not
        /// </summary>
        /// <returns>True if article exists and is reserved</returns>
        /// <param name="_articleID">The ID of the article to check</param>
        abstract Task<bool> IsArticleReserved(int _articleID);

        #endregion

        #region Searches

        /// <summary>
        /// Search for users in the system
        /// </summary>
        /// <param name="_keyword"></param>
        /// <returns>A collection of found users</returns>
        abstract Task<IEnumerable<User>> SearchUsers(string _keyword = "");

        /// <summary>
        /// Search for articles in the system
        /// </summary>
        /// <param name="_keyword"></param>
        /// <returns>A collection of found articles</returns>
        abstract Task<IEnumerable<Article>> SearchArticles(string _keyword);

        #endregion

        #region Actions

        /// <summary>
        /// Blocks a user
        /// </summary>
        /// <returns>True if user exists in the systen and could be blocked</returns>
        /// <param name="_personalNumber">The ID of the user to be blocked</param>
        abstract Task<bool> BlockUser(string _personalNumber, string _reason);

        /// <summary>
        /// Unblocks a user
        /// </summary>
        /// <returns>True if user exists in the systen and could be unblocked</returns>
        /// <param name="_personalNumber">The ID of the user to unblock</param>
        abstract Task<bool> UnblockUser(string _userID);

        /// <summary>
        /// Reserves an article for a user
        /// </summary>
        /// <returns>True if the article could be reserved</returns>
        /// <param name="_personalNumber">ID of the user that the article will be reserved to</param>
        /// <param name="_articleID">The ID of the article/param>
        /// <param name="_dateTime">Time of the reservation</param>
        abstract Task<bool> ReserveArticle(string _personalNumber, int _articleID, string _dateTime);

        /// <summary>
        /// Lends an article to a user
        /// </summary>
        /// <returns>True if article was could be loaned</returns>
        /// <param name="_personalNumber">ID of the user that the article will be lend to</param>
        /// <param name="_articleID">The ID of the article</param>
        abstract Task<bool> LoanArticle(string _personalNumber, int _articleID);

        /// <summary>
        /// Return an article from a user
        /// </summary>
        /// <returns>True if article was returned successfuly</returns>
        /// <param name="_articleID">ID of the lend article</param>
        abstract Task<bool> ReturnArticle(int _articleID);

        /// <summary>
        /// Books a seminar
        /// </summary>
        /// <returns>Returns true if seminar could be booked</returns>
        /// <param name="_personalNumber">ID of the user booking the seminar</param>
        /// <param name="_seminarID">The ID of the seminar</param>
        abstract Task<bool> BookSeminar(string _personalNumber, int _seminarID);

        /// <summary>
        /// Attempts to login the user
        /// </summary>
        /// <param name="_personalNumber">The personal number.</param>
        /// <param name="Password">The password.</param>
        /// <returns>Returns the <see cref="User"/ if the login was successful,
        ///          Null if the login failed.></returns>
        abstract Task<User> AttemptLogin(string _personalNumber, SecureString Password);

        #endregion
    }
}
