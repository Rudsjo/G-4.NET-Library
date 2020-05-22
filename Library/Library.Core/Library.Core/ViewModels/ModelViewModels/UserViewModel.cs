using System;
using System.Collections.Generic;
using System.Text;


namespace Library.Core
{
    /// <summary>
    /// View model of the <see cref="User"/> model 
    /// to be displayed and managed in the UI
    /// </summary>
    public class UserViewModel : BaseViewModel, IUser
    {
        #region Core Properties

        /// <summary>
        /// PersonalNumber of the user
        /// </summary>
        public string personalNumber { get; set; }

        /// <summary>
        /// Role ID
        /// </summary>
        public int roleID { get; set; }
        /// <summary>
        /// Role type string
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string lastName { get; set; }

        #endregion

        #region Added Properties

        /// <summary>
        /// Flag to indicate if the object is a real user or not
        /// </summary>
        public bool IsPlaceholder { get; set; }

        /// <summary>
        /// The number of loaned articles the user has
        /// </summary>
        public int loanedArticles { get; set; }

        /// <summary>
        /// The number of reserved articles the user has
        /// </summary>
        public int reservedArticles { get; set; }

        /// <summary>
        /// Property to check if a user is blocked
        /// </summary>
        public bool IsBlocked { get; set; }
        #endregion
    }
}
