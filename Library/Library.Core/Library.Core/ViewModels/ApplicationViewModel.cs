using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.Core
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// Value holder for <see cref="IsLoading"/>
        /// </summary>
        private bool _isLoading { get; set; }
        #endregion


        #region Public Properties

        /// <summary>
        /// Flag to indicate if a skeleton control is visible
        /// </summary>
        public bool IsSkeletonVisible { get; set; }

        /// <summary>
        /// The repository of the application
        /// </summary>
        public ILibraryRepository rep { get; set; } = new MSSQL();

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPages CurrentPage { get; private set; } = ApplicationPages.MainPage;


        /// <summary>
        /// The current user of the application, standard is an empty user marked as visitor
        /// </summary>
        public UserViewModel CurrentUser { get; set; }

        /// <summary>
        /// Flag to indicate if <see cref="CurrentUser"/> is SuperAdmin (RoleID = 1)
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// Flag to indicate if  <see cref="CurrentUser"/> is admin (RoleID = 2)
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Flag to indicate if  <see cref="CurrentUser"/> is user (RoleID = 3)
        /// </summary>
        public bool IsUser { get; set; }


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
        public bool LoggedInToDatabase { get; set; } = true;

        /// <summary>
        /// Flag to indicate if the application is loading
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {

                if (_isLoading == value)
                    return;

                _isLoading = value;

                if (_isLoading)
                    OpenPopUp(PopUpContents.Loading);
                else
                    ClosePopUp();
            }
        }

        /// <summary>
        /// Flag to indicate if a page has been loaded
        /// </summary>
        public bool PageLoadComplete { get; set; }


        #endregion


        #region Public Methods

        /// <summary>
        /// Resets the current user
        /// </summary>
        public void ResetCurrentUser() { CurrentUser = new UserViewModel() { roleID = 4 }; }

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
        public void SetCurrentUserRole()
        {
            // Set all back to false
            IsSuperAdmin = IsAdmin = IsUser = false;
            IoC.CreateInstance<SideMenuControlViewModel>().ExitContent = "Gå tillbaka";

            // Check the role and set it
            switch (CurrentUser.roleID)
            {
                case 1:
                    {
                        IsSuperAdmin = IsAdmin = IsUser = true;
                        IoC.CreateInstance<SideMenuControlViewModel>().ExitContent = "Logga ut";
                        IoC.CreateInstance<AddUserControlViewModel>().GetRoles();
                        break;
                    }

                case 2:
                    {
                        IsAdmin = IsUser = true;
                        IoC.CreateInstance<SideMenuControlViewModel>().ExitContent = "Logga ut";
                        IoC.CreateInstance<AddUserControlViewModel>().GetRoles();
                        break;
                    }

                case 3:
                    {
                        IsUser = true;
                        IoC.CreateInstance<SideMenuControlViewModel>().ExitContent = "Logga ut";
                        break;
                    }
            }
        }


        public void OpenPopUp(PopUpContents content)
        {
            PopUpVisible = true;
            IoC.CreateInstance<PopUpControlViewModel>().PopUpContent = content;
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
