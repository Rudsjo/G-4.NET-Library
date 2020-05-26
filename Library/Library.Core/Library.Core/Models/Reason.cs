using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    public class Reason
    {
        public int reasonID { get; set; }

        public string reason { get; }

        public ReasonTypes reasonType { get; set; }

    }
}
