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
        private MyTunesEntities _entities;
        private MP3Repository mp3Repository;
        private PlaylistRepository playlistRepository;

        public UnitOfWork()
        {
            _entities = new MyTunesEntities();
            _entities.Configuration.LazyLoadingEnabled = false;
            //_entities.Configuration.ProxyCreationEnabled = false;
        }

        public MP3Repository Mp3Repository
        {
            get
            {
                if(this.mp3Repository == null)
                {
                    this.mp3Repository = new MP3Repository(_entities);
                }
                return mp3Repository;
            }
        }
        public PlaylistRepository PlaylistRepository
        {
            get
            {
                if (this.playlistRepository == null)
                {
                    this.playlistRepository = new PlaylistRepository(_entities);
                }
                return playlistRepository;
            }
        }
        
        public void Commit()
        {
            _entities.SaveChanges();
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
                    _entities.Dispose();
                }
            }
            this.disposed = true;
        }

    }
}
