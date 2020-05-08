using System;
using System.Collections.Generic;
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


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddUserControlViewModel()
        {
            // Setting commands
            CloseAddUserControl = new RelayCommand(async () => await CloseAddUserControlCommand());
            AddUser = new RelayCommand(async () => await AddUserCommand());
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
        private async Task AddUserCommand()
        {
            // Adds a temp user
            // REMOVE AND REPLACE WITH REAL ADD FUNCTION
            IoC.CreateInstance<TableControlViewModel>().TempList.Add
                (
                    new User() { personalNumber = PNumber, firstName = FirstName, lastName = LastName }
                );

            // Closes the pop up
            // TODO: Implement confirmation window
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }

        #endregion

    }
}
