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
            FillSearchableArticleList();

            //Fills list of articles when page loads
            IoC.CreateInstance<TableControlViewModel>().LoadItems();

            // Setting the dynamic texts
            IoC.CreateInstance<MainContentUserControlViewModel>().HeaderText = "Alla böcker";
            IoC.CreateInstance<MainContentUserControlViewModel>().AddButtonText = "Lägg till bok";
            IoC.CreateInstance<TableControlViewModel>().TableHeaderTexts = new string[] { "Titel", "Författare", "Placering", "Tillgänglighet" };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to get all searchable articles
        /// </summary>
        private async void FillSearchableArticleList()
        { 
            // Get the full list
            IoC.CreateInstance<MainContentUserControlViewModel>().ArticleSearchList = 
                (await IoC.CreateInstance<ApplicationViewModel>().rep.SearchArticles()).ToModelDataToViewModel<IArticle, ArticleViewModel>();
        }

        #endregion
    }
}
