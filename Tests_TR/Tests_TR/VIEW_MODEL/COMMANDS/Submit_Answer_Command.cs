namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Submit_Answer_Command()
        {
            return new(given_answer =>
           {
               given_Answers.Insert(selectedIndex[0], (string)given_answer); //int?
               isActive_Buttons[selectedIndex[0]] = "False";
           });
        }
    }
}
