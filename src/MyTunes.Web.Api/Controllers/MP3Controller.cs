///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 28.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MyTunes.Data.UnitOfWork;
using MyTunes.Web.Api.ViewModels;
using MyTunes.Data.EntityModel;
using MyTunes.Services.ServiceContracts;

using AutoMapper;

namespace MyTunes.Web.Api.Controllers
{
    public class MP3Controller : ApiController
    {
        private IMP3Service _mp3service;

        public MP3Controller(IMP3Service mp3service)
        {
            _mp3service = mp3service;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var mp3s = _mp3service.GetAll();
            var mp3ViewModels = new List<MP3ViewModel>();
            Mapper.Map(mp3s, mp3ViewModels);

            return Ok(mp3ViewModels);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int mp3Id)
        {
            var mp3ToReturn = _mp3service.Get(mp3Id);
            var mp3ViewModel = new MP3ViewModel();
            Mapper.Map(mp3ToReturn, mp3ViewModel);

            return Ok(mp3ViewModel);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/MP3/getInPlaylist/{playlistId:int}")]
        public IHttpActionResult GetInPlaylist(int playlistId)
        {
            var mp3sToReturn = _mp3service.GetInPlaylist(playlistId);

            return Ok(mp3sToReturn);
        }

        [HttpGet]
        [Route("api/MP3/getNotInPlaylist/{playlistId:int}")]
        public IHttpActionResult GetNotInPlaylist(int playlistId)
        {
            var mp3sToReturn = _mp3service.GetNotInPlaylist(playlistId);

            return Ok(mp3sToReturn);
        }

        [HttpGet]
        [Route("api/MP3/getFiltered/{searchQuery}")]
        public IHttpActionResult GetFiltered(string searchQuery)
        {
            var mp3sToReturn = _mp3service.GetFiltered(searchQuery);

            return Ok(mp3sToReturn);
        }


        [HttpGet]
        [Route("api/MP3/getPaginated/{page:int}")]
        public IHttpActionResult GetPaginated(int page)
        {
            var pagedData = _mp3service.GetPaged(page);
            var pagedDataToReturn = new PagedViewModel<MP3>();
            Mapper.Map(pagedData, pagedDataToReturn);

            return Ok(pagedDataToReturn);
        }

        [HttpGet]
        [Route("api/MP3/getCount")]
        public IHttpActionResult GetCount()
        {
            var mp3s = _mp3service.GetAll();
            return Ok(new { Count = mp3s.Count });
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]MP3ViewModel mp3)
        {
            if (ModelState.IsValid)
            {
                var mp3ToCreate = new MP3();
                Mapper.Map(mp3, mp3ToCreate);
                _mp3service.Create(mp3ToCreate);
                return Created(Url.Link("DefaultApi", new { id = mp3ToCreate.MP3ID }), mp3ToCreate);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]MP3ViewModel mp3)
        {
            if (ModelState.IsValid)
            {
                var mp3ToUpdate = new MP3();
                Mapper.Map(mp3, mp3ToUpdate);
                _mp3service.Update(mp3ToUpdate);

                return Ok(mp3ToUpdate);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            _mp3service.Delete(id);

            return Ok();
        }
    }
}