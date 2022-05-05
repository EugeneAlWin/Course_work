using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tests_TR.MODEL;
using Tests_TR.VIEW.PAGES;
namespace Tests_TR
{

    public partial class PagesViewModel //Props
    {
        public string Given_Answer
        {
            get => given_Answer; set
            {
                if (value == "1" || value == "2" || value == "3" || value == "4" || value == "")
                    given_Answer = value;

                else return;
                OnPropertyChanged("Given_Answer");
            }
        }
        public static string[] Tests_List { get => tests_List; }
        public static Page Current_Page { get => current_Page; set => current_Page = value; }
        public static List<Page> Pages_List { get => pages_List; set => pages_List = value; }

        public static ObservableCollection<User> Users { get => users; set => users = value; }

        public static ObservableCollection<Questions> Questions { get => questions; set => questions = value; }
        public static ObservableCollection<string> Login_Password { get => login_Password; set => login_Password = value; }
        public static ObservableCollection<string> IsActive_Buttons { get => isActive_Buttons; set => isActive_Buttons = value; }
        public static ObservableCollection<byte> SelectedIndex { get => selectedIndex; set => selectedIndex = value; }
        public static ObservableCollection<Style> Tab_Header_Color { get => tab_Header_Color; set => tab_Header_Color = value; }

        public static RelayCommand Switch_Page { get => switch_Page; }
        public static RelayCommand To_Test { get => to_Test; }
        public static RelayCommand Switch_Tab { get => switch_Tab; }
        public static RelayCommand Switch_Tab_Color { get => switch_Tab_Color; }
        public static RelayCommand Submit_Answer { get => submit_Answer; }
        public static RelayCommand Log_In_Out { get => log_In_Out; }
        public static RelayCommand Update_Database { get => update_Database; }

        public static async void Load_Data()
        {
            await db.Users.LoadAsync();
            await db.Questions.LoadAsync();
            await db.Tests.LoadAsync();
        }
        public static async void Save_Data()
        {
            try
            {
                await db.SaveChangesAsync();
                MessageBox.Show("Данные успешно добавлены!", "Успех!");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при обновлении базы данных.\n");
            }
        }
        public PagesViewModel()
        {

        }
        static PagesViewModel()
        {

            Load_Data();
        }
    }


    public partial class PagesViewModel : INotifyPropertyChanged //Fields
    {

        private static readonly Login_Page login_Page = new(); //Awailable page
        private static readonly Main_Page main_Page = new(); //Awailable page
        private static readonly Testing_Page testing_Page = new(); //Awailable page
        private static readonly Admin_Page admin_Page = new(); //Awailable page
        private static Page current_Page = login_Page;

        private static readonly Style tab_Wrong = (Style)Application.Current.FindResource("Tab_Header_Wrong");
        private static readonly Style tab_Untouched = (Style)Application.Current.FindResource("Tab_Header_Untouched");
        private static readonly Style tab_Right = (Style)Application.Current.FindResource("Tab_Header_Right");

        private string given_Answer = "";
        private static readonly string[] tests_List = { "0", "1", "2", "3", "4", "5", "6", "7", }; //list of chapters (0=exam)

        private static ObservableCollection<byte> selectedIndex = new() { 0 }; //Index of tab

        private static ObservableCollection<Style> tab_Header_Color = new();
        private static ObservableCollection<string> login_Password = new() { "", "" };
        private static ObservableCollection<string> isActive_Buttons = new();
        private static ObservableCollection<Questions> questions = new();


        private static List<Page> pages_List = new() { login_Page, main_Page, testing_Page, admin_Page };
        private static List<string> correct_Answers = new();
        private static List<string> given_Answers = new();
        private static readonly DatabaseContext db = new();


        private static ObservableCollection<User> users = db.Users.Local.ToObservableCollection();




        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }


