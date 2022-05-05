using System.Windows.Controls;

namespace Tests_TR.VIEW.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        public Login_Page()
        {
            InitializeComponent();
            DataContext = new PagesViewModel();
        }

    }
}
