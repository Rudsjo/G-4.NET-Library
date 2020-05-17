using Library.Core;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library
{
    /// <summary>
    /// Interaction logic for AddArticleControl.xaml
    /// </summary>
    public partial class AddArticleControl : UserControl
    {
        public AddArticleControl()
        {
            InitializeComponent();


            DataContext = IoC.CreateInstance<AddArticleControlViewModel>();
        }


        private void KeyBinding_Changed(object sender, System.EventArgs e)
        {
            FirstBox.Focus();
        }
    }
}
