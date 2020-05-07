using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class MainContentUserControlViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// The command to open MyProfile popup,
        /// if user is not logged in the login pop up will be displayed
        /// </summary>
        public ICommand MyProfile { get; set; }

        #endregion


        #region Contructor

        public MainContentUserControlViewModel()
        {
            MyProfile = new RelayCommand(async () => await MyProfileCommandAsync());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command to open a MyProfile popup,
        /// if user is not logged in the login popup will be displayed
        /// </summary>
        /// <returns></returns>
        public async Task MyProfileCommandAsync()
        {
            // Indicating that a pop up control will be shown
            IoC.CreateInstance<ApplicationViewModel>().PopUpVisible = true;

            // Setting the pop up content
            IoC.CreateInstance<PopUpControlViewModel>().PopUpContent = PopUpContents.UserLogin;

            // Getting rid of disgusting warning
            await Task.Delay(1);

        }

        #endregion

    }
}
