using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Tests_TR.VIEV_MODEL;
using Tests_TR.VIEV.PAGES;
using System.Collections.Generic;

namespace Tests_TR
{

    public abstract class Pages
    {
        public static Login_Page login_Page = new();
        public static Main_Page main_Page = new();
        public static Page Current_Page { get; set; }
        public static RelayCommand Switch_Page { get; set; }

        public static List<Page> Pages_List { get; set; } = new() { login_Page, main_Page };


    }
    public class MainWindowViewModel : Pages
    {


        public MainWindowViewModel()
        {
            Current_Page = login_Page;
        }

    }

    public class PagesVievModel : Pages
    {
        public PagesVievModel()
        {
            Switch_Page = new RelayCommand(obj =>
            {
                NavigationService.GetNavigationService(Current_Page).Navigate(obj);
                Current_Page = (Page)obj;
            });
        }
    }
}
