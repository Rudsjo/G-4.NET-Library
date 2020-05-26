namespace Library.Core
{
    #region Namespaces
    using System;
    using System.Collections.ObjectModel;
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

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportPageViewModel()
        {
            // Create the CSV collection
            CurrentCSV = new ObservableCollection<dynamic>()
            {
                new { ID = 1, FirstName = "Jimmy", LasName = "Sassila", Age = 22 },
                new { ID = 1, FirstName = "Jimmy", LasName = "Sassila", Age = 22 },
                new { ID = 1, FirstName = "Jimmy", LasName = "Sassila", Age = 22 },
            };

            // Create the update command
            UpdateCSVItems = new RelayCommand(UpdateCSV);
        }


        #region Private functions

        /// <summary>
        /// Update the <see cref="Curre"/>
        /// </summary>
        private async void UpdateCSV()
        {

        } 

        #endregion
    }
}
