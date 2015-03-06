///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 28.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using MyTunes.Data.EntityModel;
using MyTunes.Data.Repositories;
using System;
namespace MyTunes.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        MP3Repository Mp3Repository { get; }
        PlaylistRepository PlaylistRepository { get; }

        void Commit();
    }
}
