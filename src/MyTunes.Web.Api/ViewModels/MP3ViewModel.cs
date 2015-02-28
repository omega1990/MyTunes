using MyTunes.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTunes.Web.Api.ViewModels
{
    public class MP3ViewModel
    {
        public MP3ViewModel()
        {
            this.Playlist = new HashSet<Playlist>();
        }
    
        public int MP3ID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public Nullable<short> Year { get; set; }
    
        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}