using System.Windows.Controls;

namespace Tests_TR.VIEW.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Rules_Page.xaml
    /// </summary>
    public partial class Rules_Page : Page
    {
        public Rules_Page()
        {
            InitializeComponent();
            DataContext = new PagesViewModel();
        }
    }
}
