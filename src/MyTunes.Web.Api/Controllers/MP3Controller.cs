using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MyTunes.Data.UnitOfWork;
using MyTunes.Web.Api.ViewModels;
using MyTunes.Data.EntityModel;

namespace MyTunes.Web.Api.Controllers
{
    public class MP3Controller : ApiController
    {
        UnitOfWork UoW = new UnitOfWork();

        // GET api/<controller>
        public IList<MP3ViewModel> Get()
        {
            var MP3s = UoW.Mp3Repository.GetAll();
            return AutoMapper.Mapper.Map<IList<MP3>, IList<MP3ViewModel>>(MP3s);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}