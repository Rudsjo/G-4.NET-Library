using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        /// <summary>
        /// List to hold all roles available
        /// </summary>
        public IEnumerable<IRole> CurrentRoles { get; set; } = new ObservableCollection<RoleViewModel>();

        /// <summary>
        /// The personal number written by the user
        /// </summary>
        [DefaultValue("")]
        public string PNumber { get; set; } = String.Empty;

        /// <summary>
        /// The first name written by the user
        /// </summary>
        [DefaultValue("")]
        public string FirstName { get; set; } = String.Empty;

        /// <summary>
        /// The last name written by the user
        /// </summary>
        [DefaultValue("")]
        public string LastName { get; set; } = String.Empty;

        /// <summary>
        /// The role chosen by the user
        /// </summary>
        public IRole CurrentRole { get; set; } = new RoleViewModel();

        /// <summary>
        /// Flag to indicate if the user already exists
        /// </summary>
        public bool IsWrongInput { get; set; }

        /// <summary>
        /// Flag to indicate if the password is wrong
        /// </summary>
        public bool PasswordIsWrong { get; set; }

        /// <summary>
        /// The message to show to a user if they try to add a user and it doesn't work
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// The placeholder to be shown in the combobox before any choice has been made
        /// </summary>
        public string ComboBoxPlaceholder { get; set; } = "Roll";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddUserControlViewModel()
        {
            // Gett all user data
            //IoC.CreateInstance<TableControlViewModel>().LoadItems();
            GetRoles();

            // Setting commands
            CloseAddUserControl = new RelayCommand(async () => await CloseAddUserControlCommand());
            AddUser = new RelayParameterizedCommand(async (password) => await AddUserCommand(password));                       
        }

        #endregion

        #region Private Methods

        private async void GetRoles() { CurrentRoles = await IoC.CreateInstance<ApplicationViewModel>().rep.GetAllRoles(); }


        /// <summary>
        /// Command to close the pop up content
        /// </summary>
        /// <returns></returns>
        private async Task CloseAddUserControlCommand()
        {
            // Closing the pop up
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            // Reset Properties
            ViewModelHelpers.ResetInputProperties<AddUserControlViewModel>(nameof(PNumber), nameof(FirstName), nameof(LastName), nameof(CurrentRole), nameof(ErrorText));
            IsWrongInput = false;
            PasswordIsWrong = false;
            ComboBoxPlaceholder = "Roll";
            CurrentRole = new RoleViewModel();

            await Task.Delay(1);
        }

        /// <summary>
        /// Command to add a user to the database
        /// </summary>
        /// <returns></returns>
        private async Task AddUserCommand(object password)
        {
            // Make sure all warnings are resetted
            IsWrongInput = false;
            PasswordIsWrong = false;

            // Check if all fields are filled
            if (PNumber == null || FirstName == null || LastName == null || CurrentRole.roleID == 0)
            {
                ErrorText = "Alla fält är inte ifyllda korrekt";
                IsWrongInput = true;

                return;
            }

            // Check if the password input is correct while everything else is fine
            if ((password as IHavePassword).SecurePassword.Length < 4)
            {
                // Set the flag
                PasswordIsWrong = true;
                return;
            }

            // Try to add a user
            if (!await CreateUser(PNumber, FirstName, LastName, CurrentRole.roleID, (password as IHavePassword).SecurePassword))
            {
                // If the creation of a user fails, tell the user
                ErrorText = "Något gick fel, \nkontrollera så att användaren inte finns!";

                // Display the text
                IsWrongInput = true;
                
                // And disable the button
                return;
            }

            // Close to reset visable property
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            // Opens up to successfully added
            IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

            await Task.Delay(700);

            // Close popup
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            // Fill the new data
            IoC.CreateInstance<TableControlViewModel>().LoadItems();
 
            // Closes the pop up
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            // Resetting the input properties
            ViewModelHelpers.ResetInputProperties<AddUserControlViewModel>(nameof(PNumber), nameof(FirstName), nameof(LastName), nameof(CurrentRole));
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
