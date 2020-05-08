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
        /// The main page to hold basic information about all articles in a non-logged in mode
        /// </summary>
        MainPage = 0,

        /// <summary>
        /// The book page containing all available books in the system
        /// </summary>
        BookPage = 1,

        /// <summary>
        /// The page to display all employess
        /// </summary>
        EmployeePage = 2,

        /// <summary>
        /// the page to display all customers
        /// </summary>
        CustomerPage = 3,      
    }

    /// <summary>
    /// The different types of pop up windows
    /// </summary>
    public enum PopUpContents
    {
        /// <summary>
        /// No popup
        /// </summary>
        None = 0,

        /// <summary>
        /// The login popup for the database
        /// </summary>
        DatabaseLogin = 1,

        /// <summary>
        /// The login popup for users
        /// </summary>
        UserLogin = 2,

        /// <summary>
        /// The popup content when any database error is happening
        /// </summary>
        DatabaseError = 3,

        /// <summary>
        /// Popup content to add a user
        /// </summary>
        AddUser = 4,

    }

    /// <summary>
    /// All the sortable tables
    /// </summary>
    public enum SortableTables
    {
        /// <summary>
        /// 
        /// </summary>
        PersonalNumber = 0,

        /// <summary>
        /// 
        /// </summary>
        FirstName = 1,

        /// <summary>
        /// 
        /// </summary>
        LastName = 2,

        /// <summary>
        /// 
        /// </summary>
        LoanedArticles = 3,

        /// <summary>
        /// 
        /// </summary>
        ReservedArticles = 4,

        /// <summary>
        /// 
        /// </summary>
        Title = 5,
    }

}
