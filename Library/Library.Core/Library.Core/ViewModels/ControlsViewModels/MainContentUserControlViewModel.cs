using Library.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using System.Xml;

namespace Library.Core
{
    public class MainContentUserControlViewModel : BaseViewModel
    {
        public ICommand ReturnReservation { get; set; }
        public ICommand LoanReservation { get; set; }
        public ObservableCollection<Reservation> NotificationList { get; set; }

        #region Public Properties

        /// <summary>
        /// The actual list of article to search from
        /// </summary>
        public IEnumerable<ArticleViewModel> ArticleSearchList { get; set; }

        /// <summary>
        /// The actual list of users to search from
        /// </summary>
        public IEnumerable<UserViewModel> UserSearchList { get; set; }

        /// <summary>
        /// Sets the color of the Filter button
        /// </summary>
        public FilterColors FilterColor { get; set; }

        /// <summary>
        /// Sets the color of the Notification button
        /// </summary>
        public NotificationColors NotificationColor { get; set; }

        /// <summary>
        /// The command to open MyProfile popup,
        /// if user is not logged in the login pop up will be displayed
        /// </summary>
        public ICommand MyProfile { get; set; }

        /// <summary>
        /// The command to add 
        /// </summary>
        public ICommand OpenAdd { get; set; }

        /// <summary>
        ///The command to open Filterpopup 
        /// </summary>
        public ICommand OpenFilter { get; set; }

        /// <summary>
        /// The command to exit the filterpopup
        /// </summary>
        public ICommand ExitFilter { get; set; }

        /// <summary>
        /// Command to open notification popup
        /// </summary>
        public ICommand OpenNotification { get; set; }

        /// <summary>
        /// Command to close the notification popup
        /// </summary>
        public ICommand ExitNotification { get; set; }

        /// <summary>
        /// Command to show all removed articles in the system
        /// </summary>
        public ICommand ShowRemovedArticles { get; set; }

        /// <summary>
        /// Command for clearing all filters
        /// </summary>
        public ICommand ClearFilters { get; set; }

        /// <summary>
        /// Flag to indicate if removed articles are shown
        /// </summary>
        public bool IsShowingRemovedArticles { get; set; } = false;

        /// <summary>
        /// The text to show in the header above the table
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// The text to show in the add button
        /// </summary>
        public string AddButtonText { get; set; }

        public IEnumerable<object> CurrentList { get; set; }

        /// <summary>
        /// Private backingfield for search
        /// </summary>
        private string _searchText;

        /// <summary>
        /// Fullprop that uses the searchmethod to get the sorted articlelist
        /// </summary>
        public string SearchText 
        {
            get { return _searchText; }

            set
            {
                if (value == null)
                {
                    return;
                }

                _searchText = value;

                // Search in realtime for articles and display them for the user
                SearchUpdate();          
            }
        }

        /// <summary>
        /// The filter button text content
        /// </summary>
        public string FilterText { get; set; } = "Filter";

        /// <summary>
        /// Counter for how many filters are checked in the filterpopup
        /// </summary>
        public int FilterCheckboxCounter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show clear filter button].
        /// Should only show this button if any filters are applied.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show clear filter button]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowClearFilterButton { get; set; }

        #region Bools for filter popup

        /// <summary>
        /// Bool for the title-checkbox in filterpopup
        /// </summary>
        private bool _titleFilter;
        public bool TitleFilter
        {
            get { return _titleFilter; }
            set
            {
                if (_titleFilter == value)
                    return;

                if (value == true)
                    FilterCheckboxCounter++;
                else
                    FilterCheckboxCounter--;

                _titleFilter = value;

            }
        }
        /// <summary>
        /// Bool for the author-checkbox in filterpopup
        /// </summary>
        private bool _authorFilter;
        public bool AuthorFilter
        {
            get { return _authorFilter; }
            set
            {
                if (_authorFilter == value)
                    return;

                if (value == true)
                    FilterCheckboxCounter++;
                else
                    FilterCheckboxCounter--;

                _authorFilter = value;



            }
        }
        /// <summary>
        /// Bool for the ISBN-checkbox in filterpopup
        /// </summary>
        private bool _isbnFilter;
        public bool IsbnFilter
        {
            get { return _isbnFilter; }
            set
            {
                if (_isbnFilter == value)
                    return;


                if (value == true)
                    FilterCheckboxCounter++;
                else
                    FilterCheckboxCounter--;

                _isbnFilter = value;



            }
        }
        /// <summary>
        /// Bool for the EBook-checkbox in filterpopup
        /// </summary>
        private bool _eBookFilter;
        public bool EBookFilter
        {
            get { return _eBookFilter; }
            set
            {
                if (_eBookFilter == value)
                    return;

                if (value == true)
                    FilterCheckboxCounter++;
                else
                    FilterCheckboxCounter--;

                _eBookFilter = value;
            }
        }

