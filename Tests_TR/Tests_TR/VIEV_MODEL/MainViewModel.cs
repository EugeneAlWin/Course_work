using System.Collections.Generic;
using System.Windows;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tests_TR.VIEV.PAGES;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Tests_TR.MODEL;
namespace Tests_TR
{

    public partial class PagesVievModel //Props
    {

        public static string[] Tests_List { get => tests_List; }
        public ObservableCollection<string> Login_Password { get => login_Password; set => login_Password = value; }
        public static List<Page> Pages_List { get => pages_List; set => pages_List = value; }
        public static ObservableCollection<Questions> Questions { get => questions; set => questions = value; }
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


    public partial class PagesVievModel : INotifyPropertyChanged //Fields
    {

        private static readonly Login_Page login_Page = new(); //Awailable page
        private static readonly Main_Page main_Page = new(); //Awailable page
        private static readonly Testing_Page testing_Page = new(); //Awailable page
        private static readonly Admin_Page admin_Page = new(); //Awailable page
        private static Page current_Page = login_Page;

        private static readonly Style tab_Wrong = (Style)Application.Current.FindResource("Tab_Header_Wrong");
        private static readonly Style tab_Untouched = (Style)Application.Current.FindResource("Tab_Header_Untouched");
        private static readonly Style tab_Right = (Style)Application.Current.FindResource("Tab_Header_Right");

        private static readonly string[] tests_List = { "0", "1", "2", "3", "4", "5", "6", "7", }; //list of chapters (0=exam)

        private static ObservableCollection<int> selectedIndex = new() { 0 }; //Index of tab

        private static ObservableCollection<Style> tab_Header_Color = new() { tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, tab_Untouched, };
        private static ObservableCollection<string> login_Password = new() { "", "" };
        private static ObservableCollection<Questions> questions = new();


        private static List<Page> pages_List = new() { login_Page, main_Page, testing_Page, admin_Page };

        private static readonly DatabaseContext db = new();




        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }


    public partial class PagesVievModel //Commands' fields
    {
        private static readonly RelayCommand switch_Page = new(next_page => //Page navigation
        {
            //MessageBoxResult mbr = MessageBox.Show("Вы действительно хотите прервать тест?\nРезультат будет утерян.", "Выйти?", MessageBoxButton.YesNo);
            if (next_page == login_Page)
            {
                login_Password[0] = login_Password[1] = "";
            }
            if (current_Page == login_Page)
            {
                if (login_Password[0] == "" || login_Password[1] == "") return;


                var temp_Acsess = db.Users.Where(x => x.Login == login_Password[0] && x.Password == login_Password[1]).FirstOrDefault();

                if (temp_Acsess != null)
                {
                    var temp_Role = db.Users.Where(p => p.Login == login_Password[0] && p.Password == login_Password[1]).Where(p => p.Role == "Admin").FirstOrDefault();
                    MessageBox.Show(temp_Role != null ? "Вы вошли как администратор" : "Вы вошли как студент", "Авторизация прошла успешно!");
                }
                else MessageBox.Show("Неверно введен логин или пароль", "Ошибка авторизации!");
            }
            NavigationService.GetNavigationService(current_Page).Navigate(next_page);
            selectedIndex[0] = 0; current_Page = (Page)next_page;
        });

        private static RelayCommand to_Test = new(num_of_test => //Tests (page) navigation
        {
            NavigationService.GetNavigationService(current_Page).Navigate(testing_Page);
            current_Page = testing_Page;
            for (int i = 0; i <= 9; i++) tab_Header_Color.Insert(i, tab_Untouched);
            questions.Clear();

            var temp = db.Questions.Where(p => p.Test.Id == Convert.ToInt32(num_of_test)).Count();
            var random_Range = Enumerable.Range(0, temp).OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            var questions_Range = db.Questions.Where(p => p.Test.Id == Convert.ToInt32(num_of_test)).ToList();
            foreach (var item in random_Range)
            {
                questions.Add(questions_Range[item]);
            }


        });

        private static RelayCommand switch_Tab = new(next_tab => //Tab nav
        {
            var temp = Convert.ToBoolean(next_tab) ? ++selectedIndex[0] : --selectedIndex[0];
            if (selectedIndex[0] > 9) selectedIndex[0] = 0;
            if (selectedIndex[0] < 0) selectedIndex[0] = 9;
        });
        private static RelayCommand switch_Tab_Color = new(curr_tub => //Tab color
        {
            var cur_tub_num = Convert.ToByte(curr_tub);
            tab_Header_Color[cur_tub_num] = tab_Right;
        });
    }
}
