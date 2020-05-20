using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    public class RoleViewModel : BaseViewModel, IRole
    {
        #region Properties
        /// <summary>
        /// The roleID
        /// </summary>
        public int roleID { get; set; }
        /// <summary>
        /// The Type of role
        /// </summary>
        public string type { get; set; }

        #endregion

    }
}
