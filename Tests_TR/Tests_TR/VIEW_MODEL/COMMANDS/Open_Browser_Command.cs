using System.Diagnostics;
using System.Windows;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Open_Browser_Command() => new(cap =>
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://vk.com/justeugene10011") { UseShellExecute = true });
            }
            catch (System.Exception)
            {
                MessageBox.Show("Ошибка открытия браузера!");
            }
        });
    }
}
