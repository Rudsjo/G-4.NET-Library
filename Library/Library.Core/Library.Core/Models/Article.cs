namespace Library.Core
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using System;
    using Library.Core;
    #endregion

    /// <summary>
    /// This is a model of an article in
    /// the library's system
    /// </summary>
    public class Article : IArticle
    {
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
        /// The edition of the article
        /// </summary>
        public string edition { get; set; }

        /// <summary>
        /// The reason for an eventual removal
        /// </summary>
        public int reasonID { get; set; }

    }
}
