namespace Repository
{
    /// <summary>
    /// Required nemspaces
    /// </summary>
    #region Namespaces
    using System;
    using System.Net;
    using System.Security;
    using static System.Runtime.InteropServices.Marshal;
    #endregion

    /// <summary>
    /// Extension methods for the <see cref="SecureString"/> class.
    /// </summary>
    public static class Extensions
    {
        #region Extensions for the SecureStringClass

        /// <summary>
        /// Reveals the string of the SecureString
        /// </summary>
        /// <param name="_secureString">The secure string.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToUnsecureString(this SecureString _secureString)
        {         
            // Check if the secure string is null
            if (_secureString == null)
                return null;

            // Create a pointer
            IntPtr pointer = IntPtr.Zero;

            // Get the string from the secure string
            try
            {
                // Copy the content from the SecureString into unmanaged memory
                pointer = SecureStringToCoTaskMemUnicode(_secureString);

                // Return the content that the new pointer is pointing to
                return PtrToStringUni(pointer);
            }
            // Finally, clean up at the memory address 
            finally
            {
                // Free the memory
                ZeroFreeGlobalAllocUnicode(pointer);
            }

        }

        #endregion

        #region String extensions

        /// <summary>
        /// Converts to securestring.
        /// </summary>
        /// <param name="UnsecureString">The unsecure string.</param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string UnsecureString)
        {
            // Check if the string is null
            if (String.IsNullOrEmpty(UnsecureString))
                return null;

            // Create a SecureString
            var Result = new SecureString();
            // Append all characters from the string to the SecureString
            foreach (char c in UnsecureString) Result.AppendChar(c);

            // Return the new SecureString
            return Result;
        }

        #endregion

    }
}
