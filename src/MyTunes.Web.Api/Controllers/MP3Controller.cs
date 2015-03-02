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
        public IHttpActionResult Get(int id)
        {
            var mp3ToReturn = _mp3service.Get(id);
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


        // POST api/<controller>
        public void Post([FromBody]MP3ViewModel mp3)
        {
            var mp3ToCreate = AutoMapper.Mapper.Map<MP3ViewModel, MP3>(mp3);
            _mp3service.Create(mp3ToCreate);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody]MP3ViewModel mp3)
        {
            var mp3ToUpdate = AutoMapper.Mapper.Map<MP3ViewModel, MP3>(mp3);
            _mp3service.Update(mp3ToUpdate);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            _mp3service.Delete(id);
        }
    }
}