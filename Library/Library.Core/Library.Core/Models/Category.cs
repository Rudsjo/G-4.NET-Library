using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    /// <summary>
    /// The category model
    /// </summary>
    public class Category : ICategory
    {
        /// <summary>
        /// The id of the category
        /// </summary>
        public int categoryID { get; set; }

        /// <summary>
        /// The type of the category
        /// </summary>
        public string type { get; set; }
    }
}
