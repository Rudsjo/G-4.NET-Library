namespace Library.Core
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPages CurrentPage { get; private set; } = ApplicationPages.MainPage;

        /// <summary>
        /// The role of the current user
        /// </summary>
        public UserTypes CurrentRole { get; private set; } = UserTypes.Visitor;


        /// <summary>
        /// Flag to indicate if the side menu should be visible or not
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// Flag to indicate if a popup window should be shown or not
        /// </summary>
        public bool PopUpVisible { get; set; } = false;

        /// <summary>
        /// Flag to indicate if the system is connected to the database or not
        /// </summary>
        public bool LoggedInToDatabase { get; set; } = false;


        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPages page)
        {
            // Check if there is a connection to the database
            if (!LoggedInToDatabase)
            {
                // Setting the pop up content
                IoC.CreateInstance<PopUpControlViewModel>().PopUpContent = PopUpContents.DatabaseLogin;

                // Showing the pop up window
                PopUpVisible = true;
            }

            // Set the current page
            CurrentPage = page;

            // Check if side menu should be visible
            SideMenuVisible = page != ApplicationPages.MainPage;
        }

        /// <summary>
        /// Setting the current role of the user
        /// </summary>
        /// <param name="role">The role of the current user</param>
        public void SetCurrentUserRole(UserTypes role)
        {
            // Setting the role
            CurrentRole = role;
        }

        /// <summary>
        /// Closes the current pop up control
        /// </summary>
        public void ClosePopUp()
        {
            // Making sure no pop ups can appear
            LoggedInToDatabase = true;

            // Closing
            PopUpVisible = false;
        }

        #endregion


    }
}
