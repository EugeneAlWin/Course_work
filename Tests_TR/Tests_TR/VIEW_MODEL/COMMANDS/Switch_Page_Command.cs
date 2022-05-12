using System.Windows;
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
                if (Current_Page == testing_Page)
                {
                    var result = MessageBox.Show("Вы действительно хотите прервать тест?", "Прервать тест?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No) return;
                    timer?.Dispose();
                }

                NavService.Navigate(next_page);
                selectedIndex[0] = 0; current_Page = (Page)next_page;
            });
        }
    }
}
