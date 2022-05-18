using System.Windows;

namespace Tests_TR.MODEL
{
    internal class Unit_Of_Work
    {
        private readonly DatabaseContext db = new();
        private Questions_Repository? questions_Repository;
        private Tests_Repository? tests_Repository;
        private Users_Repository? users_Repository;

        public Questions_Repository Questions { get => questions_Repository ??= new(db); }
        public Tests_Repository Tests { get => tests_Repository ??= new(db); }
        public Users_Repository Users { get => users_Repository ??= new(db); }

        public async void SaveChangesAsync()
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
