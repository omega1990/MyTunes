﻿using System;
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
            var data = _repository.GetAll().ToList();
            
            var filteredData = data.Select(x => new MP3
                    {
                        MP3ID = x.MP3ID,
                        Title = x.Title,
                        Artist = x.Artist,
                        Album = x.Album,
                        Year = x.Year,
                        Playlist = x.Playlist.Select(y =>
                            new Playlist
                            {
                                PlaylistID = y.PlaylistID,
                                Name = y.Name
                            }).ToList()
                    }).ToList();

            return filteredData;
        }


        public MP3 Get(int id)
        {
            var data = _repository.Get(x => x.MP3ID.Equals(id));

            var filteredData = new MP3
            {
                MP3ID = data.MP3ID,
                Title = data.Title,
                Artist = data.Artist,
                Album = data.Album,
                Year = data.Year,
                Playlist = data.Playlist.Select(y =>
                    new Playlist
                    {
                        PlaylistID = y.PlaylistID,
                        Name = y.Name
                    }).ToList()
            };

            return filteredData;
        }

        public IList<MP3> GetInPlaylist(int playlistID)
        {
            var playlist = _uow.PlaylistRepository.GetAll().Where(x => x.PlaylistID == playlistID).FirstOrDefault();
            if (playlist != null)
            {
                List<MP3> mp3InPlaylist = new List<MP3>();

                mp3InPlaylist.AddRange(playlist.MP3);
                return mp3InPlaylist;
            }
            return null;
        }

        public IList<MP3> GetNotInPlaylist(int playlistID)
        {
            var playlist = _uow.PlaylistRepository.GetAll().Where(x => x.PlaylistID == playlistID).FirstOrDefault();
            if (playlist != null)
            {
                var mp3InPlaylist = new List<MP3>();
                mp3InPlaylist.AddRange(playlist.MP3);

                IList<MP3> mp3sNotInPlaylist = _repository.GetAll().ToList();
                foreach (var mp3 in mp3InPlaylist)
                {
                    mp3sNotInPlaylist.Remove(mp3);
                }

                return mp3sNotInPlaylist;
            }
            return null;
        }

        public IList<MP3> GetFiltered(string searchQuery)
        {
            var mp3s = _repository.GetAll();
            var queriedMp3s = new List<MP3>();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                foreach (var mp3 in mp3s)
                {
                    if (mp3.Title != null)
                    {
                        if (mp3.Title.ToLower().Contains(searchQuery))
                        {
                            queriedMp3s.Add(mp3);
                            continue;
                        }
                    }

                    if (mp3.Artist != null)
                    {
                        if (mp3.Artist.ToLower().Contains(searchQuery))
                        {
                            queriedMp3s.Add(mp3);
                            continue;
                        }
                    }

                    foreach (var playlist in mp3.Playlist)
                    {
                        if (playlist.Name.ToLower().Contains(searchQuery))
                        {
                            queriedMp3s.Add(mp3);
                            break;
                        }
                    }
                }
            }
            return queriedMp3s;
        }

        public PagedModel<MP3> GetPaged(int page)
        {
            int pageSize = 10;
            var mp3s = GetAll();
            var mp3sPaged = mp3s
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            var pagesCount = (int)Math.Ceiling((double)mp3s.Count / (double)pageSize);
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

            var pagedData = new PagedModel<MP3>(mp3sPaged, pages, page, nextActive, previousActive);
            return pagedData;
        }

        public void Create(MP3 entity)
        {
            IList<Playlist> attachedPlaylists = new List<Playlist>();
            foreach (var playlist in entity.Playlist)
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
