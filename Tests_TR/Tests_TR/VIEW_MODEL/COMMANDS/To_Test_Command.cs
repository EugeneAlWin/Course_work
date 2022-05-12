using System;
using System.Linq;
using System.Threading;
using System.Windows.Navigation;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static TimeSpan timeSpan;
        private static readonly TimerCallback tcb = new(Timer_Tick);
        private static readonly TimeSpan border = new(0, 0, 0);
        private static readonly TimeSpan interval = new(0, 0, 1);
        private static Timer? timer;

        static void Timer_Tick(object? o)
        {
            timeSpan = timeSpan.Subtract(interval);
            timeSpanLabel[0] = timeSpan.ToString("mm\\:ss");
            if (timeSpan.Equals(border))
            {
                if (IsTest_Started) Check_Test();
            }

        }
        private static RelayCommand To_Test_Command()
        {

            return new(num_of_test => //Tests (page) navigation
            {

                testing_Page = new();
                timeSpan = new(0, 10, 0);
                timeSpanLabel[0] = timeSpan.ToString("mm\\:ss");
                IsTest_Started = true;
                NavigationService? NavService = NavigationService.GetNavigationService(current_Page);
                NavService.Navigate(testing_Page);
                current_Page = testing_Page;
                given_Answers.Clear();
                questions_For_Test.Clear();
                correct_Answers.Clear();
                given_Answer.Clear();
                paragraphs_For_Test.Clear();
                Table_With_Answers.Clear();

                current_Table_Color.Insert(0, table_Untouched);

                for (int i = 0; i <= 9; i++)
                {
                    tab_Header_Color.Insert(i, tab_Untouched);
                    table_Colors.Insert(i, table_Untouched);
                    isActive_Buttons.Insert(i, "True");
                    given_Answer.Insert(i, "");
                    //given_Answers.Insert(i, "");
                    Table_With_Answers.Insert(i, "");

                };
                Table_With_Answer[0] = Table_With_Answers[selectedIndex[0]];
                var questions_From_Db = db.Questions.Where(p => p.Test.Topic == Convert.ToInt32(num_of_test));
                var questions_Count = questions_From_Db.Count();
                var random_Range = Enumerable.Range(0, questions_Count).OrderBy(x => Guid.NewGuid()).Take(10).ToList();
                var questions_Range = questions_From_Db.ToList();

                foreach (var item in random_Range)
                {
                    questions_For_Test.Add(questions_Range[item]);

                }

                foreach (var item in questions_For_Test)
                {
                    correct_Answers.Add(item.Right_Answer);
                    paragraphs_For_Test.Add(item.Paragraph);
                }
                timer = new(tcb, null, 0, 1000);
            });
        }
    }
}
