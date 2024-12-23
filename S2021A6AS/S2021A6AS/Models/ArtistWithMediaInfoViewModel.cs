using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class ArtistWithMediaInfoViewModel : ArtistBaseViewModel 
    {
        public ArtistWithMediaInfoViewModel()
        {
            ArtistMediaItems = new List<ArtistMediaItemContentViewModel>();
        }

        public IEnumerable<ArtistMediaItemContentViewModel> ArtistMediaItems { get; set; }
    }
}