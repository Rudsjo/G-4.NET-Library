namespace Library.Core
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
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
        /// 
        public ObservableCollection<dynamic> CurrentCSV { get; set; }

        /// <summary>
        /// Command to download the CSV file
        /// </summary>
        public ICommand DownloadCSV { get; set; }

        /// <summary>
        /// Command to show the report of loaned articles
        /// </summary>
        public ICommand ShowLoanedArticles { get; set; }

        /// <summary>
        /// Command to show the report of reserved articles
        /// </summary>
        public ICommand ShowReservedArticles { get; set; }


        /// <summary>
        /// Flag to be set if all loaned books should be shown
        /// </summary>
        public bool AllLoanedBooksFilter { get; set; }

        /// <summary>
        /// Flag to be set if all loaned books should be shown
        /// </summary>
        public bool AllReservedBooksFilter { get; set; }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportPageViewModel()
        {
            // Setting initial list
            CurrentCSV = new ObservableCollection<dynamic>()
            {
                new { Rubrik = "" }
            };

            // Setting commands
            ShowLoanedArticles = new RelayCommand(GetAllLoanedArticlesAndTheUser);
            ShowReservedArticles = new RelayCommand(GetAllReservedArticlesAndTheUser);

            DownloadCSV = new RelayCommand(DownloadCSVCommand);
        }

        #region Private functions

        private void DownloadCSVCommand()
        {
            IoC.CreateInstance<ApplicationViewModel>().ShowDownload();

            string CSVName = IoC.CreateInstance<ApplicationViewModel>().CurrentPage.ToString();

            // File index
            int FileNumber = 0;
            // Find the file with the highest or missing index
            foreach (string file_string in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\"))
                if (Regex.IsMatch(new FileInfo(file_string).Name, @$"{CSVName}[\d]+\.csv") && int.TryParse(Regex.Match(new FileInfo(file_string).Name, @"[\d]+").Value, out int i) && i == FileNumber)
                    FileNumber++;

            CurrentCSV.SaveAsCSV(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{CSVName}{FileNumber}.csv");

        }

        /// <summary>
        /// Gets all the current loans and the user loaning it
        /// </summary>
        private async void GetAllLoanedArticlesAndTheUser()
        {
            try
            {
                // Making sure the user doesnt click anything while loading
                IoC.CreateInstance<ApplicationViewModel>().IsLoading = true;

                // Setting a temporary list
                var templist = new ObservableCollection<dynamic>();

                // Getting all the items
                foreach (var user in IoC.CreateInstance<MainContentUserControlViewModel>().UserSearchList)
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

                // Setting the flags to change backgrounds in UI
                AllLoanedBooksFilter = true;
                AllReservedBooksFilter = false;

                // Making sure we have items, otherwise we tell the user
                if (templist.Count != 0)
                    CurrentCSV = templist;
                else
                    CurrentCSV = new ObservableCollection<dynamic>()
                {
                    new { NoResults = "" },
                };
            }

            // End the load no matter what happens
            finally { IoC.CreateInstance<ApplicationViewModel>().IsLoading = false; }

        } 

        /// <summary>
        /// Gets all the current reservations and the user reserving it
        /// </summary>
        /// <returns></returns>
        private async void GetAllReservedArticlesAndTheUser()
        {
            try
            {
                // Making sure the user doesnt click anything while loading
                IoC.CreateInstance<ApplicationViewModel>().IsLoading = true;

                // Setting a temporary list
                var templist = new ObservableCollection<dynamic>();

                // Getting all the items
                foreach (var user in IoC.CreateInstance<MainContentUserControlViewModel>().UserSearchList)
                {
                    (await IoC.CreateInstance<ApplicationViewModel>().rep.GetUserReservations(user.personalNumber)).ToList().ForEach(x =>
                    {
                        templist.Add(new
                        {
                            Namn = $"{user.firstName} {user.lastName}",
                            Bok = $"{x.title}"
                        });
                    });

                }

                // Setting the flags to change backgrounds in UI
                AllReservedBooksFilter = true;
                AllLoanedBooksFilter = false;

                // Making sure we have items, otherwise we tell the user
                if (templist.Count != 0)
                    CurrentCSV = templist;
                else
                    CurrentCSV = new ObservableCollection<dynamic>()
                {
                    new { NoResults = "" },
                };
            }

            // End the load no matter what happens
            finally { IoC.CreateInstance<ApplicationViewModel>().IsLoading = false; }
        }

        #endregion
    }
}
