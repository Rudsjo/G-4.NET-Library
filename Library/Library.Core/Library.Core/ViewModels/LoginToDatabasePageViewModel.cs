﻿
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class LoginToDatabasePageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Property of the command to check and log in to database
        /// </summary>
        public ICommand LoginToDatabase { get; set; }

        /// <summary>
        /// A flag indicating if the <see cref="LoginToDatabase"/> command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginToDatabasePageViewModel()
        {
            // Setting the commands
            LoginToDatabase = new RelayCommand(async () => await LoginToDatabaseCommandAsync());
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Checks the database login and logs the user in if successful
        /// </summary>
        /// <returns></returns>
        public async Task LoginToDatabaseCommandAsync()
        {
            // Checking if the login is already running, used to avoid overload
            if (LoginIsRunning)
                return;

            // Indicate that the login is running
            LoginIsRunning = true;

            // The actual login trial
            try
            {
                // Temporarily used waiter to simulate animation
                await Task.Delay(500);

                // Calling a new instance of the Application view model and changes the current page
                IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);
            }

            finally
            {
                // When the login is done, no matter if it succeeds or not, the flag will be set to false
                LoginIsRunning = false;
            }
        } 

        #endregion

    }
}
