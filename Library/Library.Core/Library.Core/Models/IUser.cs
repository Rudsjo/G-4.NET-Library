using System;

namespace Library.Core
{
    public interface IUser : ICloneable
    {
        string firstName { get; set; }
        string lastName { get; set; }
        string personalNumber { get; set; }
        int roleID { get; set; }
        string type { get; set; }
    }
}