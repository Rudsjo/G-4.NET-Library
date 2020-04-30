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
    public class Article
    {
        /// <summary>
        /// Title of the article
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// This article's ISBN
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Author of the article
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The category of this article>
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// This article's release date
        /// </summary>
        public DateTime Published { get; set; }

    }
}
