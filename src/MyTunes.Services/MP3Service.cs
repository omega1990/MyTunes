using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Services.ServiceContracts;
using MyTunes.Data.Repositories;
using MyTunes.Data.EntityModel;
using MyTunes.Data.UnitOfWork;

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
            _repository.Create(entity);
        }

        public void Update(MP3 entity)
        {
            _repository.Update(entity);
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
