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

        public PlaylistService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.PlaylistRepository;
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
            _repository.Create(entity);
        }

        public void Update(Playlist entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            Save();
        }

        public void Save()
        {
            _uow.Commit();
        }
    }
}
