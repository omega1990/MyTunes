using MyTunes.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes.Services.ServiceContracts
{
    public interface IPlaylistService
    {
        IList<Playlist> GetAll();
        Playlist Get(int id);
        IList<Playlist> GetFiltered(string searchQuery);
        void Create(Playlist entity);
        void Update(Playlist entity);
        void Delete(int id);
        void Save();
    }
}
