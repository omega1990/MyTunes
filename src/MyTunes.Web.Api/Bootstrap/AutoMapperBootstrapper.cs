///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 28.02.2015
//
///////////////////////////////////////////////////////////////////////////////////

using MyTunes.Data.EntityModel;
using MyTunes.Web.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTunes.Web.Api.Bootstrap
{
    public static class AutoMapperBootstrapper
    {
        public static void Bootstrap()
        {
            // Model -> ViewModel
            AutoMapper.Mapper.CreateMap<MP3, MP3ViewModel>();
            AutoMapper.Mapper.CreateMap<Playlist, PlaylistViewModel>();

            // ViewModel -> Model
            AutoMapper.Mapper.CreateMap<MP3ViewModel, MP3>();
            AutoMapper.Mapper.CreateMap<PlaylistViewModel, Playlist>();
        }
    }
}