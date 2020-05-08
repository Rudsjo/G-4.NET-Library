namespace Library.Core
{
    /// <summary>
    /// Required nemspaces
    /// </summary>
    #region Namespaces
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Reflection;
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

        #region List Extensions

        /// <summary>
        /// Extension method to convert a <see cref="IEnumerable{T}"/> to an <see cref="ObservableCollection{T}"/>
        /// </summary
        /// <typeparam name="T">The type the list is filled with</typeparam>
        /// <typeparam name="L">The type of list</typeparam>
        /// <param name="listToConvert">The lsit to convert</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T, L>(this L listToConvert)
            where L : IEnumerable<T>
        {
            // Creating the observable collection to return
            var collectionToReturn = new ObservableCollection<T>();

            // Getting base functionality from the List class to perform the convertion
            var listToConvertAsList = (listToConvert as List<T>);

            // Get all values from the sent in list and send them into the collection
            listToConvertAsList.ForEach((x) =>
            {
                collectionToReturn.Add(x);
            });

            // Returning
            return collectionToReturn;
        }

        /// <summary>
        /// Extension method to fill a list with placeholder dummy data
        /// </summary>
        /// <typeparam name="T">The type of the list content</typeparam>
        /// <typeparam name="L">The type of the list</typeparam>
        /// <param name="listToFill">The list to fill up</param>
        /// <param name="maxNumberOfItems">The maximum number of items</param>
        /// <returns></returns>
        public static ObservableCollection<T> FillPlaceHolders<T, L>(this L listToFill, int maxNumberOfItems)
            where L : IEnumerable, new()
            where T : new()
        {
            // Creating a nee temporary list
            var tempList = new List<T>();

            // Filling all the existing items
            foreach (var item in listToFill)
                tempList.Add((T)item);

            // Adding remaining dummies
            while (tempList.Count < maxNumberOfItems)
                tempList.Add(new T());

            // Returns the list as an observable collection
            return tempList.ToObservableCollection<T, List<T>>();
        }

        #endregion

    }
}
