using System;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Switch_Tab_Command()
        {
            return new(next_tab => //Tab nav
            {
                byte temp = Convert.ToBoolean(next_tab) ? ++selectedIndex[0] : --selectedIndex[0];
                if (selectedIndex[0] > 9) selectedIndex[0] = 0;
                else if (selectedIndex[0] < 0) selectedIndex[0] = 9;
            });
        }
    }
}
