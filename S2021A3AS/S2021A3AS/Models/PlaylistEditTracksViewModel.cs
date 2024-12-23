using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A3AS.Models
{
    public class PlaylistEditTracksViewModel : PlaylistBaseViewModel
    {
        public IEnumerable<int> NewTracksList { get; set; }
    }
}