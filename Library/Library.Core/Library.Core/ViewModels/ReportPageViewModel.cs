namespace Library.Core
{
    #region Namespaces
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
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
            GetList();

            // Create the update command
            UpdateCSVItems = new RelayCommand(UpdateCSV);
            DownloadCSV = new RelayCommand(() =>
            {
                File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Test.csv", CurrentCSV.ToCSV());
            });
        }

        async void GetList()
        {
            var list = (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchUsers()).ToList().ToObservableCollection();

            // Create the CSV collection
            CurrentCSV = new ObservableCollection<dynamic>(list);
        }


        #region Private functions

        /// <summary>
        /// Update the <see cref="CurrentCSV"/>
        /// </summary>
        private async void UpdateCSV()
        {

        } 

        #endregion
    }
}
