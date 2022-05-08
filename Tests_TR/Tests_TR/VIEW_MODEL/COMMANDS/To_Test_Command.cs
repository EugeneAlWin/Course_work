using System;
using System.Linq;
using System.Windows.Navigation;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand To_Test_Command()
        {
            return new(num_of_test => //Tests (page) navigation
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
                var questions_Count = db.Questions.Where(p => p.Test.Topic == Convert.ToInt32(num_of_test)).Count();
                var random_Range = Enumerable.Range(0, questions_Count).OrderBy(x => Guid.NewGuid()).Take(10).ToList();
                var questions_Range = db.Questions.Where(p => p.Test.Topic == Convert.ToInt32(num_of_test)).ToList();

                foreach (var item in random_Range)
                {
                    questions.Add(questions_Range[item]);
                }
                foreach (var item in questions_Range)
                {
                    correct_Answers.Add(item.Right_Answer);
                }
            });
        }
    }
}
