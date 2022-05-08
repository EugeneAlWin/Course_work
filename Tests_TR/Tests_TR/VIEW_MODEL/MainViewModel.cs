using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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


        public static ObservableCollection<Questions> Questions { get => questions; set => questions = value; }
        public static ObservableCollection<string> Login_Password { get => login_Password; set => login_Password = value; }
        public static ObservableCollection<string> IsActive_Buttons { get => isActive_Buttons; set => isActive_Buttons = value; }
        public static ObservableCollection<byte> SelectedIndex { get => selectedIndex; set => selectedIndex = value; }
        public static ObservableCollection<Style> Tab_Header_Color { get => tab_Header_Color; set => tab_Header_Color = value; }

        #region Admin_Region
        public static ObservableCollection<User> Users { get => users; set => users = value; }
        public static List<Questions> Questions_Test_0 { get => questions_Test_0; set => questions_Test_0 = value; } //exam
        public static List<Questions> Questions_Test_1 { get => questions_Test_1; set => questions_Test_1 = value; }
        public static List<Questions> Questions_Test_2 { get => questions_Test_2; set => questions_Test_2 = value; }
        public static List<Questions> Questions_Test_3 { get => questions_Test_3; set => questions_Test_3 = value; }
        public static List<Questions> Questions_Test_4 { get => questions_Test_4; set => questions_Test_4 = value; }
        public static List<Questions> Questions_Test_5 { get => questions_Test_5; set => questions_Test_5 = value; }
        public static List<Questions> Questions_Test_6 { get => questions_Test_6; set => questions_Test_6 = value; }
        public static List<Questions> Questions_Test_7 { get => questions_Test_7; set => questions_Test_7 = value; }
        #endregion

        #region Command_Region
        public static RelayCommand Switch_Page { get => switch_Page; }
        public static RelayCommand To_Test { get => to_Test; }
        public static RelayCommand Switch_Tab { get => switch_Tab; }
        public static RelayCommand Switch_Tab_Color { get => switch_Tab_Color; }
        public static RelayCommand Submit_Answer { get => submit_Answer; }
        public static RelayCommand Log_In_Out { get => log_In_Out; }
        public static RelayCommand Update_Database { get => update_Database; }

        #endregion
        public PagesViewModel()
        {

        }

    }


    public partial class PagesViewModel //Fields
    {
        private static readonly DatabaseContext db = new();

        private static readonly Login_Page login_Page = new(); //Awailable page
        private static readonly Main_Page main_Page = new(); //Awailable page
        private static readonly Testing_Page testing_Page = new(); //Awailable page
        private static readonly Admin_Page admin_Page = new(); //Awailable page
        private static readonly Rules_Page rules_Page = new(); //Awailable page
        private static Page current_Page = rules_Page;

        #region Test_Region
        private static readonly Style tab_Wrong = (Style)Application.Current.FindResource("Tab_Header_Wrong");
        private static readonly Style tab_Untouched = (Style)Application.Current.FindResource("Tab_Header_Untouched");
        private static readonly Style tab_Right = (Style)Application.Current.FindResource("Tab_Header_Right");

        private string given_Answer = "";
        private static readonly string[] tests_List = { "0", "1", "2", "3", "4", "5", "6", "7", }; //list of chapters (0=exam

        private static ObservableCollection<byte> selectedIndex = new() { 0 }; //Index of tab
        private static ObservableCollection<Style> tab_Header_Color = new();
        private static ObservableCollection<string> login_Password = new() { "", "" };
        private static ObservableCollection<string> isActive_Buttons = new();
        private static ObservableCollection<Questions> questions = new();

        private static List<Page> pages_List = new() { login_Page, main_Page, testing_Page, admin_Page, rules_Page };//0, 1, 2, 3, 4
        private static List<string> correct_Answers = new();
        private static List<string> given_Answers = new();
        #endregion

        #region Admin_Region
        private static ObservableCollection<User> users = db.Users.Local.ToObservableCollection();
        private static List<Questions> questions_Test_0 = db.Questions.Where(question => question.Test.Id == 0).ToList();
        private static List<Questions> questions_Test_1 = db.Questions.Where(question => question.Test.Id == 1).ToList();
        private static List<Questions> questions_Test_2 = db.Questions.Where(question => question.Test.Id == 2).ToList();
        private static List<Questions> questions_Test_3 = db.Questions.Where(question => question.Test.Id == 3).ToList();
        private static List<Questions> questions_Test_4 = db.Questions.Where(question => question.Test.Id == 4).ToList();
        private static List<Questions> questions_Test_5 = db.Questions.Where(question => question.Test.Id == 5).ToList();
        private static List<Questions> questions_Test_6 = db.Questions.Where(question => question.Test.Id == 6).ToList();
        private static List<Questions> questions_Test_7 = db.Questions.Where(question => question.Test.Id == 7).ToList();
        #endregion

        private static bool app_started_flag = false;

    }


    public partial class PagesViewModel //Commands' fields
    {
        #region Command_Region
        private static readonly RelayCommand log_In_Out = Log_In_Out_Command();
        private static readonly RelayCommand switch_Page = Switch_Page_Command();
        private static readonly RelayCommand to_Test = To_Test_Command();
        private static readonly RelayCommand switch_Tab = Switch_Tab_Command();
        private static readonly RelayCommand switch_Tab_Color = Switch_Tab_Color_Command();
        private static readonly RelayCommand submit_Answer = Submit_Answer_Command();
        private static readonly RelayCommand update_Database = Update_Database_Command();
        #endregion
    }

    public partial class PagesViewModel : INotifyPropertyChanged //INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
