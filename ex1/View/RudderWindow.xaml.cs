using System.Windows;
using System.Windows.Controls;

namespace ex1.View
{
    /// <summary>
    /// Interaction logic for RudderWindow.xaml
    /// </summary>
    public partial class RudderWindow : UserControl
    {
            //Window Initializer
        public RudderWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).RudderVM;
        }
    }
}
