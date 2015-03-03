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
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [StringLength(50, ErrorMessage = "Title is too long.")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Artist name is too long.")]
        public string Artist { get; set; }

        [StringLength(50, ErrorMessage = "Album name is too long.")]
        public string Album { get; set; }

        [Range(1, 9999,ErrorMessage="Year out of range.")]
        public Nullable<short> Year { get; set; }

        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}