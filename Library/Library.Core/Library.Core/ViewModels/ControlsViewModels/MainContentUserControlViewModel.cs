﻿using System;
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

        /// <summary>
        /// List to hold articles
        /// </summary>
        private IEnumerable<ArticleViewModel> ArticleItems;

        /// <summary>
        /// List to hold articles
        /// </summary>
        private IEnumerable<UserViewModel> UserItems;

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
        ///  Searches for articles in the articles list 
        /// </summary>
        private void SearchUpdate()
        {
            if(IoC.CreateInstance<ApplicationViewModel>().CurrentPage == ApplicationPages.BookPage)
            {
                IoC.CreateInstance<TableControlViewModel>().CurrentList = ArticleItems.Where(s =>
                s.author.ToLower().Contains(_searchText.ToLower()) ||
                s.isbn.ToLower().Contains(_searchText.ToLower()) ||
                s.publisher.ToLower().Contains(_searchText.ToLower()) ||
                s.title.ToLower().Contains(_searchText.ToLower())).FillPlaceHolders();
            }
            else
            {
                IoC.CreateInstance<TableControlViewModel>().CurrentList = UserItems.Where(u =>
                u.personalNumber.ToLower().Contains(_searchText.ToLower()) ||
                u.firstName.ToLower().Contains(_searchText.ToLower()) ||
                u.lastName.ToLower().Contains(_searchText.ToLower()) ||
                u.type.ToLower().Contains(_searchText.ToLower())).FillPlaceHolders();
            }
        }

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

            // Get items from the database
            LoadItems();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to fill a local list with data from the database
        /// </summary>
        private async void LoadItems() 
        { 
            ArticleItems = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchArticles()).ConvertModelDataToViewModel<IArticle, ArticleViewModel>(); 
            UserItems = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchUsers()).ConvertModelDataToViewModel<IUser, UserViewModel>();
        }


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

        #endregion

    }
}
