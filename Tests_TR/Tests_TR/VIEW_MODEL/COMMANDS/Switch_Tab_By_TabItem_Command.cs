namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Switch_Tab_By_TabItem_Command()
        {
            return new(next_tab => //Tab nav
            {
                Table_With_Answer[0] = Table_With_Answers[selectedIndex[0]];
                current_Table_Color[0] = table_Colors[selectedIndex[0]];
            });
        }
    }
}
