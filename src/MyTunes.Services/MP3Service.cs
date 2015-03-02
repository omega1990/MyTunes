using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Services.ServiceContracts;
using MyTunes.Data.Repositories;
using MyTunes.Data.EntityModel;
using MyTunes.Data.UnitOfWork;
using System.Data.Entity;

namespace MyTunes.Services
{
    public class MP3Service : IMP3Service
    {
        private readonly IRepository<MP3> _repository;
        private readonly IUnitOfWork _uow;

        public MP3Service(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.Mp3Repository;
        }

        public IList<Data.EntityModel.MP3> GetAll()
        {
            return _repository.GetAll();
        }

        public MP3 Get(int id)
        {
            return _repository.Get(x => x.MP3ID.Equals(id)); 
        }

        public void Create(MP3 entity)
        {
            IList<Playlist> attachedPlaylists = new List<Playlist>();
            foreach(var playlist in entity.Playlist)
            {
                attachedPlaylists.Add(_uow.PlaylistRepository.GetAll().FirstOrDefault(x => x.PlaylistID == playlist.PlaylistID));
            }

            entity.Playlist.Clear();
            entity.Playlist = attachedPlaylists;

            _repository.Create(entity);
            Save();
        }

        public void Update(MP3 entity)
        {
            var attachedEntity = _uow.Mp3Repository.Get(x => x.MP3ID == entity.MP3ID);
            IList<Playlist> attachedPlaylists = new List<Playlist>();
            attachedEntity.Playlist.Clear();
            foreach (var playlist in entity.Playlist)
            {
                attachedEntity.Playlist.Add(_uow.PlaylistRepository.GetAll().FirstOrDefault(x => x.PlaylistID == playlist.PlaylistID));
            }

            attachedEntity.Album = entity.Album;
            attachedEntity.Artist = entity.Artist;
            attachedEntity.Title = entity.Title;
            attachedEntity.Year = entity.Year;

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
