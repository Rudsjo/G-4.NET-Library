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
            // Setting commands
            ChangePage = new RelayParameterizedCommand(async (pageToOpen) => await LogoutCommandAsync(pageToOpen));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command to log out a user
        /// </summary>
        /// <returns></returns>
        private async Task LogoutCommandAsync(object pageToOpen)
        {
            // Resets the Filterpopup when changing page
            IoC.CreateInstance<MainContentUserControlViewModel>().ResetFilterPopup();

            switch ((string)pageToOpen)
            {
                case "BookPage":
                    {
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);
                        break;
                    }

                case "EmployeePage":
                    {
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.EmployeePage);
                        break;
                    }

                case "CustomerPage":
                    {
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.CustomerPage);
                        break;
                    }

                case "Logout":
                    {
                        IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.MainPage);
                        break;
                    }
            }

            await Task.Delay(1);
        }

        #endregion
    }
}
