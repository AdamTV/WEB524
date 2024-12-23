using S2021A6AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class ArtistBaseViewModel
    {
        [Display(Name = "Birth Name (if applicable)")]
        public string BirthName { get; set; }
        [Display(Name = "Birth / Start Date")]
        public DateTime BirthOrStartDate { get; set; }
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }
        [Display(Name = "Artist's Primary Genre")]
        public string Genre { get; set; }

        public int Id { get; set; }
        [Display(Name = "Artist / Stage name")]
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Artist Photo")]
        public string UrlArtist { get; set; }

        public IEnumerable<Album> Albums { get; set; }

        [Display(Name = "Number of Albums")]
        public int AlbumsCount { get; set; }


        [StringLength(5000)]
        public string Biography { get; set; }
    }
}