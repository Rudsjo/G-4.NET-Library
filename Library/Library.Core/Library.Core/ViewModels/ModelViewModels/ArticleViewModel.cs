using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Reason for an eventual removal
        /// </summary>
        [ReflectionSkipper]
        public int reasonID { get; set; }

        #endregion

        #region Added Properties

        /// <summary>
        /// Flag to indicate if the object is a real user or not
        /// </summary>
        [ReflectionSkipper]
        public bool IsPlaceholder { get; set; }

        /// <summary>
        /// The quantity of the article
        /// </summary>
        [ReflectionSkipper]
        public int quantity { get; set; }

        /// <summary>
        /// The number of articles available
        /// </summary>
        [ReflectionSkipper]
        public string availability { get; set; }

        /// <summary>
        /// Flag to indicate if animation should take place
        /// </summary>
        [ReflectionSkipper]
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// Flag to indicate if to show loan article button
        /// </summary>
        [ReflectionSkipper]
        private bool _availableToLoanVisibility;

        [ReflectionSkipper]
        public bool AvailableToLoanVisibility
        {
            get
            {
                if (statusID == 1)
                    _availableToLoanVisibility = true;
                else
                    _availableToLoanVisibility = false;

                return _availableToLoanVisibility;

            }
            set { }
        }

        /// <summary>
        /// Flag to indicate if to show reserve article button
        /// </summary>
        [ReflectionSkipper]
        private bool _availableToReserveVisibility;

        [ReflectionSkipper]
        public bool AvailableToReserveVisibility
        {
            get
            {
                // Check if this book is loaned by the user that is logged in
                if (!IsLoanedByCurrentUser)
                {
                    // Check if the article is available
                    if (statusID == 2 || statusID == 4)
                        _availableToReserveVisibility = true;
                    // If not, then hide the reserve button 
                    else
                        _availableToReserveVisibility = false;
                }
                // If it is, then hide the reserve button
                else _availableToReserveVisibility =  false;

                return _availableToReserveVisibility;
            }
            set { }
        }

        /// <summary>
        /// A flag that indicates if the reserve button should be hidden from the current user.
        /// </summary>
        /// 
        [ReflectionSkipper]
        public bool IsLoanedByCurrentUser { get; set; }
        #endregion

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            // Create a new instance of this class
            ArticleViewModel vm = new ArticleViewModel();

            #region Copy all properties from this class to the new one
            vm.articleID                    = this.articleID;
            vm.availability                 = this.availability;
            vm.AvailableToLoanVisibility    = this.AvailableToLoanVisibility;
            vm.AvailableToReserveVisibility = this.AvailableToReserveVisibility;
            vm.categoryID                   = this.categoryID;
            vm.IsLoanedByCurrentUser        = this.IsLoanedByCurrentUser;
            vm.IsPlaceholder                = this.IsPlaceholder;
            vm.loanTime                     = this.loanTime;
            vm.price                        = this.price;
            vm.quantity                     = this.quantity;
            vm.reasonID                     = this.reasonID;
            vm.statusID                     = this.statusID;
            vm.ShouldAnimateOut             = this.ShouldAnimateOut;
            vm.title       = new string(this.title);
            vm.publisher   = new string(this.publisher);
            vm.placement   = new string(this.placement);
            vm.edition     = new string(this.edition);
            vm.description = new string(this.description);
            vm.author      = new string(this.author);
            vm.isbn        = new string(this.isbn);
            #endregion

            // Return the new instance
            return vm;
        }
    }
}
