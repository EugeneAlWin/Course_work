using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests_TR.MODEL
{
    internal class Users_Repository : IRepository<User>
    {
        private readonly DatabaseContext db;
        public Users_Repository(DatabaseContext context) => db = context;
        public IEnumerable<User> GetAll() => db.Users;

        public ObservableCollection<User> GetAllToObservableCollection() => db.Users.Local.ToObservableCollection();
    }
}