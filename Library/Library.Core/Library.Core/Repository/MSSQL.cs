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
        /// <see cref="ILibraryRepository.AddCategory(string)"/>
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
        /// <see cref="ILibraryRepository.AddArticle(Article)"/>
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
                        placement = _article.placement,
                        edition = _article.edition
                    },
                    commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.AddUser(string, string, string, int, SecureString, string)"/>
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddUser(string PersonalNumber, string FirstName, string LastName, int RoleID, SecureString Password, string SaltBase64)
        {
            // Open a new connection to the databse.
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result as bool
                return (await Connection.QueryAsync<int>("AddUser", new {
                    personalNumber = PersonalNumber,
                    firstName      = FirstName,
                    lastName       = LastName,
                    roleID         = RoleID,
                    password       = Password.ToUnsecureString(),
                    salt           = SaltBase64
                }, commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        #endregion

        #region 'Delete' queries

        /// <summary>
        /// <see cref="ILibraryRepository.DeleteArticle(int)"/>
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
        /// <see cref="ILibraryRepository.DeleteCategory(int)"/>
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
        /// <see cref="ILibraryRepository.DeleteUser(string)"/>
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

        /// <summary>
        /// <see cref="ILibraryRepository.DeleteReservation(int)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public async Task<bool> DeleteReservation(int _articleID, string _personalNumber)
        {
            //Opens a new connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
                //return the result
                return (await Connection.QueryAsync<int>("DeleteReservation", new { articleID = _articleID, personalNumber = _personalNumber }, commandType: CommandType.StoredProcedure)).First() != 0;
        }

        #endregion

        #region 'Get' queries

        /// <summary>
        /// <see cref="ILibraryRepository.GetArticlesByStatus(int)"/>
        /// </summary>
        /// <param name="_status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetArticlesByStatus(int _status)
        {
            // Open a connectin to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Return the result
                return await Connection.QueryAsync<Article>("GetAllArticlesByStatus", new { statusID = _status }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.GetUserByID(string)"/>
        /// </summary>
        /// <param name="_personalNumber">The personalnumber.</param>
        /// <returns>
        /// The salt from that user
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetUserByID(string _personalNumber)
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Get the result from the database.
                var Result = await Connection.QueryAsync<string>("FindUser", new { PersonalNumber = _personalNumber }, commandType: CommandType.StoredProcedure);

                // Check if result contains any elements.
                return (Result.Count() == 0) ? null : Result.First();
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.GetAllRoles"/>
        /// </summary>
        /// <returns>Returns all the roles in the database</returns>
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Get the result from the database
                return await Connection.QueryAsync<Role>("GetAllRoles", new { }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.GetAllCategories"/>
        /// </summary>
        /// <returns>Returns all the roles in the database</returns>
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            // Open a connection to the database
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Get the result from the database
                return await Connection.QueryAsync<Category>("GetAllCategories", new { }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.GetUserLoans(string)"/>
        /// Returns a collection of the user's loaned articles. containing 
        /// <see cref="Article.articleID"/>, 
        /// <see cref="Article.loanTime"/>, 
        /// <see cref="Article.title"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <returns>
        /// </returns>
        public async Task<IEnumerable<Article>> GetUserLoans(string _personalNumber)
        {
            // Create a new connection
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Get and return the collection 
                return await Connection.QueryAsync<Article>("GetUserLoans", new { PersonalNumber = _personalNumber }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.GetUserReservations(string)"/>
        /// Returns a collection of the user's reserved articles. containing 
        /// <see cref="Article.articleID"/>, 
        /// <see cref="Article.dateTime"/>, 
        /// <see cref="Article.title"/>
        /// </summary>
        /// <param name="_personalNumber"></param>
        /// <returns>
        /// </returns>
        public async Task<IEnumerable<Article>> GetUserReservations(string _personalNumber)
        {
            // Create a new connection
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Get and return the collection 
                return await Connection.QueryAsync<Article>("GetUserReservations", new { PersonalNumber = _personalNumber }, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region 'Check' queries

        /// <summary>
        /// <see cref="ILibraryRepository.IsArticleReserved(int)"/>
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
        /// <see cref="ILibraryRepository.IsUserBlocked(string)"/>
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
        /// <see cref="ILibraryRepository.BlockUser(string, string)"/>
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
        /// <see cref="ILibraryRepository.UnblockUser(string)"/>
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
        /// <see cref="ILibraryRepository.BookSeminar(string, int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <param name="_seminarID"></param>
        /// <returns></returns>
        public Task<bool> BookSeminar(string _userID, int _seminarID) { throw new NotImplementedException(); }

        /// <summary>
        /// <see cref="ILibraryRepository.LoanArticle(string, int)"/>
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
        /// <see cref="ILibraryRepository.ReserveArticle(string, int, string)"/>
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
        /// <see cref="ILibraryRepository.ReturnArticle(int)"/>
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

        /// <summary>
        /// <see cref="ILibraryRepository.AttemptLogin(string, SecureString)"/>
        /// </summary>
        /// <param name="_personalNumber">The personal number.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public async Task<User> AttemptLogin(string _personalNumber, SecureString Password)
        {
            // Open a new connection to the database.
            using (SqlConnection Connection = CreateSQLConnection())
            {
                // Get the result from the database.
                var Result = await Connection.QueryAsync<User>("AttemptLogin", new { PersonalNumber = _personalNumber,
                                                                                     Password = Password.ToUnsecureString() }, commandType: CommandType.StoredProcedure);
                // Check if the result has an element, if not return null
                return (Result.Count() == 0) ? null : Result.First();
            }
        }

        #endregion

        #region 'Search' queries

        /// <summary>
        /// <see cref="ILibraryRepository.SearchArticles"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> SearchArticles(string _keyword = "")
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
        /// <see cref="ILibraryRepository.SearchUsers"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> SearchUsers(string _keyword = "")
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
        /// <see cref="ILibraryRepository.UpdateArticle(int, Article)"/>
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
                        articleID = _articleID               ,
                        title     = newArticle.title         ,
                        author    = newArticle.author        ,
                        publisher = newArticle.publisher     ,
                        isbn      = newArticle.isbn          ,
                        price       = newArticle.price       ,
                        categoryID  = newArticle.categoryID  ,
                        description = newArticle.description ,
                        loanTime  = newArticle.loanTime      ,
                        statusID  = newArticle.statusID      ,
                        placement = newArticle.placement     ,
                        edition = newArticle.edition
                    }, 
                    commandType: CommandType.StoredProcedure)).First() != 0;
            }
        }

        /// <summary>
        /// <see cref="ILibraryRepository.UpdateCategory(int, string)"/>
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
        /// <see cref="ILibraryRepository.UpdateUser(string, User)"/>
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
