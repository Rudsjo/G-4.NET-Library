namespace Library.Core
{
    /// <summary>
    /// Categories for articles
    /// </summary>
    public enum ArticleCategories
    {
        /// <summary>
        /// A horror article
        /// </summary>
        Horror,

        /// <summary>
        /// A comedy article
        /// </summary>
        Comedy,

        /// <summary>
        /// An action article
        /// </summary>
        Action,  

        /* 
          
            Add more later

         */
    }

    /// <summary>
    /// Usertypes
    /// </summary>
    public enum UserTypes
    {
        /// <summary>
        /// Someone that can do everything
        /// </summary>
        Administrator = 0,

        /// <summary>
        /// A librarian ? ?
        /// </summary>
        Librarian = 1,

        /// <summary>
        /// A logged in user
        /// </summary>
        User = 2,

        /// <summary>
        /// A visitor that is not logged in
        /// </summary>
        Visitor = 3
    }

}