        /// <summary>
        /// Bool for changing the color if any filters is checked in the filterpopup
        /// </summary>
        private bool _changeFilterColor;
        public bool ChangeFilterColor
        {
            get { return _changeFilterColor; }
            set
            {
                if (_changeFilterColor == value)
                    return;

                _changeFilterColor = value;

            }
        }


        #endregion

        #region Properties for return multiple articles
        /// <summary>
        /// Command that lets the user undo an add
        /// </summary>
        public ICommand RemoveFromList { get; set; }

        /// <summary>
        /// Command to add an article to the list
        /// </summary>
        public ICommand AddToList { get; set; }

        /// <summary>
        /// Extra command to close the Returnarticle popup
        /// </summary>
        public ICommand Close { get; set; }

        /// <summary>
        /// Command to run the return-function that returns all books to the library
        /// </summary>
        public ICommand Return { get; set; }

        /// <summary>
        /// Command to open the popup for return article
        /// </summary>
        public ICommand OpenReturn { get; set; }

        /// <summary>
        /// The temporary list that holds all items to be returned
        /// </summary>
        public ObservableCollection<ArticleViewModel> ListOfArticlesToReturn { get; set; }

        /// <summary>
        /// A specified article to return
        /// </summary>
        public ArticleViewModel ArticleToReturn { get; set; }

        /// <summary>
        /// Input to find the specified article in the database and display it with name and id
        /// </summary>
        public string ArticleID { get; set; }

        /// <summary>
        /// Bool to show errormessage if the article could not be added. message - "something went wrong"
        /// </summary>
        public bool ArticleDoesNotExists { get; set; }

        #endregion

        #endregion


        #region Contructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainContentUserControlViewModel()
        {

            // Setting commands
            MyProfile = new RelayCommand(MyProfileCommand);
            OpenAdd = new RelayCommand(async () => await OpenAddCommand());
            OpenFilter = new RelayCommand(async () => await OpenFilterCommand());
            ExitFilter = new RelayCommand(async () => await ExitFilterCommand());
            ClearFilters = new RelayCommand(() => ResetFilterPopup());
            OpenNotification = new RelayCommand(async () => await OpenNotificationCommand());
            ExitNotification = new RelayCommand(async () => await ExitNotificationCommand());

            ShowRemovedArticles = new RelayCommand(ShowRemovedArticlesCommand);

            #region Added for LoansPopup
            //Added
            OpenReturn = new RelayCommand(async () => await OpenReturnLoansCommand());
            //Close is just a simple popup close command
            Close = new RelayCommand(async () => await ClosePopup());
            Return = new RelayCommand(async () => await ReturnLoansCommand());
            AddToList = new RelayCommand(async () => await AddToListCommand());
            RemoveFromList = new RelayParameterizedCommand(async (parameter) => await RemoveFromListCommand(parameter));
            ListOfArticlesToReturn = new ObservableCollection<ArticleViewModel>();

            #region Added for notification
            ReturnReservation = new RelayParameterizedCommand(async (parameter) => await ReturnReservationCommand(parameter));
            LoanReservation = new RelayParameterizedCommand(async (parameter) => await LoanReservationCommand(parameter));
            #endregion
            #endregion
        }

        #endregion   

        #region Private Methods

