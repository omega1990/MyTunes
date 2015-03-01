///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 27.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using MyTunes.Data.EntityModel;

namespace MyTunes.Data.Repositories
{
    class MP3Repository : GenericRepository<MP3>
    {
        public MP3Repository(MyTunesEntities entities)
            : base(entities)
        {

        }

        //TODO: Custom methods if needed
    }
}
