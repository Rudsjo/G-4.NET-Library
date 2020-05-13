using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    /// <summary>
    /// View model of the <see cref="Article"/> model
    /// to be displayed and managed in the UI
    /// </summary>
    public class ArticleViewModel : BaseViewModel, IArticle
    {
        #region Core Properties

        /// <value>
        /// The article identifier.
        /// </value>
        public int articleID { get; set; }

        /// <value>
        /// The category identifier.
        /// </value>
        public int categoryID { get; set; }

        /// <value>
        /// The status identifier.
        /// </value>
        public int statusID { get; set; }


        /// <summary>
        /// Title of the article
        /// </summary>
        public string title { get; set; }

        /// <value>
        /// The price.
        /// </value>
        public float price { get; set; }

        /// <value>
        /// The isbn.
        /// </value>
        public string isbn { get; set; }

        /// <summary>
        /// Author of the article
        /// </summary>
        public string author { get; set; }

        /// <value>
        /// The publisher.
        /// </value>
        public string publisher { get; set; }

        /// <value>
        /// The placement.
        /// </value>
        public string placement { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// This article's release date
        /// </summary>
        public int loanTime { get; set; }

        /// <summary>
        /// The articles edition
        /// </summary>
        public string edition { get; set; }

        #endregion

        #region Added Properties

        /// <summary>
        /// Flag to indicate if the object is a real user or not
        /// </summary>
        public bool IsPlaceholder { get; set; }

        /// <summary>
        /// The quantity of the article
        /// </summary>
        public string quantity { get; set; }

        /// <summary>
        /// The number of articles available
        /// </summary>
        public string availability { get; set; }

        /// <summary>
        /// Flag to indicate if animation should take place
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        #endregion
    }
}
