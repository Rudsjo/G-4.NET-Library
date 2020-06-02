using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core
{
    /// <summary>
    /// View model for the employee page
    /// </summary>
    public class EmployeePageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The list of users
        /// </summary>
        public ObservableCollection<UserViewModel> UserList { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeePageViewModel()
        {
            IoC.CreateInstance<MainContentUserControlViewModel>().IsShowingRemovedArticles = false;

            // Setting the dynamic texts
            IoC.CreateInstance<MainContentUserControlViewModel>().HeaderText = "Alla användare";
            IoC.CreateInstance<MainContentUserControlViewModel>().AddButtonText = "Lägg till användare";
            IoC.CreateInstance<TableControlViewModel>().TableHeaderTexts = new string[] { "Personnummer", "Förnamn", "Efternamn", "Lånade artiklar", "Reserverade artiklar" };

            // Setting statuses
            IoC.CreateInstance<TableControlViewModel>().TableToSort = SortableTables.None;

            IoC.CreateInstance<ApplicationViewModel>().CurrentReasons = new ObservableCollection<Reason>(
                IoC.CreateInstance<ApplicationViewModel>().Reasons.Where(x => x.reasonType == ReasonTypes.UserReasons).ToList().ToObservableCollection());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to get all searchable users
        /// </summary>
        public async void FillSearchableUserList()
        {
            // Get the full list
            IoC.CreateInstance<MainContentUserControlViewModel>().UserSearchList =
                (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchUsers()).ToModelDataToViewModel<IUser, UserViewModel>();
        }

        #endregion
    }
}
