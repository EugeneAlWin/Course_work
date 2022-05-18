using System.Windows;

namespace Tests_TR.MODEL
{
    internal class Unit_Of_Work
    {
        private readonly DatabaseContext db = new();
        private Questions_Repository? questions_Repository;
        private Tests_Repository? tests_Repository;
        private Users_Repository? users_Repository;

        public Questions_Repository Questions
        {
            get
            {
                if (questions_Repository == null) questions_Repository = new(db);
                return questions_Repository;
            }
        }
        public Tests_Repository Tests
        {
            get
            {
                if (tests_Repository == null) tests_Repository = new(db);
                return tests_Repository;
            }
        }
        public Users_Repository Users
        {
            get
            {
                if (users_Repository == null) users_Repository = new(db);
                return users_Repository;
            }
        }

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
