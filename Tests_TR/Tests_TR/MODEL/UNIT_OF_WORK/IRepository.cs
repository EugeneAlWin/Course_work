using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests_TR.MODEL
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        ObservableCollection<T> GetAllToObservableCollection();
    }
}
