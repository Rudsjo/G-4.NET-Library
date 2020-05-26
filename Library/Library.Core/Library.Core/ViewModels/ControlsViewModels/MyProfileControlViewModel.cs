using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class MyProfileControlViewModel : BaseViewModel
    {
        /// <summary>
        /// Command to log out a user
        /// </summary>
        public ICommand Logout { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MyProfileControlViewModel()
        {
            // Setting commands
            Logout = new RelayCommand(async () => await LogoutCommand());
        }


        /// <summary>
        /// The command to logout a user
        /// </summary>
        /// <returns></returns>
        private async Task LogoutCommand()
        {
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.MainPage);

            await Task.Delay(1);
        }

    }
}
