using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class TrackBaseViewModel
    {
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }
        [Display(Name = "Composer(s)")]
        public string Composers { get; set; }
        [Display(Name = "Track's primary Genre")]
        public string Genre { get; set; }

        public int Id { get; set; }
        [Display(Name = "Track's Name")]
        public string Name { get; set; }
        [Display(Name = "Albums with this track")]
        public ICollection<string> AlbumNames { get; set; }
    }
}