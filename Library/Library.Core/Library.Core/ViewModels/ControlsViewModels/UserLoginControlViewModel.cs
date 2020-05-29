using Library.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            CloseUserLoginControl = new RelayCommand(CloseUserLoginControlCommand);
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

                await CheckNotifications();

                // If the login is made from the start screen we go to the book page
                if (IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.MainPage)
                    IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);

                // Closes the popup if we're not in firstload-mode
                if (!IoC.CreateInstance<ApplicationViewModel>().IsLoading && IoC.CreateInstance<ApplicationViewModel>().CurrentPage != ApplicationPages.MainPage)
                    IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

                IoC.CreateInstance<MyProfileControlViewModel>().GetMyLoansAndReservations();

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
        public void CloseUserLoginControlCommand()
        {
            // Hide the text
            ShowLoginFailedText = false;

            // Closes the popup control
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
        }

        ///// <summary>
        ///// Method to check if a user has any reservations available for loan
        ///// </summary>
        ///// <returns></returns>
        //public async Task CheckUserForNotifications()
        //{
        //    //Create a new list to hold all reservations avaliable for loan
        //    IoC.CreateInstance<MainContentUserControlViewModel>().NotificationList = new ObservableCollection<Reservation>();

        //    //get all the reservations in database
        //    var allReservations = await IoC.CreateInstance<ApplicationViewModel>().rep.GetAllReservations();

        //    //get user specific reservations
        //    var userReservations = await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserReservations(IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber);

        //    //Go trough every reservation in user reservation
        //    foreach (var reservation in userReservations)
        //    {

        //        //gets a result of the first occuring sequence in database, list goes backward?!
        //        // || x.userID == IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber, this section was removed for trying out, seems to work
        //        var result = allReservations.Where(x => x.articleID == reservation.articleID && x.userID == IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber).First();

        //        //if user from result is the same as current user, and article has status 4, the notification enables
        //        if (reservation.statusID == 4)
        //        {
        //            IoC.CreateInstance<ApplicationViewModel>().CurrentUser.NumberOfNotifications++;
        //            //Change notification
        //            IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.Notification;

        //            //Adding the reservation avaliable for loan to notificationlist and displaying it
        //            //IoC.CreateInstance<MainContentUserControlViewModel>().NotificationList.Add(reservation as Reservation);
        //        }
        //    }
        //}


        public async Task CheckNotifications()
        {
            //Create a new list to hold all reservations avaliable for loan
            IoC.CreateInstance<MainContentUserControlViewModel>().NotificationList = new ObservableCollection<Reservation>();

            //get all the reservations in database
            var allReservations = await IoC.CreateInstance<ApplicationViewModel>().rep.GetAllReservations();

            var duplicates = allReservations.GroupBy(x => x.articleID)
                                            .Where(g => g.Count() > 1)
                                            .Select(g => g.Key)
                                            .ToList();

            foreach (var item in allReservations)
            {
                if (item.userID == IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber)
                {
                    if (!duplicates.Contains(item.articleID) && item.statusID == 4)
                    {
                        //Change notification
                        IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.Notification;

                        //Adding the reservation avaliable for loan to notificationlist and displaying it
                        IoC.CreateInstance<MainContentUserControlViewModel>().NotificationList.Add(item);
                    }

                    else if (duplicates.Contains(item.articleID))
                    {
                        var first = allReservations
                                   .Where(x => x.articleID == item.articleID && item.userID == IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber)
                                   .ToList()
                                   .First();

                        if(first.userID == IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber && item.statusID == 4)
                        {
                            //Change notification
                            IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.Notification;

                            //Adding the reservation avaliable for loan to notificationlist and displaying it
                            IoC.CreateInstance<MainContentUserControlViewModel>().NotificationList.Add(item);
                        }
                    }
                    
                }
            }

        }
        #endregion
    }
}
