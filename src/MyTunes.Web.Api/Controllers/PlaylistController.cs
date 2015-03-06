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

using AutoMapper;

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
        public IHttpActionResult Get()
        {
            var playlists = _playlistService.GetAll();
            var playlistsToReturn = new List<PlaylistViewModel>();
            Mapper.Map(playlists, playlistsToReturn);

            foreach (var playlist in playlists)
            {
                playlist.MP3 = null;
            }

            return Ok(playlistsToReturn);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int playlistId)
        {
            var playlist = _playlistService.Get(playlistId);
            var playlistToReturn = new PlaylistViewModel();
            Mapper.Map(playlist, playlistToReturn);

            return Ok(playlistToReturn);
        }

        [HttpGet]
        [Route("api/Playlist/getFiltered/{searchQuery:alpha}")]
        public IHttpActionResult GetFiltered(string searchQuery)
        {
            var playlists = _playlistService.GetFiltered(searchQuery);
            var playlistsToReturn = new List<PlaylistViewModel>();
            Mapper.Map(playlists, playlistsToReturn);

            return Ok(playlistsToReturn);
        }

        [HttpGet]
        [Route("api/Playlist/getPaginated/{page:int}")]
        public IHttpActionResult GetPaginated(int page)
        {
            var pagedData = _playlistService.GetPaged(page);
            var pagedDataToReturn = new PagedViewModel<Playlist>();
            Mapper.Map(pagedData, pagedDataToReturn);

            return Ok(pagedDataToReturn);
        }

        [HttpGet]
        [Route("api/Playlist/getCount")]
        public IHttpActionResult GetCount()
        {
            var playlists = _playlistService.GetAll();
            return Ok(new { Count = playlists.Count });
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]PlaylistViewModel playlist)
        {
            var playlistToCreate = new Playlist();
            Mapper.Map(playlist, playlistToCreate);
            _playlistService.Create(playlistToCreate);

            return Created(Url.Link("DefaultApi", new { id = playlistToCreate.PlaylistID }), playlistToCreate);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]PlaylistViewModel playlist)
        {
            var playlistToUpdate = new Playlist();
            Mapper.Map(playlist, playlistToUpdate);
            _playlistService.Update(playlistToUpdate);

            return Ok(playlistToUpdate);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            _playlistService.Delete(id);

            return Ok();
        }
    }
}