using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class TableControlViewModel : BaseViewModel
    {

        public ObservableCollection<User> TempList { get; set; }

        #region Public Properties

        /// <summary>
        /// Command to sort a specific column
        /// </summary>
        public ICommand Sort { get; set; }

        /// <summary>
        /// State of which table to sort
        /// </summary>
        public SortableTables TableToSort { get; set; }

        /// <summary>
        /// The current list to display in the table
        /// </summary>
        public IEnumerable CurrentList { get; set; }

        /// <summary>
        /// The maximum number of items to be displayed in the table
        /// </summary>
        public int MaxItemsToShowInTable { get; set; } = 8;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TableControlViewModel()
        {
            // Setting commands
            Sort = new RelayParameterizedCommand(SortCommand);

            // Filling dummy data
            TempList = new ObservableCollection<User>()
            {
                new User()
                {
                    personalNumber = "2",
                    firstName = "B",
                    lastName = "Ett",
                },
                new User()
                {
                    personalNumber = "3",
                    firstName = "A",
                    lastName = "Två",
                },
                new User()
                {
                    personalNumber = "1",
                    firstName = "C",
                    lastName = "Tre",
                },
            };

            // Filling placeholders
            TempList = TempList.FillPlaceHolders<User, ObservableCollection<User>>(MaxItemsToShowInTable);

        }


        #endregion

        #region Private Methods

        private void SortCommand(object table)
        {
            switch ((string)table)
            {
                case "PNumber":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.PersonalNumber;

                        // Get the ordered list
                        var tempList = TempList.OrderBy(x => x.personalNumber).Where(x => x.personalNumber != null).ToList();

                        // Return the ordered list with placeholders
                        TempList = tempList.FillPlaceHolders<User, List<User>>(MaxItemsToShowInTable);
                        break;
                    }

                case "FName":
                    {
                        TableToSort = SortableTables.FirstName;
                        var tempList = TempList.OrderBy(x => x.firstName).Where(x => x.firstName != null).ToList();
                        TempList = tempList.FillPlaceHolders<User, List<User>>(MaxItemsToShowInTable);
                        break;
                    }

                case "LName":
                    {
                        TableToSort = SortableTables.LastName;
                        var tempList = TempList.OrderBy(x => x.lastName).Where(x => x.lastName != null).ToList();
                        TempList = tempList.FillPlaceHolders<User, List<User>>(MaxItemsToShowInTable);
                        break;
                    }

                case "LArticles":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.LoanedArticles;

                        // Get the ordered list
                        var tempList = TempList.OrderBy(x => x.personalNumber).Where(x => x.personalNumber != null).ToList();

                        // Return the ordered list with placeholders
                        TempList = tempList.FillPlaceHolders<User, List<User>>(MaxItemsToShowInTable);
                        break;
                    }

                case "RArticles":
                    {
                        // Set the correct state
                        TableToSort = SortableTables.ReservedArticles;

                        // Get the ordered list
                        var tempList = TempList.OrderBy(x => x.personalNumber).Where(x => x.personalNumber != null).ToList();

                        // Return the ordered list with placeholders
                        TempList = tempList.FillPlaceHolders<User, List<User>>(MaxItemsToShowInTable);
                        break;
                    }

            }
        }

        #endregion
    }
}
