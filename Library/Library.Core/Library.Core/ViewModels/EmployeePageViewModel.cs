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
            IoC.CreateInstance<TableControlViewModel>().LoadItems();

            // Setting the dynamic texts
            IoC.CreateInstance<MainContentUserControlViewModel>().HeaderText = "Alla användare";
            IoC.CreateInstance<MainContentUserControlViewModel>().AddButtonText = "Lägg till användare";
            IoC.CreateInstance<TableControlViewModel>().TableHeaderTexts = new string[] { "Personnummer", "Förnamn", "Efternamn", "Lånade artiklar", "Reserverade artiklar" };

            // Setting statuses
            IoC.CreateInstance<TableControlViewModel>().TableToSort = SortableTables.None;
        }

        #endregion
    }
}
