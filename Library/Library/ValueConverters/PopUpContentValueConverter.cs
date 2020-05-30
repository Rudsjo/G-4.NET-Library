using Library.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Library
{
    /// <summary>
    /// Value converter to convert a <see cref="PopupContents"/> to a <see cref="UserControl"/>
    /// </summary>
    public class PopUpContentValueConverter : BaseValueConverter<PopUpContentValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PopUpContents)value)
            {
                case PopUpContents.None:
                    return null;

                case PopUpContents.DatabaseLogin:
                    return new DatabaseLoginControl();

                case PopUpContents.UserLogin:
                    return new UserLoginControl();

                case PopUpContents.AddUser:
                    return new AddUserControl();

                case PopUpContents.AddArticle:
                    return new AddArticleControl();

                case PopUpContents.Loading:
                    return new LoadingControl();

                case PopUpContents.Confirmation:
                    return new ConfirmationControl();

                case PopUpContents.Success:
                    return new SuccessfulControl();

                case PopUpContents.Info:
                    return new ArticleInfoControl();

                case PopUpContents.Edit:
                    return new ArticleEditControl();

                case PopUpContents.Filter:
                    return new ArticleFilterControl();

                case PopUpContents.UserInfo:
                    return new UserEditControl();

                case PopUpContents.Notification:
                    return new NotificationControl();

                case PopUpContents.MyProfile:
                    return new MyProfileControl();

                case PopUpContents.ReturnLoans:
                    return new ReturnLoansControl();

                case PopUpContents.ChangePassword:
                    return new ChangePasswordControl();

                case PopUpContents.ReportFilter:
                    return new ReportFilterControl();

                case PopUpContents.Downloading:
                    return new DownloadingControl();

                default:
                    return null;
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
