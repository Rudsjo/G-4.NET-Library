namespace Repository
{
    /// <summary>
    /// Required namespaces
    /// </summary>
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using Library.Core;
    using static RepositoryHelpers;
    #endregion

    /// <summary>
    /// Backend class that uses Microsoft SQL Server
    /// </summary>
    public class MSSQL : IRepository
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MSSQL()
        {
            
        }
        #endregion

        #region 'Add' queries

        /// <summary>
        /// <see cref="IRepository.AddArticle(Article)"/>
        /// </summary>
        /// <param name="_article"></param>
        /// <returns></returns>
        public bool AddArticle(Article _article)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.AddCategory(string)"/>
        /// </summary>
        /// <param name="_categoryName"></param>
        /// <returns></returns>
        public bool AddCategory(string _categoryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.AddUser"/>
        /// </summary>
        /// <returns></returns>
        public bool AddUser()
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
        public bool DeleteArticle(int _articleID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.DeleteCategory(int)"/>
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <returns></returns>
        public bool DeleteCategory(int _categoryID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.DeleteUser(int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public bool DeleteUser(int _userID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 'Get' queries

        /// <summary>
        /// <see cref="IRepository.GetArticlesByStatus(int)"/>
        /// </summary>
        /// <param name="_status"></param>
        /// <returns></returns>
        public object GetArticlesByStatus(int _status)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 'Check' queries

        /// <summary>
        /// <see cref="IRepository.IsArticleReserved(int)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public bool IsArticleReserved(int _articleID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.IsUserBlocked(int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public bool IsUserBlocked(int _userID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 'Action' queries

        /// <summary>
        /// <see cref="IRepository.BlockUser(int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public bool BlockUser(int _userID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.BookSeminar(int, int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <param name="_seminarID"></param>
        /// <returns></returns>
        public bool BookSeminar(int _userID, int _seminarID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.LoanArticle(int, int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public bool LoanArticle(int _userID, int _articleID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.ReserveArticle(int, int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public bool ReserveArticle(int _userID, int _articleID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.ReturnArticle(int)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <returns></returns>
        public bool ReturnArticle(int _articleID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.UnblockUser(int)"/>
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public bool UnblockUser(int _userID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 'Search' queries

        /// <summary>
        /// <see cref="IRepository.SearchArticles"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Article> SearchArticles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.SearchUsers"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> SearchUsers()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 'Update' queries

        /// <summary>
        /// <see cref="IRepository.UpdateArticle(int, Article)"/>
        /// </summary>
        /// <param name="_articleID"></param>
        /// <param name="newArticle"></param>
        /// <returns></returns>
        public bool UpdateArticle(int _articleID, Article newArticle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.UpdateCategory(int, string)"/>
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <param name="_newCategory"></param>
        /// <returns></returns>
        public bool UpdateCategory(int _categoryID, string _newCategory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRepository.UpdateUser(int, User)"/>
        /// </summary>
        /// <param name="_UserID"></param>
        /// <param name="_newUser"></param>
        /// <returns></returns>
        public bool UpdateUser(int _UserID, User _newUser)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
