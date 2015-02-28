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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Data.EntityModel;
using MyTunes.Data.Repositories;


namespace MyTunes.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //TODO: Transactions
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
        
        public void Commit()
        {
            entities.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected void Dispose(bool disposing)
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

    }
}
