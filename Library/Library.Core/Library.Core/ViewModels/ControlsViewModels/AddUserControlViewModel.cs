using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    /// <summary>
    /// View model for the add user control
    /// </summary>
    public class AddUserControlViewModel : BaseViewModel
    {
        #region Public Properties

        // Command to close the add user pop up content
        public ICommand CloseAddUserControl { get; set; }

        /// <summary>
        /// Command to add a user to the database
        /// </summary>
        public ICommand AddUser { get; set; }

        public IEnumerable<UserViewModel> UserList { get; set; }

        /// <summary>
        /// The personal number written by the user
        /// </summary>
        public string PNumber { get; set; }

        /// <summary>
        /// The first name written by the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name written by the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RoleID { get; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddUserControlViewModel()
        {
            // Setting commands
            CloseAddUserControl = new RelayCommand(async () => await CloseAddUserControlCommand());
            AddUser = new RelayParameterizedCommand(async (password) => await AddUserCommand(password));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command to close the pop up content
        /// </summary>
        /// <returns></returns>
        private async Task CloseAddUserControlCommand()
        {
            // Closing the pop up
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }

        /// <summary>
        /// Command to add a user to the database
        /// </summary>
        /// <returns></returns>
        private async Task AddUserCommand(object password)
        {
            // Get all the items from the saved employee list
            UserList = IoC.CreateInstance<EmployeePageViewModel>().EmployeeList;

            // If list hasn't been created before...
            if(UserList == null)
                // Create an instance of a list
                UserList = IoC.CreateList<ObservableCollection<UserViewModel>>();

            // Remove any placeholders
            UserList = UserList.Where(x => x.IsPlaceholder != true).ToList().ToObservableCollection();

            // Adds a temp user
            //(UserList as ObservableCollection<UserViewModel>).Add(new UserViewModel() { personalNumber = PNumber, firstName = FirstName, lastName = LastName });
            if (!await CreateUser(PNumber, FirstName, LastName, RoleID, (password as IHavePassword).SecurePassword))
                return;


            // Fill up any remaining placeholders
            UserList = UserList.FillPlaceHolders().ToObservableCollection();

            // Set the updated list
            IoC.CreateInstance<TableControlViewModel>().CurrentList = UserList;
            IoC.CreateInstance<EmployeePageViewModel>().EmployeeList = UserList;


            // Closes the pop up
            // TODO: Implement confirmation window
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            // Resetting the input properties
            ViewModelHelpers.ResetInputProperties<AddUserControlViewModel>(nameof(PNumber), nameof(FirstName), nameof(LastName));

            await Task.Delay(1);
        }

        /// <summary>
        /// Creates a user and adds it to the system
        /// </summary>
        /// <param name="PersonalNumber"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="RoleID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private async Task<bool> CreateUser(string PersonalNumber,
                                                  string FirstName,
                                                  string LastName,
                                                  int RoleID,
                                                  SecureString Password)
        {
            // Check if provided string is null.
            if (Password == null)
                // If null, throw an exception.
                throw new Exception("Password can not be null");

            // Create a SHA256 hasher.
            var Hasher = SHA256.Create();

            // Where the salt bytes will be.
            var SaltBytes = new byte[16];

            // Create a crypyographic pseudo-random generator.
            using (RandomNumberGenerator RNG = RandomNumberGenerator.Create())
            {
                // Get 16 random bytes and fill the salt byte array.
                RNG.GetBytes(SaltBytes);
            }

            var SaltBase64 = Convert.ToBase64String(SaltBytes);

            /* - Add the salt to the end of the password.
             * - Get the bytes from the salted password.
             * - Hash the password.
             * - Convert it to a Base64 string.
             */
            Password = Convert.ToBase64String(Hasher.ComputeHash(
                                               Encoding.Default.GetBytes(
                                                                          Password.ToUnsecureString() + SaltBase64
                                                                         ))).ToSecureString();
            // Try to add the new user to the system.
            return await IoC.CreateInstance<ApplicationViewModel>().rep.AddUser(PersonalNumber, FirstName, LastName, RoleID, Password, SaltBase64);
        }

        #endregion

    }
}
