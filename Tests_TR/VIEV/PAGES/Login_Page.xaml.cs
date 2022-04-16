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
    /// Логика взаимодействия для Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        public Login_Page()
        {
            InitializeComponent();
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new Main_Page());
        }
    }
}
