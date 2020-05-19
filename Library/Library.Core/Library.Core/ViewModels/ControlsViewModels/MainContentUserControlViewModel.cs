using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace Library.Core
{
    public class MainContentUserControlViewModel : BaseViewModel
    {

        #region Public Properties

        public IEnumerable<ArticleViewModel> ArticleSearchList { get; set; }
        public IEnumerable<UserViewModel> UserSearchList { get; set; }

        /// <summary>
        /// Sets the color of the Filter button
        /// </summary>
        public FilterColors FilterColor { get; set; }

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
        /// Command to show all removed articles in the system
        /// </summary>
        public ICommand ShowRemovedArticles { get; set; }

        /// <summary>
        /// Flag to indicate if removed articles are shown
        /// </summary>
        public bool IsShowingRemovedArticles { get; set; }

        /// <summary>
        /// The text to show in the header above the table
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// The text to show in the add button
        /// </summary>
        public string AddButtonText { get; set; }


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

        #endregion


        #region Contructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainContentUserControlViewModel()
        {
            // Setting commands
            MyProfile = new RelayCommand(async () => await MyProfileCommandAsync());
            OpenAdd = new RelayCommand(async () => await OpenAddCommand());
            OpenFilter = new RelayCommand(async () => await OpenFilterCommand());
            ExitFilter = new RelayCommand(async () => await ExitFilterCommand());

            ShowRemovedArticles = new RelayCommand(async () => await ShowRemovedArticlesCommand());
        }

        #endregion

        #region Private Methods

        private async Task ShowRemovedArticlesCommand()
        {
            if (IsShowingRemovedArticles)
            {
                IoC.CreateInstance<TableControlViewModel>().CurrentList = 
                    ArticleSearchList = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchArticles())
                    .ToModelDataToViewModel<IArticle, ArticleViewModel>().FillPlaceHolders();
                
                IsShowingRemovedArticles = false;
            }
            else
            {
                IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(x => x.statusID == 3).ToList().ToObservableCollection().FillPlaceHolders();

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

                // Customer page
                case ApplicationPages.CustomerPage:
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
            }
            //Resets the filter-buttons color and text.
            else
            {
                FilterColor = FilterColors.UnChecked;
                FilterText = "Filter";
                FilterCheckboxCounter = 0;
            }

            await Task.Delay(1);
        }

        /// <summary>
        ///  Searches for articles in the articles list 
        /// </summary>
        private async Task SearchUpdate()
        {

            if (IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.BookPage)
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
                    // Search through the list
                    IoC.CreateInstance<TableControlViewModel>().CurrentList =
                    ArticleSearchList.Where(s =>
                    s.IsPlaceholder == false && (
                    s.author.ToLower().Contains(_searchText.ToLower()) ||
                    s.isbn.ToLower().Contains(_searchText.ToLower()) ||
                    s.publisher.ToLower().Contains(_searchText.ToLower()) ||
                    s.title.ToLower().Contains(_searchText.ToLower())))
                    .ToList().ToObservableCollection().FillPlaceHolders();
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
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Command to open a MyProfile popup,
        /// if user is not logged in the login popup will be displayed
        /// </summary>
        /// <returns></returns>
        public async Task MyProfileCommandAsync()
        {
            // Indicating that a pop up control will be shown
            IoC.CreateInstance<ApplicationViewModel>().PopUpVisible = true;

            // Setting the pop up content
            IoC.CreateInstance<PopUpControlViewModel>().PopUpContent = PopUpContents.UserLogin;


            // Getting rid of disgusting warning
            await Task.Delay(1);

        }

        #endregion
    }
}
