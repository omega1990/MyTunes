///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 28.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using MyTunes.Data.EntityModel;
using MyTunes.Services.ServiceContracts;
using MyTunes.Web.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyTunes.Web.Api.Controllers
{
    public class PlaylistController : ApiController
    {
        private IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        // GET api/<controller>
        public IList<PlaylistViewModel> Get()
        {
            var playlists = _playlistService.GetAll();
            return AutoMapper.Mapper.Map<IList<Playlist>, IList<PlaylistViewModel>>(playlists);
        }

        // GET api/<controller>/5
        public PlaylistViewModel Get(int id)
        {
            var playlistToReturn = _playlistService.Get(id);
            return AutoMapper.Mapper.Map<Playlist, PlaylistViewModel>(playlistToReturn);
        }

        // POST api/<controller>
        public void Post([FromBody]PlaylistViewModel playlist)
        {
            var playlistToCreate = AutoMapper.Mapper.Map<PlaylistViewModel, Playlist>(playlist);
            _playlistService.Create(playlistToCreate);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]PlaylistViewModel playlist)
        {
            var playlistToUpdate = AutoMapper.Mapper.Map<PlaylistViewModel, Playlist>(playlist);
            _playlistService.Update(playlistToUpdate);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            _playlistService.Delete(id);
        }
    }
}