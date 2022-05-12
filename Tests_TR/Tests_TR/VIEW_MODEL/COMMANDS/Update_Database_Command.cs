using System.Windows;

namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Update_Database_Command()
        {
            return new(cap =>
          {
              Save_Data();
          });
        }
        private static async void Save_Data()
        {
            try
            {
                await db.SaveChangesAsync();
                MessageBox.Show("Данные успешно добавлены!", "Успех!");
            }
            catch (System.Exception)
            {
                MessageBox.Show("Ошибка при обновлении базы данных.\nПроверьте корректность данных!", "Ошибка сохранения!");
            }
        }
    }
}
