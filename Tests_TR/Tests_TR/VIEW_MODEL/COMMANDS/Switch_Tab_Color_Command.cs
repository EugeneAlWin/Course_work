using System;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Switch_Tab_Color_Command()
        {
            return new(curr_tub => //Tab color
            {
                byte cur_tub_num = Convert.ToByte(curr_tub);
                tab_Header_Color[cur_tub_num] = tab_Right;
            });
        }
    }
}
