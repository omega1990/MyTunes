using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Data.Entity;

using MyTunes.Data.EntityModel;
using MyTunes.Web.Api.ViewModels;

namespace MyTunes.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutoMapper.Mapper.CreateMap<MP3, MP3ViewModel>();
            AutoMapper.Mapper.CreateMap<MP3ViewModel, MP3>();
        }
    }
}