        private async void ShowRemovedArticlesCommand()
        {
            if (IsShowingRemovedArticles)
            {
                IoC.CreateInstance<TableControlViewModel>().CurrentList = 
                    ArticleSearchList = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchArticles())
                    .ToModelDataToViewModel<IArticle, ArticleViewModel>().FillPlaceHolders();
                
                IsShowingRemovedArticles = false;
                IoC.CreateInstance<ApplicationViewModel>().CurrentUser = IoC.CreateInstance<ApplicationViewModel>().CurrentUser;
                IoC.CreateInstance<TableControlViewModel>().InitializeUpdateArticleStatuses();

            }
            else
            {
                IoC.CreateInstance<TableControlViewModel>().RemovedArticles = ArticleSearchList =
                    (await IoC.CreateInstance<ApplicationViewModel>().rep.GetRemovedArticles()).ToModelDataToViewModel<IArticle, ArticleViewModel>()
                    .ToList().ToObservableCollection().FillPlaceHolders();

                IsShowingRemovedArticles = true;
            }
        }

        /// <summary>
        /// Command to open the correct Add popup
        /// </summary>
        /// <returns></returns>
        private async Task OpenAddCommand()
        {
            // Check the page
            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                // Book page
                case ApplicationPages.BookPage:
                    {
                        IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.AddArticle);
                        break;
                    }

                // Employee page
                case ApplicationPages.EmployeePage:
                    {
                        IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.AddUser);
                        break;
                    }
            }


            await Task.Delay(1);
        }

        /// <summary>
        /// Command that opens up the filter popup
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task OpenFilterCommand()
        {

            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                case ApplicationPages.BookPage:
                    {
                        IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Filter);
                        break;
                    }

                default:
                    break;
            }

            await Task.Delay(1);

        }

        /// <summary>
        /// Command that exits the filter popup and changes the filter button if any checkboxes is checked.
        /// </summary>
        /// <returns></returns>
        private async Task ExitFilterCommand()
        {
            //Closes the popup
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            //Checks if any checkboxes are filled, then sets ChangeFilterColor property and changes the filter text.
            if (TitleFilter == true || AuthorFilter == true || IsbnFilter == true || EBookFilter == true)
            {
                FilterColor = FilterColors.Checked;
                FilterText = $"Filter ({FilterCheckboxCounter})";
                ShowClearFilterButton = true;
            }
            //Resets the filter-buttons color and text.
            else
            {
                FilterColor = FilterColors.UnChecked;
                FilterText = "Filter";
                FilterCheckboxCounter = 0;
                ShowClearFilterButton = false;
            }

            await Task.Delay(1);
        }

        /// <summary>
        /// Command to open Notification pop up
        /// </summary>
        /// <returns></returns>
        private async Task OpenNotificationCommand()
        {
            int roleID = IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID;
            if (roleID != 4)
            IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Notification);

            await Task.Delay(1);

        }

        /// <summary>
        /// Command to close Notification pop up
        /// </summary>
        /// <returns></returns>
        private async Task ExitNotificationCommand()
        {
            //Closes the popup
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
            //CheckIfReservedIsAvailable();

            await Task.Delay(1);
        }
        /// <summary>
        ///  Searches for articles in the articles list 
        /// </summary>
        private void SearchUpdate()
        {

            if (IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.BookPage && IsShowingRemovedArticles == false)
            {
  
                //Check if any filter is checked
                if (AuthorFilter == true && TitleFilter == true && IsbnFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.title.ToLower().Contains(_searchText.ToLower()) ||
                    s.author.ToLower().Contains(_searchText.ToLower()) ||
                    s.isbn.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else if (AuthorFilter == true && TitleFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.title.ToLower().Contains(_searchText.ToLower()) ||
                    s.author.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else if (AuthorFilter == true && IsbnFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.author.ToLower().Contains(_searchText.ToLower()) ||
                    s.isbn.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else if (TitleFilter == true && IsbnFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.title.ToLower().Contains(_searchText.ToLower()) ||
                    s.isbn.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else if (AuthorFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.author.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else if (TitleFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.title.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else if (IsbnFilter == true)
                {
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.isbn.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }

                else
                {
                    //Search through the list
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.author.ToLower().Contains(_searchText.ToLower()) ||
                    s.isbn.ToLower().Contains(_searchText.ToLower()) ||
                    s.publisher.ToLower().Contains(_searchText.ToLower()) ||
                    s.title.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
                }
            }

            else if(IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.BookPage && IsShowingRemovedArticles == true)
            {
                var id = IoC.CreateInstance<ApplicationViewModel>().Reasons.Where(r => r.reason.ToLower().Contains(_searchText.ToLower())).Select(r => r.reasonID);

                IoC.CreateInstance<TableControlViewModel>().RemovedArticles =
                ArticleSearchList.Where(x =>
                    x.IsPlaceholder == false && (
                    x.articleID.ToString().Contains(_searchText) ||
                    x.title.ToLower().Contains(_searchText.ToLower()) || 
                    id.Contains(x.reasonID)
                    
                )).ToList().ToObservableCollection().FillPlaceHolders();
            }

            else
            {

                IoC.CreateInstance<TableControlViewModel>().CurrentList =
                (IoC.CreateInstance<TableControlViewModel>().CurrentList as IEnumerable<UserViewModel>).Where(u =>
                u.IsPlaceholder == false && (
                u.personalNumber.ToLower().Contains(_searchText.ToLower()) ||
                u.firstName.ToLower().Contains(_searchText.ToLower()) ||
                u.lastName.ToLower().Contains(_searchText.ToLower()) ||
                u.type.ToLower().Contains(_searchText.ToLower()))).ToList().ToObservableCollection().FillPlaceHolders();
            }
        }

        #region Added for return loans
        /// <summary>
        /// MethodCommand that opens the popup for return loans
        /// </summary>
        private async Task OpenReturnLoansCommand()
        {
            //Open popup
            IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.ReturnLoans);
            await Task.Delay(1);
        }

        /// <summary>
        /// MethodCommand for closing the return loans popup
        /// </summary>
        private async Task ClosePopup()
        {
            //Close the popup
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            //Resetting the bool if popup closes
            ArticleDoesNotExists = false;
            await Task.Delay(1);
        }


        /// <summary>
        /// MethodCommand for returning the loans to the library.
        /// </summary>
        /// <returns></returns>
        private async Task ReturnLoansCommand()
        {
            //Goes through the list of articles add returns them to the library
            foreach (var item in ListOfArticlesToReturn)
            {
                //Returning specified article
                await IoC.CreateInstance<ApplicationViewModel>().rep.ReturnArticle(item.articleID);
            }

            //Open subpopup for successful return
            IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Success);

            await Task.Delay(700);

            //Closing subpopup
            IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();

            //Resetting the properties for next user or session
            ListOfArticlesToReturn = null;
            ArticleToReturn = null;
            ArticleID = null;

            //reload users to get updated list. Closes the window
            IoC.CreateInstance<TableControlViewModel>().LoadItems();

            //TODO! Change LoadItems To one that doesnt close the window. 
        }

        /// <summary>
        /// MethodCommand for finding and adding an article to the returnlist
        /// </summary>
        /// <returns></returns>
        private async Task AddToListCommand()
        {
            //Try to parse the input from user
            if (int.TryParse(ArticleID, out int result))
            {
                //If the list does not exist ( if the user does the return twice ), add to list creates a new list
                if (ListOfArticlesToReturn == null) ListOfArticlesToReturn = new ObservableCollection<ArticleViewModel>();

                //Transfer the result to a temporary variable to keep it safe from multiple key-presses that runs faster than the program can handle.
                int temp = result;

                //Finding an article in the database with ArticleID-prop, and setting the ArticleToReturn to that same object for displaying correct items in the list
                ArticleToReturn = (await IoC.CreateInstance<ApplicationViewModel>().rep.GetArticleByID(temp)).ToModel<IArticle, ArticleViewModel>();


                //Check to se so that the article is not aldreay added, that the article is not null and that the status of the object is 2 ( loaned ) 
                if (!ListOfArticlesToReturn.Any(x => x.articleID == temp) && ArticleToReturn != null && ArticleToReturn.statusID == 2)
                {
                    //Adds the article to the list
                    ListOfArticlesToReturn.Add(ArticleToReturn);

                    //Setting bool to false and removing warningtext
                    ArticleDoesNotExists = false;
                }

                //Did not add to the list and opens a warningtext
                else ArticleDoesNotExists = true;
            }

            //If it fails from start, open up warningtext
            else ArticleDoesNotExists = true;

            //Resetting the properties for next article
            ArticleToReturn = null;
            ArticleID = null;

        }

        /// <summary>
        /// MethodCommand for removing an item from the list
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        private async Task RemoveFromListCommand(object SelectedItem)
        {
            //Removes the article from the list
            ListOfArticlesToReturn.Remove((SelectedItem as ArticleViewModel));
            await Task.Delay(1);
        }

        /// <summary>
        /// MethodCommand to return a reservation if the user first in line regrets reserving it
        /// </summary>
        /// <param name="itemToReturn"></param>
        /// <returns></returns>
        private async Task ReturnReservationCommand(object itemToReturn)
        {
            //Small check so that itemToReturn is not null
            if (itemToReturn != null)
            {
                //Deletes the reservation from current user, and if there are more reservations on article sets the status to 4
                await IoC.CreateInstance<ApplicationViewModel>().rep.DeleteReservation((int)itemToReturn, IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber);
                
                //Clear the list
                NotificationList.Clear();

                //Open popup to indicate successfull return
                IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Success);             

                await Task.Delay(700);

                //Close popup
                IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();

                //Check the user if there are any more avaliable reservations
                await IoC.CreateInstance<UserLoginControlViewModel>().CheckNotifications();

                //Check if the new list contains any items
                if (NotificationList.Count > 0)
                {
                    //If true sets the notification
                    IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.Notification;
                }
                //Else set no notification
                else IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.NoNotification;
            }
        }

        /// <summary>
        /// MethodCommand to loan a article that is reserved by the user. (User is first in line on that article
        /// </summary>
        /// <param name="itemToLoan"></param>
        /// <returns></returns>
        private async Task LoanReservationCommand(object itemToLoan)
        {
            //Small check so that itemToLoan is not null
            if (itemToLoan != null)
            {
                //Loan article from reservation-list.
                await IoC.CreateInstance<ApplicationViewModel>().rep.LoanArticle(IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber, (int)itemToLoan);

                //Clear the list
                NotificationList.Clear();

                //Open popup to indicate successful loan
                IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Success);

                await Task.Delay(700);

                //Close popup
                IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();

                //Check the user if there are any more avaliable reservations
                await IoC.CreateInstance<UserLoginControlViewModel>().CheckNotifications();

                //Check if the new list contains any items
                if (NotificationList.Count > 0)
                {
                    //If true sets the notification
                    IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.Notification;
                }
                //Else set no notification
                else IoC.CreateInstance<MainContentUserControlViewModel>().NotificationColor = NotificationColors.NoNotification;
            }
        }
        #endregion

        #endregion


        #region Public Methods

        /// <summary>
        /// Command to open a MyProfile popup,
        /// if user is not logged in the login popup will be displayed
        /// </summary>
        /// <returns></returns>
        public void MyProfileCommand()
        {
            if (IoC.CreateInstance<ApplicationViewModel>().CurrentUser.roleID != 4)
            {
                //Load the data to the profile everytime it opens
                IoC.CreateInstance<MyProfileControlViewModel>().GetMyLoansAndReservations();
                // Setting the pop up content
                IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.MyProfile);
            }     
            else
                IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.UserLogin);
        }

        /// <summary>
        /// Resets the FilterPopup checkboxes for when changing pages.
        /// </summary>
        public void ResetFilterPopup()
        {
            TitleFilter = false;
            AuthorFilter = false;
            IsbnFilter = false;
            EBookFilter = false;

            FilterColor = FilterColors.UnChecked;
            FilterText = "Filter";
            FilterCheckboxCounter = 0;

            ShowClearFilterButton = false;
        }

        #endregion
    }
}
