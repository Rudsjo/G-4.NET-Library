﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        /// <summary>
        /// The personal number of the user to block
        /// </summary>
        public string UserToBlock { get; set; }

        /// <summary>
        /// The reason that gets picked
        /// </summary>
        public Reason ChosenReason { get; set; }

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
                    {
                        // Sets the status to 3 and becomes unavaliable
                        await IoC.CreateInstance<ApplicationViewModel>().rep.DeleteArticle(ArticleToDelete, ChosenReason.reasonID);

                        IoC.CreateInstance<TableControlViewModel>().LoadItems();

                        //Close the subpopup
                        IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                        break;
                    }

                // Employee page
                case ApplicationPages.EmployeePage:
                    {
                        //Checks so that both needed parameters is not null, only then can a user be blocked
                        if (ChosenReason != null && UserToBlock != null)
                        {
                            //Block user with a reason
                            await IoC.CreateInstance<ApplicationViewModel>().rep.BlockUser(UserToBlock, ChosenReason.reasonID);

                            //Sets the IsBlocked to true
                            (IoC.CreateInstance<TableControlViewModel>().SelectedUser as UserViewModel).IsBlocked = true;

                            //Resetting values
                            UserToBlock = null;

                            //Close the subpopup
                            IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                            break;
                        }
                        //Check so that a user is selected and a reason is null, and then deletes a user
                        else if (UserToDelete != null && ChosenReason != null)
                        {
                            // Deletes the user
                            await IoC.CreateInstance<ApplicationViewModel>().rep.DeleteUser(UserToDelete);

                            //reset values
                            UserToDelete = null;

                            IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                            IoC.CreateInstance<TableControlViewModel>().LoadItems();

                            break;
                        }

                        break;
                    }

                default:
                    break;
            }
        }

        /// <summary>
        /// Command to abort an action
        /// </summary>
        /// <returns></returns>
        private async Task AbortCommand()
        {
            switch (IoC.CreateInstance<ApplicationViewModel>().CurrentPage)
            {
                //Book page
                case ApplicationPages.BookPage:
                    {
                        //Closes the popup
                        IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                        break;
                    }

                //EmployeePage
                case ApplicationPages.EmployeePage:
                    {
                        // Closes the pop up
                        IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                        break;
                    }

                default:
                    break;
            }

            await Task.Delay(1);
        }
        #endregion
    }
}
