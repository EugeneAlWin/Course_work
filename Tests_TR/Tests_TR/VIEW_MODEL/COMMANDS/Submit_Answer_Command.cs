using System.Windows;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static byte correct_Answers_count = 0, clicks = 0;
        private static bool IsAnswer_Correct() => given_Answers[selectedIndex[0]] == correct_Answers[selectedIndex[0]];

        private static RelayCommand Submit_Answer_Command()
        {
            return new(given_answer_parameter => //Parameter
           {
               given_Answers[selectedIndex[0]] = ((string)given_answer_parameter);
               isActive_Buttons[selectedIndex[0]] = "False";
               tab_Header_Color[selectedIndex[0]] = IsAnswer_Correct() ? tab_Right : tab_Wrong;
               table_Colors[selectedIndex[0]] = IsAnswer_Correct() ? table_Right : table_Wrong;
               current_Table_Color[0] = table_Colors[selectedIndex[0]];
               pF = "";

               Table_With_Answers[selectedIndex[0]] = IsAnswer_Correct() ?
               $"Правильно!" :
               $"Неправильно! Правильный ответ: {correct_Answers[selectedIndex[0]]}. {paragraphs_For_Test[selectedIndex[0]]}!";

               Table_With_Answer[0] = Table_With_Answers[selectedIndex[0]];

               clicks += 1;

               if (clicks == 10)  //end of test
               {
                   Check_Test();
               }
           });
        }

        private static void Check_Test()
        {
            timer?.Dispose();

            for (byte i = 0; i < 10; i++)
                if (given_Answers[i] == correct_Answers[i]) correct_Answers_count += 1;

            string ex_or_test = selectedIndex[0] == 0 ? "Экзамен" : "Тест",
                 passed_or_not = correct_Answers_count >= 9 ? "СДАН" : "НЕ СДАН",
                 expexted_answers = "",
                 real_answers = "";

            foreach (var item in correct_Answers) expexted_answers += item + "-";

            foreach (var item in given_Answers) real_answers += item + "-";

            var Yes_Or_No = MessageBox.Show(
                $"{ex_or_test} {passed_or_not}!\n" +
                $"Правильных ответов: {correct_Answers_count} из 10\n" +
                $"Правильные ответы: {expexted_answers}\n" +
                $"Ваши ответы: {real_answers}\n" +
                $"Хотите закрыть тест?",
                "Результаты", MessageBoxButton.YesNo);

            correct_Answers_count = 0;
            clicks = 0;
            IsTest_Started = false;

            if (Yes_Or_No == MessageBoxResult.Yes) Switch_Page.Execute(main_Page);
            else return;
        }
    }
}