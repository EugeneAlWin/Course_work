using System.Windows.Controls;

namespace Tests_TR.VIEW.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
            DataContext = new PagesViewModel();
        }
    }
}
