namespace Repository
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces

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
        abstract bool AddCategory();

        /// <summary>
        /// Add a new article to the system
        /// </summary>
        /// <returns>Returns true if article could be added</returns>
        abstract bool AddArticle();

        #endregion

        #region 'Get' declerations

        /// <summary>
        /// Gets all atrticles from the system with a specific status
        /// </summary>
        /// <returns>An IEnumerable with all found articles</returns>
        abstract object GetAtriclesByStatus();

        #endregion

        #region 'Update' declerations

        /// <summary>
        /// Preform changes to an existing article
        /// </summary>
        /// <returns>True if article exists and was updated successfuly</returns>
        abstract bool UpdateAtricle();

        /// <summary>
        /// Preform changes to an existing category
        /// </summary>
        /// <returns>True if category exists and was updated successfuly</returns>
        abstract bool UpdateCategory();

        /// <summary>
        /// Preform changes to an existing user
        /// </summary>
        /// <returns>True if user exists and was updated successfuly</returns>
        abstract bool UpdateUser();

        #endregion

        #region 'Delete' declerations

        /// <summary>
        /// Removes an existing user from the system
        /// </summary>
        /// <returns></returns>
        abstract bool DeleteUser();

        /// <summary>
        /// Removes and existing article from the system
        /// </summary>
        /// <returns></returns>
        abstract bool DeleteAtricle();

        /// <summary>
        /// Removes an existing category from the system
        /// </summary>
        /// <returns></returns>
        abstract bool DeleteCategory();

        #endregion

        #region 'Check' declerations

        /// <summary>
        /// Checks if the user is blocked or not
        /// </summary>
        /// <returns>True if user exists and is blocked</returns>
        abstract bool IsUserBlocked();

        /// <summary>
        /// Checks if a specific article is reserved or not
        /// </summary>
        /// <returns>True if article exists and is reserved</returns>
        abstract bool IsArticleReserved();

        #endregion

        #region Searches

        /// <summary>
        /// Search for users in the system
        /// </summary>
        /// <returns>Returns a list of the found users</returns>
        abstract object SearchUsers();

        /// <summary>
        /// Search for articles in the system
        /// </summary>
        /// <returns>Returns a list of found users</returns>
        abstract object SearchArticles();

        #endregion

        #region Actions

        /// <summary>
        /// Blocks a user
        /// </summary>
        /// <returns>True if user exists in the systen and could be blocked</returns>
        abstract bool BlockUser();

        /// <summary>
        /// Unblocks a user
        /// </summary>
        /// <returns>True if user exists in the systen and could be unblocked</returns>
        abstract bool UnblockUser();

        /// <summary>
        /// Reserves an article for a user
        /// </summary>
        /// <returns>True if the article could be reserved</returns>
        abstract bool ReserveArticle();

        /// <summary>
        /// Lends an article to a user
        /// </summary>
        /// <returns>True if article was could be loaned</returns>
        abstract bool LoanArticle();

        /// <summary>
        /// Returns an article to the system
        /// </summary>
        /// <returns>True if article was returned successfuly</returns>
        abstract bool ReturnArticle();

        /// <summary>
        /// Books a seminar
        /// </summary>
        /// <returns>Returns true if seminar could be booked</returns>
        abstract bool BookSeminar();

        #endregion
    }

    #region Backends

    /// <summary>
    /// Backend class that uses Microsoft SQL Server
    /// </summary>
    public class MSSQL // : IRepository
    {
        /* Väntar med att implementera interfacet 
         * till nödvändiga klasser är skapade
         */
    }

    #endregion
}
