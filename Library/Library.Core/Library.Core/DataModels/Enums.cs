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
        /// the page to create CSV files
        /// </summary>
        ReportPage = 3,      
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

        /// <summary>
        /// Popup content to add an article
        /// </summary>
        AddArticle = 5,

        /// <summary>
        /// Content when page is loading
        /// </summary>
        Loading = 6,

        /// <summary>
        /// Content with confirmation
        /// </summary>
        Confirmation = 7,
        
        /// <summary>
        /// Popup for successful add 
        /// </summary>
        Success = 8,

        /// <summary>
        /// Popup for infoscreen
        /// </summary>
        Info = 9,

        /// <summary>
        /// Popup for editscreen
        /// </summary>
        Edit = 10,

        /// <summary>
        /// Popup for filterscreen 
        /// </summary>
        Filter = 11,

        /// <summary>
        /// Popup for user info and edit
        /// </summary>
        UserInfo = 12,

        /// <summary>
        /// Popup for Notificationscreen
        /// </summary>
        Notification = 13,

        /// <summary>
        /// Popup for MyProfile
        /// </summary>
        MyProfile = 14,

        /// <summary>
        /// Popup for returning multiple loans
        /// </summary>
        ReturnLoans = 15,

        /// <summary>
        /// Popup for the change password
        /// </summary>
        ChangePassword = 16,

        /// <summary>
        /// Popup for the report filter
        /// </summary>
        ReportFilter = 17,

        /// <summary>
        /// Popup for downloading
        /// </summary>
        Downloading = 18,

    }

    /// <summary>
    /// All the sortable tables
    /// </summary>
    public enum SortableTables
    {
        /// <summary>
        /// No sorting to be done
        /// </summary>
        None = 0,

        /// <summary>
        /// Sort by <see cref="UserViewModel.personalNumber"/>
        /// </summary>
        PersonalNumber = 1,

        /// <summary>
        /// Sort by <see cref="UserViewModel.firstName"/>
        /// </summary>
        FirstName = 2,

        /// <summary>
        /// Sort by <see cref="UserViewModel.lastName"/>
        /// </summary>
        LastName = 3,

        /// <summary>
        /// Sort by <see cref="UserViewModel.loanedArticles"/>
        /// </summary>
        LoanedArticles = 4,

        /// <summary>
        /// Sort by <see cref="UserViewModel.reservedArticles"/>
        /// </summary>
        ReservedArticles = 5,

        /// <summary>
        /// Sort by <see cref="ArticleViewModel.title"/>
        /// </summary>
        Title = 6,

        /// <summary>
        /// Sort by <see cref="ArticleViewModel.author"/>
        /// </summary>
        Author = 7,

        /// <summary>
        /// Sort by <see cref="ArticleViewModel."/>
        /// </summary>
        Edition = 8,

        /// <summary>
        /// Sort by <see cref="ArticleViewModel.quantity"/>
        /// </summary>
        Availability = 9,

        /// <summary>
        /// Sort by <see cref="ArticleViewModel.placement"/>
        /// </summary>
        Placement = 10,
    }

    /// <summary>
    /// Change the FilterColors
    /// </summary>
    public enum FilterColors
    {
        UnChecked = 1,

        Checked = 2
    }

    /// <summary>
    /// Change the Notification icon color
    /// </summary>
    public enum NotificationColors
    {
        NoNotification = 1,

        Notification = 2,

    }

    public enum ReasonTypes
    {
        ArticleReasons = 0,
        UserReasons = 1,
    }

}
