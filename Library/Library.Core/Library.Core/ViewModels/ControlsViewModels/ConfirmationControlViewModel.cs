using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    /// <summary>
    /// The view model for the confirmation control
    /// </summary>
    public class ConfirmationControlViewModel : BaseViewModel
    {

        #region Public Properties
        /// <summary>
        /// Command to confirm
        /// </summary>
        public ICommand Confirm { get; set; }

        /// <summary>
        /// Command to abort
        /// </summary>
        public ICommand Abort { get; set; }

        /// <summary>
        /// The personal number of the user to delete
        /// </summary>
        public string UserToDelete { get; set; }

        /// <summary>
        /// The id of the article to delete
        /// </summary>
        public int ArticleToDelete { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfirmationControlViewModel()
        {
            // Setting command
            Confirm = new RelayCommand(async () => await ConfirmCommand());
            Abort = new RelayCommand(async () => await AbortCommand());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command to confirm the action, ex. a deletion
        /// </summary>
        /// <returns></returns>
        private async Task ConfirmCommand()
        {
            // Check the current page
            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                // Book page
                case ApplicationPages.BookPage:
                    // Deletes the book
                    // TODO: Koppla mot databas
                    IoC.CreateInstance<BookPageViewModel>().BookList.Clear();
                    break;

                // Employee page
                case ApplicationPages.EmployeePage:
                    {
                        // Deletes the user
                        await IoC.CreateInstance<ApplicationViewModel>().rep.DeleteUser(UserToDelete);
                        break;
                    }

                default:
                    break;
            }

            // Fill up the new user data
            await IoC.CreateInstance<EmployeePageViewModel>().FillUserData();

            // Close the pop up
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
        }

        /// <summary>
        /// Command to abort an action
        /// </summary>
        /// <returns></returns>
        private async Task AbortCommand()
        {
            // Closes the pop up
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }

        #endregion
    }
}
