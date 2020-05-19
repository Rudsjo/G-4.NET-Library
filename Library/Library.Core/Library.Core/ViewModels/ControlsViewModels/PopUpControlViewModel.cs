using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    public class PopUpControlViewModel : BaseViewModel
    {
        #region Public Properties

        public PopUpContents PopUpContent { get; set; } = PopUpContents.None;

        #endregion

    }
}
