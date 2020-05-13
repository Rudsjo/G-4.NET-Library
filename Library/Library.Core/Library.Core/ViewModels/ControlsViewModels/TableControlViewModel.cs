using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    /// <summary>
    /// View model for the main table control
    /// </summary>
    public class TableControlViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// Command to sort a specific column
        /// </summary>
        public ICommand Sort { get; set; }

        /// <summary>
        /// Command to delete an object
        /// </summary>
        public ICommand Delete { get; set; }

        /// <summary>
        /// State of which table to sort
        /// </summary>
        public SortableTables TableToSort { get; set; }

        /// <summary>
        /// The original list displayed in the table
        /// </summary>
        public IEnumerable CurrentList { get; set; }

        /// <summary>
        /// The headers of the table
        /// </summary>
        public string[] TableHeaderTexts { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TableControlViewModel()
        {
            // Setting commands
            Sort = new RelayParameterizedCommand(SortCommand);
            Delete = new RelayParameterizedCommand(async (parameter) => await DeleteCommand(parameter));
        }


        #endregion

        #region Private Methods
        /// <summary>
        /// Command to delete
        /// </summary>
        /// <param name="itemToDelete">The item to delete</param>
        private async Task DeleteCommand(object itemToDelete)
        {
            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                case ApplicationPages.BookPage:
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Confirmation);
                    IoC.CreateInstance<ConfirmationControlViewModel>().ArticleToDelete = (itemToDelete as IArticle).articleID;
                    break;

                case ApplicationPages.EmployeePage:
                    {
                        if (await CanDeleteUser((itemToDelete as UserViewModel).personalNumber))
                        {
                            IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Confirmation);
                            IoC.CreateInstance<ConfirmationControlViewModel>().UserToDelete = (itemToDelete as IUser).personalNumber;
                        }
                        break;
                    }
            }
        }

                /// <summary>
        /// Determines if a user can be deleted.
        /// </summary>
        /// <param name="PersonalNumber">The personal number.</param>
        /// <returns>
        ///   <c>true</c> if collection contains no articles; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> CanDeleteUser(string PersonalNumber)
        =>
        (await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserLoans(PersonalNumber)).ToList().Count() == 0;

        /// <summary>
        /// Sorts the list depending on which buttons is pressed
        /// </summary>
        /// <param name="table">The table to sort</param>
        private void SortCommand(object table)
        {
            switch ((string)table)
            {
                case "PNumber":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.PersonalNumber;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<UserViewModel>).SortByPropertyName(nameof(UserViewModel.personalNumber));

                        break;
                    }

                case "FName":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.FirstName;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<UserViewModel>).SortByPropertyName(nameof(UserViewModel.firstName));

                        break;
                    }

                case "LName":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.LastName;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<UserViewModel>).SortByPropertyName(nameof(UserViewModel.lastName));

                        break;
                    }

                case "LArticles":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.LoanedArticles;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<UserViewModel>).SortByPropertyName(nameof(UserViewModel.loanedArticles));

                        break;
                    }

                case "RArticles":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.ReservedArticles;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<UserViewModel>).SortByPropertyName(nameof(UserViewModel.reservedArticles));

                        break;
                    }

                case "Title":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Title;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.title));

                        break;
                    }

                case "Author":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Author;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.author));

                        break;
                    }

                case "Edition":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Edition;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.edition));

                        break;
                    }

                case "Availability":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Availability;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.availability));

                        break;
                    }

                case "Placement":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Placement;

                        // Return the ordered list with placeholders
                        CurrentList = (CurrentList as ObservableCollection<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.placement));

                        break;
                    }

            }
        }

        #endregion
    }
}
