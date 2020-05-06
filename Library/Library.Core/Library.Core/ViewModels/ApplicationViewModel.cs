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
        public ApplicationPages CurrentPage { get; private set; } = ApplicationPages.LoginToDatabasePage;


        /// <summary>
        /// Flag to indicate if the side menu should be visible or not
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;


        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPages page)
        {
            // Set the current page
            CurrentPage = page;

            // Check if side menu should be visible
            SideMenuVisible = page != ApplicationPages.LoginToDatabasePage;
        }

        #endregion


    }
}
