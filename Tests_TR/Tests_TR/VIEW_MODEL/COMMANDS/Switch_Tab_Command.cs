using System;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Switch_Tab_Command()
        {
            return new(next_tab => //Tab nav
            {
                sbyte temp = (Convert.ToBoolean(next_tab) ? ++selectedIndex[0] : --selectedIndex[0]);

                if (selectedIndex[0] > 9) selectedIndex[0] = 0;
                else if (selectedIndex[0] < 0) selectedIndex[0] = 9;

                Table_With_Answer[0] = Table_With_Answers[selectedIndex[0]];
                current_Table_Color[0] = table_Colors[selectedIndex[0]];
            });
        }
    }
}