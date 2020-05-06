namespace Library.Core
{
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

    /// <summary>
    /// The pages of the application
    /// </summary>
    public enum ApplicationPages
    {
        /// <summary>
        /// The login page for the database. 
        /// This page will demand the user to log in before any actions can be made in the application.
        /// </summary>
        LoginToDatabasePage = 0,

        /// <summary>
        /// The main page to hold basic information about all articles in a non-logged in mode
        /// </summary>
        MainPage = 1,

        /// <summary>
        /// The book page containing all available books in the system
        /// </summary>
        BookPage = 2,
        
    }

}
