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
        public IList<MP3ViewModel> Get()
        {
            var MP3s = _mp3service.GetAll();
            return AutoMapper.Mapper.Map<IList<MP3>, IList<MP3ViewModel>>(MP3s);
        }

        // GET api/<controller>/5
        public MP3ViewModel Get(int id)
        {
            var mp3ToReturn = _mp3service.Get(id);
            return AutoMapper.Mapper.Map<MP3, MP3ViewModel>(mp3ToReturn);
        }

        // POST api/<controller>
        public void Post([FromBody]MP3ViewModel mp3)
        {
            var mp3ToCreate = AutoMapper.Mapper.Map<MP3ViewModel, MP3>(mp3);
            _mp3service.Create(mp3ToCreate);
        }

        // PUT api/<controller>/5
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