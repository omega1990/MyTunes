///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 27.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Data.EntityModel;


namespace MyTunes.Data.Repositories
{
    public class GenericRepository <T> : IRepository<T> where T: class
    {
        internal MyTunesEntities entities;
        internal DbSet<T> dbSet;

        public GenericRepository(MyTunesEntities entities)
        {
            this.entities = entities;
            this.dbSet = entities.Set<T>();
        }

        public IList<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T Get(Func<T, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            entities.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if(entities.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void Save()
        {
            entities.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
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
