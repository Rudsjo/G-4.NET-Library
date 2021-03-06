﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class ChangePasswordControlViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Command to close the popup
        /// </summary>
        public ICommand Close { get; set; }

        /// <summary>
        /// Confirms the change
        /// </summary>
        public ICommand Confirm { get; set; }

        /// <summary>
        /// Flag to indicate if everything is filled up
        /// </summary>
        public bool IsNotFilledCorrectly { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChangePasswordControlViewModel()
        {
            // Setting commands
            Close = new RelayCommand(() => 
            {
                IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                IsNotFilledCorrectly = false;
            });
            Confirm = new RelayParameterizedCommand(async (password) => await ConfirmCommand(password));
        }

        #endregion

        #region Private Methods


        private async Task ConfirmCommand(object password)
        {
            // Check that both passwordboxes are filled
            if ((password as IHavePassword).SecurePassword == null || (password as INewPassword).SecondPassword == null ||
                (password as IHavePassword).SecurePassword.Length < 4 || (password as INewPassword).SecondPassword.Length < 4)
            {
                IsNotFilledCorrectly = true;
                return;
            }

            if(await LoginHelpers.UpdatePassword(IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber,
                                              (password as IHavePassword).SecurePassword, (password as INewPassword).SecondPassword))
            {
                IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.Success);
                await Task.Delay(700);
                IoC.CreateInstance<ApplicationViewModel>().CloseSubPopUp();
                IsNotFilledCorrectly = false;
            }
            else
            {
                IsNotFilledCorrectly = true;
                return;
            }


        }

        #endregion

    }
}
