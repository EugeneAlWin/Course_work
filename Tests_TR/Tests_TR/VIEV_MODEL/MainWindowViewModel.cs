using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tests_TR.VIEV.PAGES;
namespace Tests_TR
{

    public abstract class Pages_Props
    {
        private static readonly Login_Page login_Page = new();
        private static readonly Main_Page main_Page = new();
        private static readonly Testing_Page testing_page = new();
        private static readonly Admin_Page admin_page = new();
        public static string[] Tests_List { get; } = { "0", "1", "2", "3", "4", "5", "6", "7", };
        public static Page Current_Page { get; set; } = login_Page;
        public static List<Page> Pages_List { get; set; } = new() { login_Page, main_Page, testing_page, admin_page };

        internal static RelayCommand switch_Page = new(next_page => { NavigationService.GetNavigationService(Current_Page).Navigate(next_page); Current_Page = (Page)next_page; });
        internal static RelayCommand to_Test = new(num_of_test => { NavigationService.GetNavigationService(Current_Page).Navigate(testing_page); Current_Page = testing_page; MessageBox.Show(num_of_test.ToString()); });
        public static RelayCommand? Switch_Page { get; set; }
        public static RelayCommand? To_Test { get; set; }

    }

    public class PagesVievModel : Pages_Props
    {
        public PagesVievModel()
        {
            Switch_Page = switch_Page;
            To_Test = to_Test;
        }
    }
}
