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
            //Fills all the neccesary data on users
            Task.Run(async () => await FillUserData());

            // Setting the dynamic texts
            IoC.CreateInstance<MainContentUserControlViewModel>().HeaderText = "Alla användare";
            IoC.CreateInstance<MainContentUserControlViewModel>().AddButtonText = "Lägg till användare";
            IoC.CreateInstance<TableControlViewModel>().TableHeaderTexts = new string[] { "Personnummer", "Förnamn", "Efternamn", "Lånade artiklar", "Reserverade artiklar" };

            // Setting statuses
            IoC.CreateInstance<TableControlViewModel>().TableToSort = SortableTables.None;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get and fill all neccesary user data
        /// </summary>
        public async Task FillUserData()
        {
            // Set the flag to indicate that we're loading data
            IoC.CreateInstance<ApplicationViewModel>().IsLoading = true;

            //Fills the list of Roles in addUser Popup
            IoC.CreateInstance<AddUserControlViewModel>().CurrentRoles = ViewModelHelpers.FillWithModelData<IRole, RoleViewModel>(await IoC.CreateInstance<ApplicationViewModel>().rep.GetAllRoles());

            //Fills the list of users, gets all users when not given a parameter
            UserList = ViewModelHelpers.FillWithModelData<IUser, UserViewModel>(await IoC.CreateInstance<ApplicationViewModel>().rep.SearchUsers()).ToObservableCollection();
            IoC.CreateInstance<TableControlViewModel>().CurrentList = UserList.FillPlaceHolders().ToList().ToObservableCollection();


            // When data is loaded, set the flag
            IoC.CreateInstance<ApplicationViewModel>().IsLoading = false;
        }

        #endregion
    }
}
