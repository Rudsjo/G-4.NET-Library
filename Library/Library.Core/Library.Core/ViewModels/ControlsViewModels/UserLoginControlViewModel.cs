using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class UserLoginControlViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Command to log in a user
        /// </summary>
        public ICommand LoginAsUser { get; set; }

        /// <summary>
        /// Command to close the login control
        /// </summary>
        public ICommand CloseUserLoginControl { get; set; }

        /// <summary>
        /// Flag to indicate if a user login is running
        /// </summary>
        public bool UserLoginIsRunning { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserLoginControlViewModel()
        {
            // Setting commands
            LoginAsUser = new RelayCommand(async () => await LoginAsUserCommandAsync());
            CloseUserLoginControl = new RelayCommand(async () => await CloseUserLoginControlCommandAsync());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Logging in the user if credentials are correct
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsUserCommandAsync()
        {
            // Checking if the login is already running, used to avoid overload
            if (UserLoginIsRunning)
                return;

            // Indicate that the login is running
            UserLoginIsRunning = true;

            // The actual login trial
            try
            {
                // Temporarily used waiter to simulate animation
                await Task.Delay(500);

                // Change the current role of the user
                IoC.CreateInstance<ApplicationViewModel>().SetCurrentUserRole(UserTypes.User);

                // Close the login popup control
                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            }

            finally
            {
                // When the login is done, no matter if it succeeds or not, the flag will be set to false
                UserLoginIsRunning = false;
            }
        }

        /// <summary>
        /// Closes the User login control
        /// </summary>
        /// <returns></returns>
        public async Task CloseUserLoginControlCommandAsync()
        {
            // Closes the popup control
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }

        #endregion
    }
}
