using System.Windows;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Close_App_Command() => new(cap =>
        {
            unit_Of_Work.Dispose();
            Application.Current.Shutdown();
        });
    }
}
