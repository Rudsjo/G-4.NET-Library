using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Core
{
    public class AddArticleControlViewModel : BaseViewModel
    {
        #region Public Properties

        public string InputTitle { get; set; }

        public string InputAuthor { get; set; }

        public string InputPublisher { get; set; }

        public string InputISBN { get; set; }

        public string InputLoanTime { get; set; }

        public string InputPlacement { get; set; }

        public string InputPrice { get; set; }

        public string InputQuantity { get; set; }

        public ICommand AddToTempList { get; set; }

        public ObservableCollection<ArticleViewModel> TempListOfArticles { get; set; }

        // TODO: Add Category and Description properties

        #endregion

        public AddArticleControlViewModel()
        {
            TempListOfArticles = new ObservableCollection<ArticleViewModel>();
            AddToTempList = new RelayCommand(async () => await AddToTempListCommad());
        }

        #region Private Methods

        private async Task AddToTempListCommad()
        {
            TempListOfArticles.Add(new ArticleViewModel() { title = InputTitle });
        } 

        #endregion

    }
}
