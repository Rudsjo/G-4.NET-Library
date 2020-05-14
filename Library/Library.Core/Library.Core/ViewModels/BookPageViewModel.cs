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
    /// View model for the Book page containing logics for the page
    /// </summary>
    public class BookPageViewModel : BaseViewModel
    {
        #region Public Properties

        public ObservableCollection<ArticleViewModel> BookList { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BookPageViewModel()
        {
            //Fills list of articles when page loads
            Task.Run(async () => await FillArticleData());

            // Setting the dynamic texts
            IoC.CreateInstance<MainContentUserControlViewModel>().HeaderText = "Alla böcker";
            IoC.CreateInstance<MainContentUserControlViewModel>().AddButtonText = "Lägg till bok";
            IoC.CreateInstance<TableControlViewModel>().TableHeaderTexts = new string[] { "Titel", "Författare", "Placering", "Tillgänglighet" };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method that gets and fills list with articles from database
        /// </summary>
        /// <returns></returns>
        public async Task FillArticleData()
        {
            //Indicating that data is loading
            IoC.CreateInstance<ApplicationViewModel>().IsLoading = true;

            //Fills the list of articles, gets all articles when not given a parameter
            BookList = ViewModelHelpers.FillWithModelData<IArticle, ArticleViewModel>(await IoC.CreateInstance<ApplicationViewModel>().rep.SearchArticles()).ToObservableCollection();

            //Sets the CurrentList in TableControlViewModel to the list of articles
            IoC.CreateInstance<TableControlViewModel>().CurrentList = BookList.FillPlaceHolders().ToList().ToObservableCollection();

            //Indicating that data is loaded
            IoC.CreateInstance<ApplicationViewModel>().IsLoading = false;
        }

        #endregion
    }
}
