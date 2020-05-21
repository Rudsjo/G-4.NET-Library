namespace Repository
{
    using System.Configuration;

    /// <summary>
    /// An internal helper class for the
    /// repository
    /// </summary>
    internal static class RepositoryHelpers
    {
        /// <summary>
        /// Reads the connectionString from the App.config
        /// </summary>
        /// <returns></returns>
        internal static string GetConnectionString() 
        => 
        ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;

    }
}
