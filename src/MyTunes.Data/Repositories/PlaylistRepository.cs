///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 27.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using MyTunes.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes.Data.Repositories
{
    public class PlaylistRepository : GenericRepository<Playlist>
    {
        public PlaylistRepository(MyTunesEntities entities)
            : base(entities)
        {

        }
        //TODO: Custom methods
    }
}
