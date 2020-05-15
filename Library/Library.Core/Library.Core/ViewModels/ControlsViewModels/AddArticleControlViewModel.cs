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

        /// <summary>
        /// The current article to add
        /// </summary>
        public IArticle CurrentArticle { get; set; } = new ArticleViewModel();

        public ICategory CurrentCategory { get; set; } = new CategoryViewModel();

        public IEnumerable<ICategory> AvailableCategories { get; set; } = new ObservableCollection<CategoryViewModel>();
        

        #endregion

        public AddArticleControlViewModel()
        {

            // Setting commands
            AddToTempList = new RelayCommand(async () => await AddToTempListCommad());
            Close = new RelayCommand(() => { IoC.CreateInstance<ApplicationViewModel>().ClosePopUp(); });
            Confirm = new RelayCommand(async () => await ConfirmCommand());
            RemoveAddedItem = new RelayParameterizedCommand((item) => { TempListOfArticles.Remove(item as ArticleViewModel); });

            GetAllCategories();
        }

        #region Private Methods

        private async void GetAllCategories() 
        {

            AvailableCategories =
                  (await IoC.CreateInstance<ApplicationViewModel>().rep.GetAllCategories()).ToModelDataToViewModel<ICategory, CategoryViewModel>();
                
        }

        /// <summary>
        /// Command to add items to <see cref="TempListOfArticles"/>
        /// </summary>
        /// <returns></returns>
        private async Task AddToTempListCommad()
        {
            if ((CurrentArticle as ArticleViewModel).quantity == 0)
                (CurrentArticle as ArticleViewModel).quantity = 1;


            CurrentArticle.categoryID = CurrentCategory.categoryID;

            for (int i = 0; i < (CurrentArticle as ArticleViewModel).quantity; i++)
            {
                // Add a new item
                TempListOfArticles.Add((CurrentArticle as ArticleViewModel).ToModel<IArticle, ArticleViewModel>());
                await Task.Delay(100);
            }

            ClearProperties();
        }
        
        /// <summary>
        /// Command to confirm all the added items and add them to the database
        /// </summary>
        /// <returns></returns>
        private async Task ConfirmCommand()
        {
            foreach (var item in TempListOfArticles)
            {
                // Adds the item to the database
                if (!await IoC.CreateInstance<ApplicationViewModel>().rep.AddArticle(item.ToModel<IArticle, Article>()))
                {
                    // TODO: Skapa varningstext
                    return;
                }

                // Set the out animation
                item.ShouldAnimateOut = true;
                await Task.Delay(100);
            }

            // Make sure all animations are finished
            await Task.Delay(500);

            // Clears the temp list
            TempListOfArticles.Clear();

            IoC.CreateInstance<ApplicationViewModel>().ClosePopUp();
            IoC.CreateInstance<TableControlViewModel>().LoadItems();
        }

        private void ClearProperties()
        {
            CurrentArticle = new ArticleViewModel();
        }

        #endregion

    }
}
