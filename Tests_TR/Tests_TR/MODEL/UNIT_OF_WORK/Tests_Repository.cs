using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests_TR.MODEL
{
    internal class Tests_Repository : IRepository<Test>
    {
        private readonly DatabaseContext db;
        public Tests_Repository(DatabaseContext context) => db = context;
        public IEnumerable<Test> GetAll() => db.Tests;
        public ObservableCollection<Test> GetAllToObservableCollection() => db.Tests.Local.ToObservableCollection();
    }
}