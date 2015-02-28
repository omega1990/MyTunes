using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Data.EntityModel;
using MyTunes.Data.Repositories;


namespace MyTunes.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MyTunesEntities entities = new MyTunesEntities();
        private GenericRepository<MP3> mp3Repository;
        private GenericRepository<Playlist> playlistRepository;

        public GenericRepository<MP3> Mp3Repository
        {
            get
            {
                if(this.mp3Repository == null)
                {
                    this.mp3Repository = new GenericRepository<MP3>(entities);
                }
                return mp3Repository;
            }
        }

        public GenericRepository<Playlist> PlaylistRepository
        {
            get
            {
                if (this.playlistRepository == null)
                {
                    this.playlistRepository = new GenericRepository<Playlist>(entities);
                }
                return playlistRepository;
            }
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
            throw new NotImplementedException();
        }
    }
}
