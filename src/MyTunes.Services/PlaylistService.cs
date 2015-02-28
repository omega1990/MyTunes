using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Data.EntityModel;
using MyTunes.Data.Repositories;
using MyTunes.Data.UnitOfWork;
using MyTunes.Services.ServiceContracts;

namespace MyTunes.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist> _repository;
        private readonly IUnitOfWork _uow;

        public PlaylistService(IRepository<Playlist> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public IList<Playlist> GetAll()
        {
            return _repository.GetAll();
        }

        public Playlist Get(int id)
        {
            return _repository.Get(x => x.PlaylistID.Equals(id));
        }

        public void Create(Playlist entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Playlist entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Playlist entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
