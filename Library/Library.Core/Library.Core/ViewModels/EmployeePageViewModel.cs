using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core
{
    public class EmployeePageViewModel : BaseViewModel
    {
        #region Public Properties

        public ObservableCollection<UserViewModel> UserList { get; set; }

        public bool IsLoading { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeePageViewModel()
        {
            //Fills all the neccesary data on users
            Task.Run(async () => 
            {
                IsLoading = true;

                if (IsLoading)
                {
                    IoC.CreateInstance<ApplicationViewModel>().OpenPopUp(PopUpContents.Loading);
                }

                await FillUserData();

                IsLoading = false;
                IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();

            });

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
            //Fills the list of Roles in addUser Popup
            IoC.CreateInstance<AddUserControlViewModel>().CurrentRoles = ViewModelHelpers.FillWithModelData<IRole, RoleViewModel>(await IoC.CreateInstance<ApplicationViewModel>().rep.GetAllRoles());

            //Fills the list of users, gets all users when not given a parameter
            UserList = ViewModelHelpers.FillWithModelData<IUser, UserViewModel>(await IoC.CreateInstance<ApplicationViewModel>().rep.SearchUsers()).ToObservableCollection();
            IoC.CreateInstance<TableControlViewModel>().CurrentList = UserList.FillPlaceHolders().ToList().ToObservableCollection();

        }

        #endregion
    }
}
