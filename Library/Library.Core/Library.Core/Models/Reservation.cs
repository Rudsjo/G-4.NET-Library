using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class Reservation
    {
        public string userID { get; set; }
        public int articleID { get; set; }
        public string Title { get; set; }
        public int statusID { get; set; }
    }
}
