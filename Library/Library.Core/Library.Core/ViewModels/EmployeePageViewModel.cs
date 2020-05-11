using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Library.Core
{
    public class EmployeePageViewModel : BaseViewModel
    {
        #region Public Properties

        public IEnumerable<UserViewModel> EmployeeList { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeePageViewModel()
        {
            // ERSÄTTS AV DATABAS?
            EmployeeList = IoC.CreateInstance<AddUserControlViewModel>().UserList;

            IoC.CreateInstance<TableControlViewModel>().CurrentList = EmployeeList.FillPlaceHolders().ToList().ToObservableCollection();

            // Setting the dynamic texts
            IoC.CreateInstance<MainContentUserControlViewModel>().HeaderText = "Alla anställda";
            IoC.CreateInstance<MainContentUserControlViewModel>().AddButtonText = "Lägg till anställd";
            IoC.CreateInstance<TableControlViewModel>().TableHeaderTexts = new string[] { "Personnummer", "Förnamn", "Efternamn", "Lånade artiklar", "Reserverade artiklar" };

            // Setting statuses
            IoC.CreateInstance<TableControlViewModel>().TableToSort = SortableTables.None;
        }

        #endregion

    }
}
