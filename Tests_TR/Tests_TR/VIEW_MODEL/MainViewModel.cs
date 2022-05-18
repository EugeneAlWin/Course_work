using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Tests_TR.MODEL;
using Tests_TR.VIEW.PAGES;
namespace Tests_TR
{

    public partial class PagesViewModel //Props
    {


        private static string real_Password = "";
        private static byte fake_Password_Length = 0;
        public static string Fake_Password
        {
            get
            {
                string temp_string = "";
                for (byte i = 0; i < real_Password.Length; i++) temp_string += "*";
                return temp_string;
            }
            set
            {
                real_Password = value.Length < fake_Password_Length ? real_Password[..value.Length] : real_Password + value[fake_Password_Length..];
                fake_Password_Length = (byte)value.Length;
            }
        }

        public static ObservableCollection<string> Given_Answer
        {
            get => given_Answer;
            set => given_Answer = value;
        }
        public static string PF
        {
            get { return pF; }
            set
            {
                if (value == "1" || value == "2" || value == "3" || value == "4" || value == "")
                {
                    pF = value;
                    Given_Answer[selectedIndex[0]] = pF;
                }
                else return;

            }
        }

        public static Page Current_Page { get => current_Page; set => current_Page = value; }
        public static List<Page> Pages_List { get => pages_List; set => pages_List = value; }


        #region Test_Region
        public static string[] Tests_List { get => tests_List; }
        public static ObservableCollection<Questions> Questions_For_Test { get => questions_For_Test; set => questions_For_Test = value; }
        public static ObservableCollection<string> Login_Password { get => login_Password; set => login_Password = value; }
        public static ObservableCollection<string> IsActive_Buttons { get => isActive_Buttons; set => isActive_Buttons = value; }
        public static ObservableCollection<sbyte> SelectedIndex { get => selectedIndex; set => selectedIndex = value; }
        public static ObservableCollection<Style> Tab_Header_Color { get => tab_Header_Color; set => tab_Header_Color = value; }
        public static ObservableCollection<Style> Current_Table_Color { get => current_Table_Color; set => current_Table_Color = value; }
        public static ObservableCollection<string> TimeSpanLabel { get => timeSpanLabel; set => timeSpanLabel = value; }

        public static ObservableCollection<string> Table_With_Answer { get; set; } = new() { "" };
        public static ObservableCollection<string> Table_With_Answers { get; set; } = new() { "", "", "", "", "", "", "", "", "", "", };
        #endregion

        #region Admin_Region
        public static ObservableCollection<User> Users_Admin { get => users_Admin; set => users_Admin = value; }
        public static ObservableCollection<Test> Tests_Admin { get => tests_Admin; set => tests_Admin = value; }
        public static ObservableCollection<Questions> Questions_Admin { get => questions_Admin; set => questions_Admin = value; }
        #endregion

        #region Command_Region
        public static RelayCommand Switch_Page { get => switch_Page; }
        public static RelayCommand To_Test { get => to_Test; }
        public static RelayCommand Switch_Tab { get => switch_Tab; }
        public static RelayCommand Switch_Tab_Color { get => switch_Tab_Color; }
        public static RelayCommand Submit_Answer { get => submit_Answer; }
        public static RelayCommand Log_In_Out { get => log_In_Out; }
        public static RelayCommand Update_Database { get => update_Database; }
        public static RelayCommand Switch_Tab_By_TabItem { get => switch_Tab_By_TabItem; }

        #endregion

        public PagesViewModel()
        {

        }

    }


    public partial class PagesViewModel //Fields
    {
        private static readonly Unit_Of_Work unit_Of_Work = new();
        private static readonly Login_Page login_Page = new(); //Awailable page
        private static readonly Main_Page main_Page = new(); //Awailable page
        private static Testing_Page testing_Page = new(); //Awailable page
        private static readonly Admin_Page admin_Page = new(); //Awailable page
        private static readonly Rules_Page rules_Page = new(); //Awailable page
        private static Page current_Page = login_Page;

        #region Test_Region
        private static readonly Style tab_Wrong = (Style)Application.Current.FindResource("Tab_Header_Wrong");
        private static readonly Style tab_Untouched = (Style)Application.Current.FindResource("Tab_Header_Untouched");
        private static readonly Style tab_Right = (Style)Application.Current.FindResource("Tab_Header_Right");

        private static readonly Style table_Wrong = (Style)Application.Current.FindResource("Table_Wrong");
        private static readonly Style table_Untouched = (Style)Application.Current.FindResource("Table_Untouched");
        private static readonly Style table_Right = (Style)Application.Current.FindResource("Table_Right");
        private static ObservableCollection<Style> current_Table_Color = new() { };
        private static readonly List<Style> table_Colors = new(10);
        private static ObservableCollection<string> given_Answer = new() { "" };

        private static readonly string[] tests_List = { "0", "1", "2", "3", "4", "5", "6", "7", }; //list of chapters (0=exam

        private static ObservableCollection<sbyte> selectedIndex = new() { 0 }; //Index of tab
        private static ObservableCollection<Style> tab_Header_Color = new();
        private static ObservableCollection<string> login_Password = new() { "", Fake_Password };
        private static ObservableCollection<string> isActive_Buttons = new();
        private static ObservableCollection<Questions> questions_For_Test = new();

        private static List<Page> pages_List = new() { login_Page, main_Page, testing_Page, admin_Page, rules_Page };//0, 1, 2, 3, 4
        private static readonly List<string> correct_Answers = new();
        private static List<string> given_Answers = new(10);

        private static ObservableCollection<string> timeSpanLabel = new() { "10:00" };

        private static readonly List<string> paragraphs_For_Test = new();
        private static bool IsTest_Started = false;
        private static string pF = "";


        #endregion

        #region Admin_Region
        private static ObservableCollection<User> users_Admin = unit_Of_Work.Users.GetAllToObservableCollection();
        private static ObservableCollection<Questions> questions_Admin = unit_Of_Work.Questions.GetAllToObservableCollection();
        private static ObservableCollection<Test> tests_Admin = unit_Of_Work.Tests.GetAllToObservableCollection();
        private static readonly SHA256 sha256 = SHA256.Create();

        #endregion

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
        private static readonly RelayCommand switch_Tab_By_TabItem = Switch_Tab_By_TabItem_Command();
        #endregion
    }

    public partial class PagesViewModel //ComputeHash
    {
        public static string ComputeHash(string str)
        {
            return Encoding.UTF8.GetString(sha256.ComputeHash(Encoding.UTF8.GetBytes(str)));
        }
    }
}
