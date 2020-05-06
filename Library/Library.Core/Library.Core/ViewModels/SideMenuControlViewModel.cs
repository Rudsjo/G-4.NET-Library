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
        /// Command to log out a user
        /// </summary>
        public ICommand Logout { get; set; } 

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuControlViewModel()
        {
            // Setting commands
            Logout = new RelayCommand(async () => await LogoutCommandAsync());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command to log out a user
        /// </summary>
        /// <returns></returns>
        private async Task LogoutCommandAsync()
        {
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.LoginToDatabasePage);

            await Task.Delay(1);
        }

        #endregion
    }
}
