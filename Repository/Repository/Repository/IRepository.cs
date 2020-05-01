namespace Repository
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using Library.Core;
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// Interface that should be implemented in every
    /// in every backend class
    /// </summary>
    public interface IRepository
    {
        #region 'Add' declerations

        /// <summary>
        /// Add a new user to the system
        /// </summary>
        /// <returns>True if user could be added</returns>
        abstract bool AddUser();

        /// <summary>
        /// Add a new category to the system
        /// </summary>
        /// <returns>True if category could be added</returns>
        /// <param name="_categoryName">The name that will be given to the new category</param>
        abstract bool AddCategory(string _categoryName);

        /// <summary>
        /// Add a new article to the system
        /// </summary>
        /// <returns>Returns true if article could be added</returns>
        /// <param name="_article">Article to add to the system</param>
        abstract bool AddArticle(Article _article);

        #endregion

        #region 'Get' declerations

        /// <summary>
        /// Gets all atrticles from the system with a specific status
        /// </summary>
        /// <returns>An IEnumerable with all found articles</returns>
        /// <param name="_status">Search articles by this status</param>
        abstract object GetArticlesByStatus(int _status);

        #endregion

        #region 'Update' declerations

        /// <summary>
        /// Preform changes to an existing article
        /// </summary>
        /// <returns>True if article exists and was updated successfuly</returns>
        /// <param name="_articleID">ID of the article to be updated</param>
        /// <param name="newArticle">The new article</param>
        abstract bool UpdateArticle(int _articleID, Article newArticle);

        /// <summary>
        /// Preform changes to an existing category
        /// </summary>
        /// <returns>True if category exists and was updated successfuly</returns>
        /// <param name="_categoryID">The ID of the category to be updated</param>
        /// <param name="_newCategory">The new category name</param>
        abstract bool UpdateCategory(int _categoryID, string _newCategory);

        /// <summary>
        /// Preform changes to an existing user
        /// </summary>
        /// <returns>True if user exists and was updated successfuly</returns>
        /// <param name="_UserID">The ID of the user to be updated</param>
        /// <param name="_newUser">The new user info</param>
        abstract bool UpdateUser(int _UserID, User _newUser);

        #endregion

        #region 'Delete' declerations

        /// <summary>
        /// Removes an existing user from the system
        /// </summary>
        /// <returns>True if user exists and could be deteted</returns>
        /// <param name="_userID">The ID of the user to be deleted</param>
        abstract bool DeleteUser(int _userID);

        /// <summary>
        /// Removes and existing article from the system
        /// </summary>
        /// <returns>True if article exists and could be deleted</returns>
        /// <param name="_articleID">The ID of the article to be deleted</param>
        abstract bool DeleteArticle(int _articleID);

        /// <summary>
        /// Removes an existing category from the system
        /// </summary>
        /// <returns>True if category exists and could be deleted</returns>
        /// <param name="_categoryID">The ID of the caregory to be deleted</param>
        abstract bool DeleteCategory(int _categoryID);

        #endregion

        #region 'Check' declerations

        /// <summary>
        /// Checks if the user is blocked or not
        /// </summary>
        /// <returns>True if user exists and is blocked</returns>
        /// <param name="_userID">The ID of the user to check</param>
        abstract bool IsUserBlocked(int _userID);

        /// <summary>
        /// Checks if a specific article is reserved or not
        /// </summary>
        /// <returns>True if article exists and is reserved</returns>
        /// <param name="_articleID">The ID of the article to check</param>
        abstract bool IsArticleReserved(int _articleID);

        #endregion

        #region Searches

        /// <summary>
        /// Search for users in the system
        /// </summary>
        /// <returns>Returns a collection of the found users</returns>
        abstract IEnumerable<User> SearchUsers();

        /// <summary>
        /// Search for articles in the system
        /// </summary>
        /// <returns>Returns a collection of found users</returns>
        abstract IEnumerable<Article> SearchArticles();

        #endregion

        #region Actions

        /// <summary>
        /// Blocks a user
        /// </summary>
        /// <returns>True if user exists in the systen and could be blocked</returns>
        /// <param name="_userID">The ID of the user to be blocked</param>
        abstract bool BlockUser(int _userID);

        /// <summary>
        /// Unblocks a user
        /// </summary>
        /// <returns>True if user exists in the systen and could be unblocked</returns>
        /// <param name="_userID">The ID of the user to unblock</param>
        abstract bool UnblockUser(int _userID);

        /// <summary>
        /// Reserves an article for a user
        /// </summary>
        /// <returns>True if the article could be reserved</returns>
        /// <param name="_userID">ID of the user that the article will be reserved to</param>
        /// <param name="_articleID">The ID of the article/param>
        abstract bool ReserveArticle(int _userID, int _articleID);

        /// <summary>
        /// Lends an article to a user
        /// </summary>
        /// <returns>True if article was could be loaned</returns>
        /// <param name="_userID">ID of the user that the article will be lend to</param>
        /// <param name="_articleID">The ID of the article</param>
        abstract bool LoanArticle(int _userID, int _articleID);

        /// <summary>
        /// Return an article from a user
        /// </summary>
        /// <returns>True if article was returned successfuly</returns>
        /// <param name="_articleID">ID of the lend article</param>
        abstract bool ReturnArticle(int _articleID);

        /// <summary>
        /// Books a seminar
        /// </summary>
        /// <returns>Returns true if seminar could be booked</returns>
        /// <param name="_userID">ID of the user booking the seminar</param>
        /// <param name="_seminarID">The ID of the seminar</param>
        abstract bool BookSeminar(int _userID, int _seminarID);

        #endregion
    }
}
