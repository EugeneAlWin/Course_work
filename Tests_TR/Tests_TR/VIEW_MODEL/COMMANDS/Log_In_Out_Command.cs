using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Log_In_Out_Command()
        {
            return new(next_page =>
            {
                NavigationService? NavService = NavigationService.GetNavigationService(current_Page);

                if (next_page == login_Page)  //Logout
                {
                    var temp_Result = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Выход из аккаунта", MessageBoxButton.YesNo);
                    if (temp_Result == MessageBoxResult.No) return;
                    real_Password = "";
                    fake_Password_Length = 0;
                    login_Password[0] = "";
                    login_Page.Password.Text = "";
                    NavService.Navigate(next_page);
                    selectedIndex[0] = 0; current_Page = (Page)next_page;
                }

                if (current_Page == login_Page) //Login
                {
                    if (login_Password[0] == "" || real_Password == "") return;

                    if (login_Password[0] == "supasupauserlogin" && real_Password == "supasupauserpassword") //supauser
                    {
                        NavService.Navigate((Page)pages_List[3]);
                        current_Page = (Page)pages_List[3];
                        return;
                    }

                    var temp_Acsess = Users_Admin.Where(x => x.Login == login_Password[0] && x.Password == ComputeHash(real_Password)).FirstOrDefault();

                    if (temp_Acsess != null)
                    {
                        var temp_Role = unit_Of_Work.Users.GetAll()
                        .Where(p => p.Login == login_Password[0] && p.Password == ComputeHash(real_Password))
                        .Where(p => p.Role.ToLower() == "admin").FirstOrDefault();

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
                        MessageBox.Show("Неверно введен логин или пароль", "Ошибка авторизации!");
                    }

;
                }
            }
           );
        }
    }
}