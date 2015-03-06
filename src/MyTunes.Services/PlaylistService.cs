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
            var data = _repository.GetAll().ToList();

              var filteredData = data.Select(x => new Playlist
                    {
                        PlaylistID = x.PlaylistID,
                        Name = x.Name,
                        MP3 = x.MP3.Select(y =>
                            new MP3
                            {
                                MP3ID = y.MP3ID,
                                Title = y.Title,
                                Artist = y.Artist,
                                Album = y.Album,
                                Year = y.Year,
                            }).ToList()
                    }).ToList();

            return filteredData;
        }

        public Playlist Get(int id)
        {
            return _repository.Get(x => x.PlaylistID.Equals(id));
        }

        public IList<Playlist> GetFiltered(string searchQuery)
        {
            var playlists = _repository.GetAll();
            var queriedPlaylists = new List<Playlist>();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();


                foreach (var playlist in playlists)
                {
                    if (playlist.Name.ToLower().Contains(searchQuery))
                    {
                        queriedPlaylists.Add(playlist);
                        continue;
                    }
                }
            }
            return queriedPlaylists;
        }

        public PagedModel<Playlist> GetPaged(int page)
        {
            int pageSize = 10;
            var playlists = GetAll();
            var playlistsPaged = playlists
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            var pagesCount = (int)Math.Ceiling((double)playlists.Count / (double)pageSize);
            IList<int> pages = new List<int>();

            for (int i = 0; i < pagesCount; i++)
                pages.Add(0);

            var nextActive = true;
            var previousActive = true;

            page++;
            if (page <= 1)
            {
                page = 1;
                previousActive = false;
            }
            if (page >= pages.Count)
            {
                page = pages.Count;
                nextActive = false;
            }

            var pagedData = new PagedModel<Playlist>(playlistsPaged, pages, page, nextActive, previousActive);
            return pagedData;
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
