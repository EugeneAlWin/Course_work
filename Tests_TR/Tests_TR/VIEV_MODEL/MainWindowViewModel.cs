using System.Collections.Generic;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tests_TR.VIEV.PAGES;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Tests_TR
{

    public class PagesVievModel : Pages_Props
    {
        public static string[] Tests_List { get => tests_List; }
        public static List<Page> Pages_List { get => pages_List; set => pages_List = value; }
        public static ObservableCollection<int> SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        public static ObservableCollection<Style> Tab_Header_Color { get => tab_Header_Color; set => tab_Header_Color = value; }

        public static Page Current_Page { get => current_Page; set => current_Page = value; }
        public static RelayCommand Switch_Page { get => switch_Page; }
        public static RelayCommand To_Test { get => to_Test; set => to_Test = value; }
        public static RelayCommand Switch_Tab { get => switch_Tab; set => switch_Tab = value; }
        public static RelayCommand Switch_Tab_Color { get => switch_Tab_Color; set => switch_Tab_Color = value; }
        public PagesVievModel()
        {

        }
    }

    public abstract class Pages_Props : INotifyPropertyChanged
    {

        private static readonly Login_Page login_Page = new(); //Awailable page
        private static readonly Main_Page main_Page = new(); //Awailable page
        private static readonly Testing_Page testing_Page = new(); //Awailable page
        private static readonly Admin_Page admin_Page = new(); //Awailable page

        private static readonly Style tab_Wrong = (Style)Application.Current.FindResource("Tab_Header_Wrong");
        private static readonly Style tab_Untouched = (Style)Application.Current.FindResource("Tab_Header_Untouched");
        private static readonly Style tab_Right = (Style)Application.Current.FindResource("Tab_Header_Right");

        internal static readonly string[] tests_List = { "0", "1", "2", "3", "4", "5", "6", "7", }; //list of chapters (0=exam)

        internal static ObservableCollection<int> selectedIndex = new() { 0 }; //Index of tab

        internal static ObservableCollection<Style> tab_Header_Color = new() { tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, };

        internal static Page current_Page = login_Page;
        internal static List<Page> pages_List = new() { login_Page, main_Page, testing_Page, admin_Page };

        internal static readonly RelayCommand switch_Page = new(next_page => //Page navigation
        {
            //MessageBoxResult mbr = MessageBox.Show("Вы действительно хотите прервать тест?\nРезультат будет утерян.", "Выйти?", MessageBoxButton.YesNo);
            NavigationService.GetNavigationService(current_Page).Navigate(next_page);
            selectedIndex[0] = 0; current_Page = (Page)next_page;
        });

        internal static RelayCommand to_Test = new(num_of_test => //Tests navigation
        {
            NavigationService.GetNavigationService(current_Page).Navigate(testing_Page);
            current_Page = testing_Page;
            for (int i = 0; i <= 9; i++) tab_Header_Color.Insert(i, tab_Untouched);
        });

        internal static RelayCommand switch_Tab = new(next_tab => //Tab nav
        {
            var temp = Convert.ToBoolean(next_tab) ? ++selectedIndex[0] : --selectedIndex[0];
            if (selectedIndex[0] > 9) selectedIndex[0] = 0;
            if (selectedIndex[0] < 0) selectedIndex[0] = 9;
        });
        internal static RelayCommand switch_Tab_Color = new(curr_tub => //Tab color
        {
            var cur_tub_num = Convert.ToByte(curr_tub);
            tab_Header_Color[cur_tub_num] = tab_Right;
        });





        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
