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
    public class MP3Repository : IRepository<MP3>
    {
        internal MyTunesEntities entities;
        internal DbSet<MP3> dbSet;

        public MP3Repository(MyTunesEntities entities)
        {
            this.entities = entities;
            this.entities.Configuration.LazyLoadingEnabled = true;
            this.entities.Configuration.ProxyCreationEnabled = false;
            this.dbSet = entities.Set<MP3>();
        }

        public IQueryable<MP3> GetAll()
        {
            return dbSet
            .Include(x => x.Playlist)
            .Select(x => x);
        }

        public MP3 Get(Func<MP3, bool> predicate)
        {
            return dbSet
                .Include(x => x.Playlist)
                .FirstOrDefault(predicate);
        }

        public void Create(MP3 entity)
        {
            dbSet.Add(entity);
        }

        public void Update(MP3 entity)
        {

        }

        public void Delete(int id)
        {
            var entityToRemove = dbSet.Find(id);
            dbSet.Remove(entityToRemove);
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
