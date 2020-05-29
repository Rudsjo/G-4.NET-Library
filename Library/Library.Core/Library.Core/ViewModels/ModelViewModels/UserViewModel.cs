﻿using System;
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
        [ReflectionSkipper]
        public bool IsPlaceholder { get; set; }

        /// <summary>
        /// The number of loaned articles the user has
        /// </summary>
        /// 
        [ReflectionSkipper]
        public string loanedArticles { get; set; }

        /// <summary>
        /// The number of reserved articles the user has
        /// </summary>
        /// 
        [ReflectionSkipper]
        public string reservedArticles { get; set; }

        /// <summary>
        /// Property to check if a user is blocked
        /// </summary>
        /// 
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Counts the number of reservations that are ready for loan ( is at the top of reservationslist)
        /// </summary>
        /// 
        [ReflectionSkipper]
        public int NumberOfNotifications { get; set; }
        #endregion
    }
}
