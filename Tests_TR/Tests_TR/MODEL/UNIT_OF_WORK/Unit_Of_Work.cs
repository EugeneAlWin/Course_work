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

        public void Dispose() => db.Dispose();

        public async void SaveChangesAsync()
        {
            try
            {
                await db.SaveChangesAsync();
                MessageBox.Show("Данные успешно добавлены!", "Успех!");
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Проверьте правильность введенных данных!" +
                    $"\nОграничения USER:"
                    + "\nЛогин: больше 4 символов, уникален, необходим."
                    + "\nПароль: больше 4 символов, уникален необходим."
                    + "\nРоль: admin или user, необходима."
                    + "\nФамилия: необходима, больше 0 и меньше 30 символов."
                    + "\nИмя: необходимо, больше 0 и меньше 30 символов."
                    + "\n"
                    + "\nОграничения Questions:"
                    + "\nID теста: от 0 до 7 включительно."
                    + "\nВопрос: больше 0 символов."
                    + "\nОтвет1: больше 0 символов."
                    + "\nОтвет2: больше 0 символов."
                    + "\nПравильный ответ: 1 символ.", "Ошибка сохранения!"
                    );
            }
        }
    }
}