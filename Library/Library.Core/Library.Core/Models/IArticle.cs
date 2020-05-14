namespace Library.Core
{
    public interface IArticle
    {
        int articleID { get; set; }
        string author { get; set; }
        int categoryID { get; set; }
        string description { get; set; }
        string isbn { get; set; }
        int loanTime { get; set; }
        string placement { get; set; }
        float price { get; set; }
        string publisher { get; set; }
        int statusID { get; set; }
        string title { get; set; }

        string edition { get; set; }
    }
}