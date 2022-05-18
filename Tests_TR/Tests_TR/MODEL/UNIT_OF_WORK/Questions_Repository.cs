using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests_TR.MODEL
{
    internal class Questions_Repository : IRepository<Questions>
    {
        private readonly DatabaseContext db;
        public Questions_Repository(DatabaseContext context) => db = context;
        public IEnumerable<Questions> GetAll() => db.Questions;

        public ObservableCollection<Questions> GetAllToObservableCollection() => db.Questions.Local.ToObservableCollection();
    }
}