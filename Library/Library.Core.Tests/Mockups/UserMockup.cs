using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Tests
{
    /// <summary>
    /// Mockup class of the <see cref="User"/> class
    /// </summary>
    public class UserMockup : IUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personalNumber { get; set; }
        public int roleID { get; set; }
        public string type { get; set; }

        public bool IsPlaceholder { get; set; }
    }
}
