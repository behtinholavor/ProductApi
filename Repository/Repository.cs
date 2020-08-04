using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace product.stock.api
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected StockContext Db;
        protected DbSet<T> DbSet;
        protected DbConnection DbConn;

        public Repository(StockContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
            DbConn = context.Database.GetDbConnection();
        }        

        public Task<List<T>> List()
        {
            return DbSet.ToListAsync();
        }

        public T Select(long id)
        {
            return DbSet.Find(id);
        }       

        public void Insert(T obj)
        {
            DbSet.Add(obj);
            Save();
        }

        public void Delete(long id)
        {
            T obj = DbSet.Find(id);
            DbSet.Remove(obj);
            Save();
        }

        public void Update(T obj)
        {
            DbSet.Update(obj);
            Save();
        }

        public void Save()
        {
            Db.SaveChanges();
        }     

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    Db.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
