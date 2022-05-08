using System.Windows.Controls;
using System.Windows.Navigation;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Switch_Page_Command()
        {
            return new(next_page => //Page navigation
            {
                NavigationService? NavService = NavigationService.GetNavigationService(current_Page);
                //MessageBoxResult mbr = MessageBox.Show("Вы действительно хотите прервать тест?\nРезультат будет утерян.", "Выйти?", MessageBoxButton.YesNo);       
                NavService.Navigate(next_page);
                selectedIndex[0] = 0; current_Page = (Page)next_page;
            });
        }
    }
}
