using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    /// <summary>
    /// View model of <see cref="Category"/>
    /// </summary>
    public class CategoryViewModel : ICategory
    {
        #region Standard Properties

        /// <summary>
        /// THe id of the the category
        /// </summary>
        public int categoryID { get; set; }

        /// <summary>
        /// The type of the property
        /// </summary>
        public string type { get; set; }

        #endregion
    }
}
