using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace product.stock.api
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<List<T>> List();
        T Select(long id);
        void Insert(T obj);
        void Delete(long id);
        void Update(T obj);
        void Save();
    }
}
