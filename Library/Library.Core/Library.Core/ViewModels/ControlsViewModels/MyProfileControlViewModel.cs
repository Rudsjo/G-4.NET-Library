using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class MyProfileControlViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Command to log out a user
        /// </summary>
        public ICommand Logout { get; set; }

        /// <summary>
        /// Command to close the popup
        /// </summary>
        public ICommand Exit { get; set; }


        /// <summary>
        /// Command to open the changepassword pop up
        /// </summary>
        public ICommand ChangePassword { get; set; }

        /// <summary>
        /// The loans of the current user
        /// </summary>
        public ObservableCollection<ArticleViewModel> MyLoans { get; set; }

        /// <summary>
        /// The reservations of the current user
        /// </summary>
        public ObservableCollection<ArticleViewModel> MyReservations { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MyProfileControlViewModel()
        {
            // Setting commands
            Logout = new RelayCommand(LogoutCommand);
            Exit = new RelayCommand(() => IoC.CreateInstance<ApplicationViewModel>().ClosePopUp());
            ChangePassword = new RelayCommand(() => { IoC.CreateInstance<ApplicationViewModel>().OpenSubPopUp(PopUpContents.ChangePassword); });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Filling <see cref="MyLoans"/> and <see cref="MyReservations"/>
        /// </summary>
        public async void GetMyLoansAndReservations()
        {
            MyLoans = new ObservableCollection<ArticleViewModel>((await
                IoC.CreateInstance<ApplicationViewModel>().rep.GetUserLoans(
                    IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber))
                    .ToList().ToObservableCollection().ToModelDataToViewModel<IArticle, ArticleViewModel>().FillPlaceHolders(3));

            MyReservations = new ObservableCollection<ArticleViewModel>((await
                IoC.CreateInstance<ApplicationViewModel>().rep.GetUserReservations(
                    IoC.CreateInstance<ApplicationViewModel>().CurrentUser.personalNumber))
                    .ToList().ToObservableCollection().ToModelDataToViewModel<IArticle, ArticleViewModel>().FillPlaceHolders(3));
        }

        /// <summary>
        /// The command to logout a user
        /// </summary>
        /// <returns></returns>
        private void LogoutCommand()
        {
            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
            IoC.CreateInstance<ApplicationViewModel>().GoToPage(ApplicationPages.MainPage);

        }


        #endregion

    }
}
