using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyTunes.Data.EntityModel;

namespace MyTunes.Services.ServiceContracts
{
    public interface IMP3Service 
    {
        IList<MP3> GetAll();
        IList<MP3> GetInPlaylist(int playlistID);
        IList<MP3> GetNotInPlaylist(int playlistID);
        IList<MP3> GetFiltered(string searchQuery);
        PagedModel<MP3> GetPaged(int page);
        MP3 Get(int id);
        void Create(MP3 entity);
        void Update(MP3 entity);
        void Delete(int id);
        void Save();
    }
}
