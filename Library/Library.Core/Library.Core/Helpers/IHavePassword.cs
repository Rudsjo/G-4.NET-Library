using System.Security;

namespace Library.Core
{
    /// <summary>
    /// An interface for a class that can provide a secure password
    /// </summary>
    public interface IHavePassword
    {
        // The secure password
        SecureString SecurePassword { get; }
    }
}
