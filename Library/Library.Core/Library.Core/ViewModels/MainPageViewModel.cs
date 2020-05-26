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

        /// <summary>
        /// Command to activate a search through the system
        /// </summary>
        public ICommand FirstSearch { get; set; }

        /// <summary>
        /// The search text on the start page
        /// </summary>
        public string FirstSearchText { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPageViewModel()
        {
            // Make sure nobody is logged in
            IoC.CreateInstance<ApplicationViewModel>().ResetCurrentUser();
            IoC.CreateInstance<ApplicationViewModel>().SetCurrentUserRole();

            // Reset the sorting
            IoC.CreateInstance<TableControlViewModel>().TableToSort = SortableTables.None;


            // Setting commands
            ShowAllBooks = new RelayCommand(ShowAllBooksCommand);


            FirstSearch = new RelayCommand(test);
        }

        void test()
        {
            // Changing the page
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);

            // Setting the search text
            IoC.CreateInstance<MainContentUserControlViewModel>().SearchText = FirstSearchText;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The action ro run when the <see cref="ShowAllBooks"/> command is activated
        /// </summary>
        /// <returns></returns>
        private void ShowAllBooksCommand()
        {
            // Changing the page
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.BookPage);
        }

        #endregion
    }
}
