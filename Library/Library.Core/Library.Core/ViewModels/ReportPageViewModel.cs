namespace Library.Core
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    #endregion

    /// <summary>
    /// ViewModel for the ReportPage/>
    /// </summary>
    public class ReportPageViewModel : BaseViewModel
    {

        #region Public properties

        /// <summary>
        /// CSV items
        /// </summary>
        public ObservableCollection<dynamic> CurrentCSV { get; set; }

        /// <summary>
        /// Command for updating the <see cref="CurrentCSV"/> collection
        /// </summary>
        public ICommand UpdateCSVItems { get; set; }

        /// <summary>
        /// Command to download the CSV file
        /// </summary>
        public ICommand DownloadCSV { get; set; }

        /// <summary>
        /// Command to set the choice of filter
        /// </summary>
        public ICommand MainFilterChoice { get; set; }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportPageViewModel()
        {
            CurrentCSV = new ObservableCollection<dynamic>()
            {
                new { x = 1 }
            };

            // Create the update command
            UpdateCSVItems = new RelayCommand(async () => await UpdateCSV());
        }

        async void GetInitialList()
        {
            // Create the CSV collection
            CurrentCSV = new ObservableCollection<dynamic>();
        }


        #region Private functions

        /// <summary>
        /// Update the <see cref="CurrentCSV"/>
        /// </summary>
        private async Task UpdateCSV()
        {

            var templist = new ObservableCollection<dynamic>();

            foreach(var user in IoC.CreateInstance<MainContentUserControlViewModel>().UserSearchList)
            {
                (await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserLoans(user.personalNumber)).ToList().ForEach(x => 
                {
                    templist.Add(new
                    {
                        Namn = $"{user.firstName} {user.lastName}",
                        Bok = $"{x.title}"
                    });
                });

            }

            CurrentCSV = templist;

        } 

        #endregion
    }
}
