using System.Windows;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Close_App_Command() => new(cap =>
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Закрыть приложение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            unit_Of_Work.Dispose();
            Application.Current.Shutdown();
        });
    }
}
