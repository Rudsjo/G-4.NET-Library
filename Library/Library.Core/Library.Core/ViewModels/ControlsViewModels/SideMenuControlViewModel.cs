using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class SideMenuControlViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// Command to change page in the application
        /// </summary>
        public ICommand ChangePage { get; set; }

        /// <summary>
        /// The content to be shown in the exit button
        /// </summary>
        public string ExitContent { get; set; } = "Gå tillbaka";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuControlViewModel()
        {
            // Create the command for changing page
            ChangePage = new RelayParameterizedCommand( (pageToOpen) => ChangePageCommand(pageToOpen) );
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command to log out a user
        /// </summary>
        /// <returns></returns>
        private void ChangePageCommand(object pageToOpen)
        {
            // Resets the Filterpopup when changing page
            IoC.CreateInstance<MainContentUserControlViewModel>().ResetFilterPopup();

            switch ((string)pageToOpen)
            {
                // If the suser pressed the Books button
                case "BookPage":
                    {
                        // Change page
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);

                        // Load all articles
                        IoC.CreateInstance<BookPageViewModel>().FillSearchableArticleList();
                        IoC.CreateInstance<TableControlViewModel>().LoadItems();

                        break;
                    }
                // If the uesr pressed the employees button
                case "EmployeePage":
                    {
                        // Change page
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.EmployeePage);

                        // Load all employees
                        IoC.CreateInstance<EmployeePageViewModel>().FillSearchableUserList();
                        IoC.CreateInstance<TableControlViewModel>().LoadItems();

                        break;
                    }
                // If the user pressed the log out button
                case "Logout":
                    {
                        // Log out the current user
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.MainPage);
                        
                        break;
                    }
            }
        }

        #endregion
    }
}
