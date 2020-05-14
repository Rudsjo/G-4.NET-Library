using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    /// <summary>
    /// View model for the add article control
    /// </summary>
    public class AddArticleControlViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Holding the title written by the user
        /// </summary>
        public string InputTitle { get; set; }

        /// <summary>
        /// Holding the author written by the user
        /// </summary>
        public string InputAuthor { get; set; }

        /// <summary>
        /// Holding the publisher written by the user
        /// </summary>
        public string InputPublisher { get; set; }

        /// <summary>
        /// Holding the ISBN written by the user
        /// </summary>
        public string InputISBN { get; set; }

        /// <summary>
        /// Holding the loan time written by the user
        /// </summary>
        public string InputLoanTime { get; set; }

        /// <summary>
        /// Holding the placement written by the user
        /// </summary>
        public string InputPlacement { get; set; }

        /// <summary>
        /// Holding the price written by the user
        /// </summary>
        public string InputPrice { get; set; }

        /// <summary>
        /// The number of articles to add of the same type
        /// </summary>
        public string InputQuantity { get; set; }

        /// <summary>
        /// The command to add an item to the temporary list
        /// </summary>
        public ICommand AddToTempList { get; set; }

        /// <summary>
        /// Command to close the pop up
        /// </summary>
        public ICommand Close { get; set; }

        /// <summary>
        /// Command to confirm all added items
        /// </summary>
        public ICommand Confirm { get; set; }

        /// <summary>
        /// List to hold the added items before sending them to the database
        /// </summary>
        public ObservableCollection<ArticleViewModel> TempListOfArticles { get; set; } = new ObservableCollection<ArticleViewModel>();

        /// <summary>
        /// Flag to indicate when an item should me animated in
        /// </summary>
        public bool IsBeingAdded { get; set; }

        /// <summary>
        /// Command to remove a selected added item
        /// </summary>
        public ICommand RemoveAddedItem { get; set; }


        // TODO: Add Category and Description properties
        

        #endregion

        public AddArticleControlViewModel()
        {
            // Setting commands
            AddToTempList = new RelayCommand(async () => await AddToTempListCommad());
            Close = new RelayCommand(() => { IoC.CreateInstance<ApplicationViewModel>().ClosePopUp(); });
            Confirm = new RelayCommand(async () => await ConfirmCommand());
            RemoveAddedItem = new RelayParameterizedCommand(c);
        }

        #region Private Methods

        private void c(object item)
        {
            TempListOfArticles.Remove((item as ArticleViewModel));
        }

        /// <summary>
        /// Command to add items to <see cref="TempListOfArticles"/>
        /// </summary>
        /// <returns></returns>
        private async Task AddToTempListCommad()
        {
            TempListOfArticles.Add(new ArticleViewModel() { title = InputTitle });
        }
        
        /// <summary>
        /// Command to confirm all the added items and add them to the database
        /// </summary>
        /// <returns></returns>
        private async Task ConfirmCommand()
        {
            foreach (var item in TempListOfArticles)
            {
                // Set the out animation
                item.ShouldAnimateOut = true;
                await Task.Delay(300);

                // Adds the item to the "database"
                // TODO: Implement real db call
                IoC.CreateInstance<BookPageViewModel>().BookList.Add(item);
                IoC.CreateInstance<TableControlViewModel>().CurrentList = IoC.CreateInstance<BookPageViewModel>().BookList;
            }

            // Make sure all animations are finished
            await Task.Delay(500);

            // Clears the temp list
            TempListOfArticles.Clear();
        }

        #endregion

    }
}
