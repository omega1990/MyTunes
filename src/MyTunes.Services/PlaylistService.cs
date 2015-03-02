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
            IList<MP3> attachedMp3s = new List<MP3>();
            foreach (var mp3 in entity.MP3)
            {
                attachedMp3s.Add(_uow.Mp3Repository.GetAll().FirstOrDefault(x => x.MP3ID == mp3.MP3ID));
            }

            entity.MP3.Clear();
            entity.MP3 = attachedMp3s;
            _repository.Create(entity);
            Save();
        }

        public void Update(Playlist entity)
        {
            var attachedEntity = _repository.Get(x => x.PlaylistID == entity.PlaylistID);
            IList<Playlist> attachedMp3s = new List<Playlist>();
            attachedEntity.MP3.Clear();
            foreach (var mp3 in entity.MP3)
            {
                attachedEntity.MP3.Add(_uow.Mp3Repository.GetAll().FirstOrDefault(x => x.MP3ID == mp3.MP3ID));
            }

            attachedEntity.Name = entity.Name;
            Save();
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
