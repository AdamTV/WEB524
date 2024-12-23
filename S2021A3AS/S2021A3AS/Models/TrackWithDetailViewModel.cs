using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A3AS.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Display(Name="Artist Name")]
        [DataType(DataType.Text)]
        public string AlbumArtistName { get; set; }
        [Display(Name = "Album Title")]
        [DataType(DataType.Text)]
        public string AlbumTitle { get; set; }
        public MediaTypeBaseViewModel MediaType { get; set; }
    }
}