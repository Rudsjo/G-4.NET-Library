﻿using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        /// Command property to unblock a user
        /// </summary>
        public ICommand Unblock { get; set; }
        /// <summary>
        /// Command property to block a user
        /// </summary>
        public ICommand Block { get; set; }

        /// <summary>
        /// Command propery to return an article
        /// </summary>
        public ICommand Return { get; set; }

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
        /// Command to loan an article
        /// </summary>
        public ICommand LoanedArticle { get; set; }

        /// <summary>
        /// Command to reserve an article
        /// </summary>
        public ICommand ReserveArticle { get; set; }

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

        /// <summary>
        /// CurrentUser holds all properties for the user displayed in info/edit popup including loans and reservations
        /// </summary>
        private IUser _selectedUser;
        public IUser SelectedUser
        {
            get => _selectedUser;

            set
            {
                _selectedUser = value;
                GetUserInfo();
            }
        }

        /// <summary>
        /// List of articles that a certain user has
        /// </summary>
        public IEnumerable<ArticleViewModel> CurrentUserLoans { get; set; }

        /// <summary>
        /// List of articles that a user has reserved
        /// </summary>
        public IEnumerable<ArticleViewModel> CurrentUserReservations { get; set; }

        /// <summary>
        /// List of articles that a user has reserved, used in the ReserveArticleCommand
        /// </summary>
        public IEnumerable<object> CurrentUserReservationsList { get; set; }

        /// <summary>
        /// List of articles that a user has loaned, used in the ReserveArticleCommand
        /// </summary>
        public IEnumerable<object> CurrentUserLoansList { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TableControlViewModel()
        {
            // Setting commands
            Sort   = new RelayParameterizedCommand(SortCommand);
            Delete = new RelayParameterizedCommand(async (parameter) => await DeleteCommand(parameter));
            Info   = new RelayParameterizedCommand(async (parameter) => await InfoCommand(parameter));
            Exit   = new RelayCommand(async () => await ExitInfoCommand());
            Update = new RelayCommand(async () => await UpdateCommand());

            Return  = new RelayParameterizedCommand(async (parameter) => await ReturnArticleCommand(parameter));
            Block   = new RelayCommand(async () => await BlockUserCommand());
            Unblock = new RelayCommand(async () => await UnblockUserCommand());

            LoanedArticle  = new RelayParameterizedCommand(async (parameter) => await LoanedArticleCommand(parameter));
            ReserveArticle = new RelayParameterizedCommand(async (parameter) => await ReserveArticleCommand(parameter));
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

        /// <summary>
        /// Updates the articles statuses for the current user
        /// </summary>
        /// <returns></returns>
        public async Task UpdateArticleStatuses()
        {
            // If the user is'nt logged in
            if (IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID == 4) return;

            // Get all loan ID's from the current user
            var CurrentUserLoans = (await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserLoans(
                                          IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber)).Select(a => a.articleID).ToList();

            // Go through all articles
            for (int i = 0; i < CurrentList.Count(); i++)
            {
                // If the user currently has this article
                if (CurrentUserLoans.ToList().Contains((CurrentList as IEnumerable<ArticleViewModel>).ToList()[i].articleID))
                    // Hide the reserve button
                    (CurrentList as IEnumerable<ArticleViewModel>).ToList()[i].IsLoanedByCurrentUser = true;
                // Else
                else
                    // Show either the Loan or the Reserve button
                    (CurrentList as IEnumerable<ArticleViewModel>).ToList()[i].IsLoanedByCurrentUser = false;
            }
        }

        /// <summary>
        /// Method that returns a list of the articles that a user has loaned and reserved
        /// </summary>
        public async void GetUserInfo()
        {
            //Gets all the loans on a current user
            CurrentUserLoans = (await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserLoans(SelectedUser.personalNumber)).ToModelDataToViewModel<IArticle, ArticleViewModel>().ToObservableCollection().FillPlaceHolders(3);

            //Gets all the reservations on a current user
            CurrentUserReservations = (await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserReservations(SelectedUser.personalNumber)).ToModelDataToViewModel<IArticle, ArticleViewModel>().ToObservableCollection().FillPlaceHolders(3);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Command to loan an article if the article is available
        /// </summary>
        /// <param name="testing"></param>
        /// <returns></returns>
        private async Task LoanedArticleCommand(object testing)
        {
            string pn = IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber;
            CurrentArticle = (testing as IArticle);            

            // This sets the availability to Loaned, status = 2
            if (await IoC.CreateInstance<ApplicationViewModel>().rep.LoanArticle(pn, CurrentArticle.articleID))
            {
                // If it's available, the User can loan an article
                IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                await Task.Delay(700);

                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
                
                //Refreshes the list of articles when user successfully loans an article
                LoadItems();

            }
            // Returns false if the Article is already loaned
            else
                return;
        }

        /// <summary>
        /// When a user presses the Reserve button
        /// </summary>
        /// <param name="chosenArticle"></param>
        /// <returns></returns>
        private async Task ReserveArticleCommand(object chosenArticle)
        {
            CurrentArticle = (chosenArticle as IArticle);
            string pn = IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber;
            CurrentUserLoansList = await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserLoans(pn);
            CurrentUserReservationsList = await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserReservations(pn);
            

            string dateTime = "2000-01-01";

            //Checks to see if article is already loaned or reserved by user.
            bool AlreadyReservedArticleCheck = false;

            foreach (IArticle article in CurrentUserLoansList)
            {

                if (article.articleID == CurrentArticle.articleID)
                {
                    AlreadyReservedArticleCheck = true;
                }
            }
            foreach (IArticle article in CurrentUserReservationsList)
            {

                if (article.articleID == CurrentArticle.articleID)
                {
                    AlreadyReservedArticleCheck = true;
                }
            }
            

            if (AlreadyReservedArticleCheck == false)
            {
                // If it's available, the User can reserve an article
                if (await IoC.CreateInstance<ApplicationViewModel>().rep.ReserveArticle(SelectedUser.personalNumber, CurrentArticle.articleID, dateTime))
                {
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                    await Task.Delay(700);

                    IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

                    //Refreshes the list of articles when user successfully loans an article
                    LoadItems();
                }
            }
        }

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
                        IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Confirmation);
                        IoC.CreateInstance<ConfirmationControlViewModel>().ArticleToDelete = (CurrentArticle as IArticle).articleID;
                        break;
                    }

                case ApplicationPages.EmployeePage:
                    {
                        if (await CanDeleteUser((SelectedUser as UserViewModel).personalNumber))
                        {
                            IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Confirmation);
                            IoC.CreateInstance<ConfirmationControlViewModel>().UserToDelete = (SelectedUser as IUser).personalNumber;
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
        /// <summary>
        /// Opens up the info popup and sets the value to article displayed
        /// </summary>
        /// <param name="itemToInspect">the chosen article</param>
        /// <returns></returns>


        private async Task InfoCommand(object itemToInspect)
        {

            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                case ApplicationPages.BookPage:

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

                        default:

                            break;
                    }
                    break;

                case ApplicationPages.EmployeePage:

                    SelectedUser = (itemToInspect as IUser);
                    (SelectedUser as UserViewModel).IsBlocked = (await IoC.CreateInstance<ApplicationViewModel>().rep.IsUserBlocked((SelectedUser as IUser).personalNumber));

                    //This needs further restrictions to get correct popup for each user
                    switch (IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID)
                    {
                        case 1:
                            //Opens a popup to userinfo, if administrator, delete librarian is active
                            IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.UserInfo);
                            break;

                        case 2:
                            //Opens a popup to userinfo, if librarian, delete librarian is inactive
                            IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.UserInfo);
                            break;

                        default:

                            break;
                    }
                    break;

                default:

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
            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                //In case of currentPage = Bookpage, make sure article is updated
                case ApplicationPages.BookPage:

                    //Tries to edit and add the newly updated article to the database
                    if (await IoC.CreateInstance<ApplicationViewModel>().rep.UpdateArticle(CurrentArticle.articleID, CurrentArticle.ToModel<IArticle, Article>()))
                    {
                        //If successful a confirmation appears
                        IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                        await Task.Delay(700);

                        //Close confirmation
                        IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
                    }
                    break;

                //In case of currentPage = Employee, make sure user is updated
                case ApplicationPages.EmployeePage:

                    //Tries to edit and add the newly updated user to the database
                    if (await IoC.CreateInstance<ApplicationViewModel>().rep.UpdateUser(SelectedUser.personalNumber, SelectedUser.ToModel<IUser, User>()))
                    {
                        //If successful a confirmation appears
                        IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                        await Task.Delay(700);

                        //Close confirmation
                        IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
                    }
                    break;

                //Default. Closes a popup, should there be any
                default:
                    IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
                    break;
            }

        }


        /// <summary>
        /// Command that selects an item from a user and "returns" it to the library
        /// </summary>
        /// <param name="itemToReturn">the item to be returned or deleted depending on bool result</param>
        /// <returns></returns>
        private async Task ReturnArticleCommand(object itemToReturn)
        {
            //Checks to se if the result is true
            if (await IoC.CreateInstance<ApplicationViewModel>().rep.ReturnArticle((itemToReturn as ArticleViewModel).articleID))
            {
                //If true, item gets returned to the library and success confirmation pops up
                IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                await Task.Delay(700);

                //Close popup
                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

                //Reset the loans to ready it for next popup
                CurrentUserLoans = null;

                //Load the items to refresh the userlist
                LoadItems();
            }

            //Checks to se if the result is true
            else if (await IoC.CreateInstance<ApplicationViewModel>().rep.DeleteReservation((itemToReturn as ArticleViewModel).articleID))
            {
                //If true, item gets removed from the reservationslist and success confirmation pops up
                IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Success);

                await Task.Delay(700);

                //Close popup
                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

                //Reset the loans to ready it for next popup
                CurrentUserReservations = null;

                //Load the items to refresh the userlist
                LoadItems();
            }
        }

        /// <summary>
        /// Command to block a user from using the library, different reasons will be given
        /// </summary>
        /// <returns></returns>
        private async Task BlockUserCommand()
        {
            //Checks to se if the user is already blocked
            if (!(await IoC.CreateInstance<ApplicationViewModel>().rep.IsUserBlocked((SelectedUser as IUser).personalNumber)))
            {
                //If true, its sets the user in ConfirmationViewModel, and confirms a block with a reason.
                IoC.CreateInstance<ConfirmationControlViewModel>().UserToBlock = ((SelectedUser as IUser).personalNumber);

                //Popup for the confirmationwindow
                IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Confirmation);

                //Sets the bool IsBlocked so that the view updates and shows user is blocked
                (SelectedUser as UserViewModel).IsBlocked = (await IoC.CreateInstance<ApplicationViewModel>().rep.IsUserBlocked((SelectedUser as IUser).personalNumber));
            }
        }

        /// <summary>
        /// Command to unblock the user from the system
        /// </summary>
        /// <returns></returns>
        private async Task UnblockUserCommand()
        {
            //Checks to se if the user is blocked
            if (await IoC.CreateInstance<ApplicationViewModel>().rep.IsUserBlocked((SelectedUser as IUser).personalNumber))
            {
                //if true, it unblocks the user
                await IoC.CreateInstance<ApplicationViewModel>().rep.UnblockUser((SelectedUser as IUser).personalNumber);

                //popup for success
                IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Success);

                await Task.Delay(700);

                //Close popup
                IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();

                //Sets the bool IsBlocked so that the view updates and shows user is not blocked
                (SelectedUser as UserViewModel).IsBlocked = (await IoC.CreateInstance<ApplicationViewModel>().rep.IsUserBlocked((SelectedUser as IUser).personalNumber));
            }
        }
        #endregion
    }
}