    public partial class PagesViewModel : INotifyPropertyChanged //Commands' fields
    {
        private static readonly RelayCommand log_In_Out = new(next_page =>
        {
            NavigationService? NavService = NavigationService.GetNavigationService(current_Page);

            db.Users.LoadAsync();
            if (next_page == login_Page)
            {
                login_Password[0] = login_Password[1] = "";
                NavService.Navigate(next_page);
                selectedIndex[0] = 0; current_Page = (Page)next_page;
            }
            if (current_Page == login_Page)
            {
                if (login_Password[0] == "" || login_Password[1] == "") return;

                var temp_Acsess = users.Where(x => x.Login == login_Password[0] && x.Password == login_Password[1]).FirstOrDefault();

                if (temp_Acsess != null)
                {
                    var temp_Role = db.Users.Where(p => p.Login == login_Password[0] && p.Password == login_Password[1]).Where(p => p.Role == "Admin").FirstOrDefault();
                    MessageBox.Show(temp_Role != null ? "Вы вошли как администратор" : "Вы вошли как студент", "Авторизация прошла успешно!");
                    if (temp_Role != null)
                    {
                        NavService.Navigate((Page)pages_List[3]);
                        current_Page = (Page)pages_List[3];
                    }
                    else
                    {
                        NavService.Navigate(next_page);
                        selectedIndex[0] = 0; current_Page = (Page)next_page;
                    }
                }
                else
                {
                    //MessageBox.Show("Неверно введен логин или пароль", "Ошибка авторизации!");
                    ///////////////////////////////////
                    NavService.Navigate(next_page);
                    selectedIndex[0] = 0; current_Page = (Page)next_page;
                    //////////////////////////////////
                };
            }

        }
        );

        private static readonly RelayCommand switch_Page = new(next_page => //Page navigation
        {
            NavigationService? NavService = NavigationService.GetNavigationService(current_Page);
            //MessageBoxResult mbr = MessageBox.Show("Вы действительно хотите прервать тест?\nРезультат будет утерян.", "Выйти?", MessageBoxButton.YesNo);       
            NavService.Navigate(next_page);
            selectedIndex[0] = 0; current_Page = (Page)next_page;
        });

        private static readonly RelayCommand to_Test = new(num_of_test => //Tests (page) navigation
        {
            NavigationService? NavService = NavigationService.GetNavigationService(current_Page);
            NavService.Navigate(testing_Page);
            current_Page = testing_Page;

            for (int i = 0; i <= 9; i++)
            {
                tab_Header_Color.Insert(i, tab_Untouched);
                isActive_Buttons.Insert(i, "True");
            };


            questions.Clear();
            var questions_Count = db.Questions.Where(p => p.Test.Id == Convert.ToInt32(num_of_test)).Count();
            var random_Range = Enumerable.Range(0, questions_Count).OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            var questions_Range = db.Questions.Where(p => p.Test.Id == Convert.ToInt32(num_of_test)).ToList();

            foreach (var item in random_Range)
            {
                questions.Add(questions_Range[item]);
            }
            foreach (var item in questions_Range)
            {
                correct_Answers.Add(item.Right_Answer);
            }
        });

        private static readonly RelayCommand switch_Tab = new(next_tab => //Tab nav
        {
            byte temp = Convert.ToBoolean(next_tab) ? ++selectedIndex[0] : --selectedIndex[0];
            if (selectedIndex[0] > 9) selectedIndex[0] = 0;
            else if (selectedIndex[0] < 0) selectedIndex[0] = 9;
        });
        private static readonly RelayCommand switch_Tab_Color = new(curr_tub => //Tab color
        {
            byte cur_tub_num = Convert.ToByte(curr_tub);
            tab_Header_Color[cur_tub_num] = tab_Right;
        });

        private static readonly RelayCommand submit_Answer = new(given_answer =>
          {
              given_Answers.Insert(selectedIndex[0], (string)given_answer);
              isActive_Buttons[selectedIndex[0]] = "False";
          });
        private static readonly RelayCommand update_Database = new(cap =>
         {
             Save_Data();
         });


    }
}
