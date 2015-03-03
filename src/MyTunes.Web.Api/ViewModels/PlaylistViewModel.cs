///////////////////////////////////////////////////////////////////////////////////
//
// Project: MyTunes
//
// Author: Darko Supe
// Creation Date: 28.02.2015
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
    public class PlaylistViewModel 
    {
        public PlaylistViewModel()
        {
            this.MP3 = new HashSet<MP3>();
        }
    
        public int PlaylistID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="Playlist name too long.")]
        public string Name { get; set; }
    
        public virtual ICollection<MP3> MP3 { get; set; }

    }
}