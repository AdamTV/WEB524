using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Models
{
    public class PlaylistEditTracksFormViewModel : PlaylistBaseViewModel
    {
        [Display(Name = "All Tracks")]
        public SelectList AllTracksList { get; set; }

        [Display(Name = "Now on Playlist")]
        public IEnumerable<TrackBaseViewModel> TracksOnPlaylist { get; set; }
    }
}