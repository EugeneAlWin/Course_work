using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
