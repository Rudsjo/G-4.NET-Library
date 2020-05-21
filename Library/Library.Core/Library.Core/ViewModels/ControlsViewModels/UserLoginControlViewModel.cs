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

        /// <summary>
        /// The entered personal number from the user
        /// </summary>
        public string PNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [login failed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [login failed]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLoginFailedText { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserLoginControlViewModel()
        {
            // Setting commands
            LoginAsUser = new RelayParameterizedCommand(async (password) => await LoginAsUserCommandAsync(password));
            CloseUserLoginControl = new RelayCommand(async () => await CloseUserLoginControlCommandAsync());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Logging in the user if credentials are correct
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsUserCommandAsync(object password)
        {
            // Hide the textbox in the control
            ShowLoginFailedText = false;

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

                // Check for inputs
                if (PNumber == null || (password as IHavePassword).SecurePassword == null)
                    return;

                // Get a user object from the database
                var loggedInUser = (await LoginHelpers.AttemptLogin(PNumber, (password as IHavePassword).SecurePassword));

                // If no user is returned...
                if (loggedInUser == null)
                {
                    // Show the textbox in the control
                    ShowLoginFailedText = true;
                    return;
                }

                var NewUser = loggedInUser.ToModel<IUser, UserViewModel>();

                // Set the logged in user
                IoC.CreateInstance<ApplicationViewModel>().CurrentUser = NewUser;
                IoC.CreateInstance<ApplicationViewModel>().SetCurrentUserRole();

                // If the login is made from the start screen we go to the book page
                if (IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.MainPage)
                    IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);

                // Close the login popup control
                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

                // Reset input properties
                ViewModelHelpers.ResetInputProperties<UserLoginControlViewModel>(nameof(PNumber));
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
            // Hide the text
            ShowLoginFailedText = false;

            // Closes the popup control
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }

        #endregion
    }
}
