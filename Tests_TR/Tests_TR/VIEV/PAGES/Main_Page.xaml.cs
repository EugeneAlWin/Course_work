using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Tests_TR.VIEV.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
            DataContext = new PagesVievModel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Testing_Page());
        }
    }
}
