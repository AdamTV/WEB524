using S2021A5AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5AS.Models
{
    public class AlbumBaseViewModel
    {
        [Display(Name="Coordinator who looks after the album")]
        public string Coordinator { get; set; }

        [Display(Name = "Album's Primary Genre")]
        public string Genre { get; set; }

        public int Id { get; set; }
        [Display(Name = "Album's Name")]
        public string Name { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Album's Image / Cover Art")]
        [DataType(DataType.ImageUrl)]
        public string UrlAlbum { get; set; }

        [Display(Name = "Artists with this album")]
        public ICollection<string> ArtistNames { get; set; }

        public IEnumerable<Artist> Artists { get; set; }

        public IEnumerable<Track> Tracks { get; set; }
    }
}