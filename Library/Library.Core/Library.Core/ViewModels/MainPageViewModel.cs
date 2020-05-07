using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    /// <summary>
    /// View model for the main page of the application
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Command to let the user access the table with all books in the system
        /// </summary>
        public ICommand ShowAllBooks { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPageViewModel()
        {
            // Make sure that everytime this page loads there is nobody logged in
            IoC.CreateInstance<ApplicationViewModel>().SetCurrentUserRole(UserTypes.Visitor);


            // Setting commands
            ShowAllBooks = new RelayCommand(async () => await ShowAllBooksCommandAsync());

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The action ro run when the <see cref="ShowAllBooks"/> command is activated
        /// </summary>
        /// <returns></returns>
        private async Task ShowAllBooksCommandAsync()
        {
            // Changing the page
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);

            await Task.Delay(1);
        }

        #endregion


    }
}
