using MyTunes.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes.Data.Repositories
{
    public class PlaylistRepository : IRepository<Playlist>
    {
        internal MyTunesEntities entities;
        internal DbSet<Playlist> dbSet;

        public PlaylistRepository(MyTunesEntities entities)
        {
            this.entities = entities;
            this.entities.Configuration.LazyLoadingEnabled = true;
            this.entities.Configuration.ProxyCreationEnabled = false;
            this.dbSet = entities.Set<Playlist>();
        }

        public IQueryable<Playlist> GetAll()
        {
            return dbSet
            .Include(x => x.MP3)
            .Select(x => x);
        }

        public Playlist Get(Func<Playlist, bool> predicate)
        {
            return dbSet
                .Include(x => x.MP3)
                .FirstOrDefault(predicate);
        }

        public void Create(Playlist entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Playlist entity)
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
