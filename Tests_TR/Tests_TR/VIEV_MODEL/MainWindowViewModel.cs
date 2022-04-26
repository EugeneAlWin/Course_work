using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tests_TR.VIEV.PAGES;
using Tests_TR.VIEV_MODEL;

namespace Tests_TR
{

    public abstract class Pages_Props
    {
        private static readonly Login_Page login_Page = new();
        private static readonly Main_Page main_Page = new();
        private static readonly Testing_Page testing_page = new();
        private static readonly Admin_Page admin_page = new();
        public static Page Current_Page { get; set; } = login_Page;
        public static List<Page> Pages_List { get; set; } = new() { login_Page, main_Page, testing_page, admin_page };

        internal static RelayCommand switch_Page = new(obj => { NavigationService.GetNavigationService(Current_Page).Navigate(obj); Current_Page = (Page)obj; });
        public static RelayCommand? Switch_Page { get; set; }

    }

    public class PagesVievModel : Pages_Props
    {
        public PagesVievModel()
        {
            Switch_Page = switch_Page;
        }
    }
}
