using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    /// <summary>
    /// The class to get the roles from the database
    /// </summary>
    public class Role : IRole
    {
        /// <summary>
        /// The roleID 
        /// </summary>
        public int roleID { get; set; }

        /// <summary>
        /// The type of the role
        /// </summary>
        public string type { get; set; }
    }
}
