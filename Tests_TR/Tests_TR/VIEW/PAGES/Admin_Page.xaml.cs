using System.Windows.Controls;

namespace Tests_TR.VIEW.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Admin_Page.xaml
    /// </summary>
    public partial class Admin_Page : Page
    {
        public Admin_Page()
        {
            InitializeComponent();
            DataContext = new PagesViewModel();
        }
    }
}
