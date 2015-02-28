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

        public MP3Service(IRepository<MP3> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public IList<Data.EntityModel.MP3> GetAll()
        {
            return _repository.GetAll();
        }

        public MP3 Get(Func<MP3, bool> predicate)
        {
            return _repository.Get(predicate); 
        }

        public void Create(MP3 entity)
        {
            _repository.Create(entity);
        }

        public void Update(MP3 entity)
        {
            _repository.Update(entity);
        }

        public void Delete(MP3 entity)
        {
            _repository.Delete(entity);
        }

        public void Save()
        {
            _uow.Commit();
        }
    }
}
