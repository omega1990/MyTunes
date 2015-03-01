﻿using System;
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
        MP3 Get(int id);
        void Create(MP3 entity);
        void Update(MP3 entity);
        void Delete(int id);
        void Save();
    }
}
