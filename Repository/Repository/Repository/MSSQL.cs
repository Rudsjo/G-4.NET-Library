namespace Repository
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Security;
    using System.Threading.Tasks;
    using Library.Core;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    #endregion

    /// <summary>
    /// Backend class that uses Microsoft SQL Server,
    /// it is designed to work with our library database.
    /// </summary>
    public class MSSQL : ILibraryRepository
    {
        /// <summary>
        /// The connection string encrypted in memory
        /// </summary>
        private SecureString ConnectionString;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQL"/> class.
        /// </summary>
        public MSSQL()
        {
            /* TEMPERÄR LÖSNING, CONNECTIONSTRINGEN SKA SPARAS I EN SEPARAT
             * REDIGERBAR FIL SOM SEDAN SKA KRYPTERAS DÅ DEN INEHÅLLER LÖSENORD
             * OCH ANVÄNDARNAMN! DEN SKA ABSOLUT INTE LIGGA HÅRDKODAD!
             */
            ConnectionString = (
                    "Server  = sql5053.site4now.net; "           +
                    "Database = DB_A5DF2A_LibrarySystem; "       +
                    "User Id  = DB_A5DF2A_LibrarySystem_admin; " +
                    "Password = Grupp42020; "
                ).ToSecureString();
        }
        #endregion

        /// <summary>
        /// Creates the SQL connection.
        /// </summary>
        /// <returns></returns>
        private SqlConnection CreateSQLConnection()
        =>
        new SqlConnection(ConnectionString.ToUnsecureString());

        #region 'Add' queries

        /// <summary>
        /// <see cref="IRepository.AddCategory(string)"/>
        /// </summary>
        /// <param name="_categoryName"></param>
        /// <returns></returns>
        public async Task<bool> AddCategory(string _categoryName)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
                // Return the result
                return (await Connection.QueryAsync<int>("AddCategory", new { type = _categoryName }, commandType: CommandType.StoredProcedure)).First() != 0;

        }

        /// <summary>
        /// <see cref="IRepository.AddArticle(Article)"/>
        /// </summary>
        /// <param name="_article"></param>
        /// <returns></returns>
        public async Task<bool> AddArticle(Article _article)
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                return (await Connection.QueryAsync<int>("AddArticle",
                    new
                    {
                        title = _article.title,
                        author = _article.author,
                        publisher = _article.publisher,
                        isbn = _article.isbn,
                        price = _article.price,
                        categoryID = _article.categoryID,
                        description = _article.description,
                        loanTime = _article.loanTime,
                        statusID = _article.statusID,
                        placement = _article.placement
                    },
                    commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.AddUser"/>
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddUser()
        {
            throw new NotImplementedException();

        }

        #endregion

        #region 'Delete' queries

        /// <summary>
        /// <see cref="IRepository.DeleteArticle(int)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public async Task<bool> DeleteArticle(int _articleID)
        {
            // Open a new connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
                // return the result
                return ( await Connection.QueryAsync<int>("DeleteArticle", new { articleID = _articleID }, commandType: CommandType.StoredProcedure)).First() != 0;
        }

        /// <summary>
        /// <see cref="IRepository.DeleteCategory(int)"/>
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCategory(int _categoryID)
        {
            // Open a new connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
                // return the result
                return (await Connection.QueryAsync<int>("DeleteArticle", new { categoryID = _categoryID }, commandType: CommandType.StoredProcedure)).First() != 0;
        }

        /// <summary>
        /// <see cref="IRepository.DeleteUser(string)"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string _personalNumber)
        {
            // Open a new connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
                // return the result
                return (await Connection.QueryAsync<int>("DeleteUser", new { personalNumber = _personalNumber }, commandType: CommandType.StoredProcedure)).First() != 0;
        }

        #endregion

        #region 'Get' queries

        /// <summary>
        /// <see cref="IRepository.GetArticlesByStatus(int)"/>
        /// </summary>
        /// <param name="_status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetArticlesByStatus(int _status)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return await Connection.QueryAsync<Article>("AddUser", new { }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        #endregion

        #region 'Check' queries

        /// <summary>
        /// <see cref="IRepository.IsArticleReserved(int)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public async Task<bool> IsArticleReserved(int _articleID)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("CheckIfArticleIsReserved", new { ArticleID = _articleID }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.IsUserBlocked(string)"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <returns></returns>
        public async Task<bool> IsUserBlocked(string _personalNumber)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("CheckIfUserIsBlocked", new { personalNumber = _personalNumber }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        #endregion

        #region 'Action' queries

        /// <summary>
        /// <see cref="IRepository.BlockUser(string, string)"/>
        /// </summary>
        /// <param name="_personalNumber">PersonalNumber of the user</param>
        /// <param name="_reason">The reason for blocking this user</param>
        /// <returns></returns>
        public async Task<bool> BlockUser(string _personalNumber, string _reason)
        {
            // Open a connectin to the database
            using (IDbConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("BlockUser", new { PersonalNumber = _personalNumber, Reason = _reason }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.UnblockUser(string)"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <returns></returns>
        public async Task<bool> UnblockUser(string _personalNumber)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("UnblockUser", new { personalNumber = _personalNumber }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.BookSeminar(string, int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <param name="_seminarID"></param>
        /// <returns></returns>
        public Task<bool> BookSeminar(string _userID, int _seminarID) { throw new NotImplementedException(); }

        /// <summary>
        /// <see cref="IRepository.LoanArticle(string, int)"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public async Task<bool> LoanArticle(string _personalNumber, int _articleID)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("LoanArticle", new { UserID = _personalNumber, ArticleID = _articleID }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.ReserveArticle(string, int, string)"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <param name="_articleID"></param>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        public async Task<bool> ReserveArticle(string _personalNumber, int _articleID, string _dateTime)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("ReserveArticle", new { UserID = _personalNumber, ArticleID = _articleID, DateTime = _dateTime }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.ReturnArticle(int)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public async Task<bool> ReturnArticle(int _articleID)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return (await Connection.QueryAsync<int>("ReturnArticle", new { ArticleID = _articleID }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        #endregion

        #region 'Search' queries

        /// <summary>
        /// <see cref="IRepository.SearchArticles"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> SearchArticles(string _keyword)
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                var Articles = (await Connection.QueryAsync<Article>("SearchArticle", new { SearchKey = _keyword }, commandType: CommandType.StoredProcedure));

                // Return an empty collection if no result was found
                return (Articles == null || Articles.Count() == 0) ? new List<Article>() : Articles;
            }
        }

        /// <summary>
        /// <see cref="IRepository.SearchUsers"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> SearchUsers(string _keyword)
        {
            // Open a connection to the database
            using(SqlConnection Connection = CreateSQLConnection())
            {            
                var Users = (await Connection.QueryAsync<User>("SearchUser", new { SearchKey = _keyword }, commandType: CommandType.StoredProcedure));

                // Return an empty collection if no result was found
                return (Users == null || Users.Count() == 0) ? new List<User>() : Users;
            }
        }

        #endregion

        #region 'Update' queries

        /// <summary>
        /// <see cref="IRepository.UpdateArticle(int, Article)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <param name="newArticle"></param>
        /// <returns></returns>
        public async Task<bool> UpdateArticle(int _articleID, Article newArticle)
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                return (await Connection.QueryAsync<int>("UpdateArticle", 
                    new {
                        title     = newArticle.title         ,
                        author    = newArticle.author        ,
                        publisher = newArticle.publisher     ,
                        isbn      = newArticle.isbn          ,
                        price       = newArticle.price       ,
                        categoryID  = newArticle.categoryID  ,
                        description = newArticle.description ,
                        loanTime  = newArticle.loanTime      ,
                        statusID  = newArticle.statusID      ,
                        placement = newArticle.placement 
                    }, 
                    commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.UpdateCategory(int, string)"/>
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <param name="_newCategory"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCategory(int _categoryID, string _newCategory)
        {
            // Open a connection to the database
            using(SqlConnection Connection = CreateSQLConnection())
            {
                return (await Connection.QueryAsync<int>("UpdateCategory", new { categoryID = _categoryID, type = _newCategory }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="IRepository.UpdateUser(string, User)"/>
        /// </summary>
        /// <param name="_UserID"></param>
        /// <param name="_newUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(string _UserID, User _newUser)
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                return (await Connection.QueryAsync<int>("UpdateUser", 
                    new 
                    {
                        personalNumber = _newUser.personalNumber,
                        firstName = _newUser.firstName,
                        lastName = _newUser.lastName,
                        roleID = _newUser.roleID
                    }, 
                    commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        #endregion
    }
}
