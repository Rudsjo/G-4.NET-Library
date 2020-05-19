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
        /// Command property to update
        /// </summary>
        public ICommand Update { get; set; }

        /// <summary>
        /// Command property for ExitInfoCommand
        /// </summary>
        public ICommand Exit { get; set; }

        /// <summary>
        /// Command to open infopopup on articles
        /// </summary>
        public ICommand Info { get; set; }

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
        public IEnumerable<object> CurrentList { get; set; }

        /// <summary>
        /// The headers of the table
        /// </summary>
        public string[] TableHeaderTexts { get; set; }

        /// <summary>
        /// CurrentArticle holds all properties for the article displayed in info popup
        /// </summary>
        public IArticle CurrentArticle { get; set; }


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
            Info = new RelayParameterizedCommand(async (parameter) => await InfoCommand(parameter));
            Exit = new RelayCommand(async () => await ExitInfoCommand());
            Update = new RelayCommand(async () => await UpdateCommand());
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to fill a local list with data from the database
        /// </summary>
        public async void LoadItems()
        {
            // Set the flag to indicate that we're loading data
            IoC.CreateInstance<ApplicationViewModel>().IsLoading = true;

            // Set the content in the table
            if (IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.BookPage)
                CurrentList = IoC.CreateInstance<MainContentUserControlViewModel>().ArticleSearchList
                    = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchArticles()).ToModelDataToViewModel<IArticle, ArticleViewModel>().FillPlaceHolders();
            else
                CurrentList = IoC.CreateInstance<MainContentUserControlViewModel>().UserSearchList
                    = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchUsers()).ToModelDataToViewModel<IUser, UserViewModel>().FillPlaceHolders();

            await Task.Delay(1500);

            // When data is loaded, set the flag
            IoC.CreateInstance<ApplicationViewModel>().IsLoading = false;
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
                    {
                        IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Confirmation);
                        IoC.CreateInstance<ConfirmationControlViewModel>().ArticleToDelete = (CurrentArticle as IArticle).articleID;
                        break;
                    }

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
                        CurrentList =
                            (CurrentList as IEnumerable<UserViewModel>).SortByPropertyName(nameof(UserViewModel.personalNumber))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "FName":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.FirstName;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<UserViewModel>).SortByPropertyName(nameof(UserViewModel.firstName))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "LName":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.LastName;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<UserViewModel>).SortByPropertyName(nameof(UserViewModel.lastName))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "LArticles":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.LoanedArticles;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<UserViewModel>).SortByPropertyName(nameof(UserViewModel.loanedArticles))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "RArticles":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.ReservedArticles;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<UserViewModel>).SortByPropertyName(nameof(UserViewModel.reservedArticles))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "Title":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Title;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.title))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "Author":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Author;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.author))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "Edition":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Edition;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.edition))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "Availability":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Availability;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.availability))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

                case "Placement":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.Placement;

                        // Return the ordered list with placeholders
                        CurrentList =
                            (CurrentList as IEnumerable<ArticleViewModel>).SortByPropertyName(nameof(ArticleViewModel.placement))
                            .Where(x => x.IsPlaceholder == false).FillPlaceHolders().ToList().ToObservableCollection();

                        break;
                    }

            }
        }

        /// <summary>
        /// Opens up the info popup and sets the value to article displayed
        /// </summary>
        /// <param name="itemToInspect">the chosen article</param>
        /// <returns></returns>
        private async Task InfoCommand(object itemToInspect)
        {
            //Kan den här ligga underst? Prova
            CurrentArticle = (itemToInspect as IArticle);
            
            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID)
            {
                //Opens a popup to editscreen
                case 1:
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Edit);
                    break;

                //Opens a popup to editscreen
                case 2:
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Edit);
                    break;

                case 3:
                    //Opens a popup to infoscreen
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Info);
                    break;

                case 4:
                    //Opens a popup to infoscreen
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Info);
                    break;

                    //Default. Closes a popup, should there be any
                default:
                    IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
                    break;
            }

            await Task.Delay(1);
        }

        /// <summary>
        /// Command that exits the info popup
        /// </summary>
        /// <returns></returns>
        private async Task ExitInfoCommand()
        {
            //Closes the popup
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            await Task.Delay(1);
        }

        /// <summary>
        /// Command that updates an article in the database
        /// </summary>
        /// <returns></returns>
        private async Task UpdateCommand()
        {
            //Tries to edit and add the newly updated article to the database
            if (await IoC.CreateInstance<ApplicationViewModel>().rep.UpdateArticle(CurrentArticle.articleID, CurrentArticle.ToModel<IArticle, Article>()))
            {
                //If successful a confirmationtoken appears
                IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                await Task.Delay(700);

                //Close confirmationtoken
                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
            }
        }
        #endregion
    }
}
