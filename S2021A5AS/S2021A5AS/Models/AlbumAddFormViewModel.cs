using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace S2021A5AS.Models
{
    public class AlbumAddFormViewModel
    {
        public AlbumAddFormViewModel()
        {
            ReleaseDate = DateTime.Today;
        }
        [Display(Name = "Coordinator who looks after the album")]
        [HiddenInput]
        public string Coordinator { get; set; }
        [Display(Name = "Album's Primary Genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "All Artists")]
        public SelectList ArtistList { get; set; }

        [Display(Name = "All Tracks")]
        public SelectList TracksList { get; set; }

        public int Id { get; set; }
        [Display(Name = "Album's Name")]
        public string Name { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "URL to Album's Image / Cover Art")]
        [DataType(DataType.ImageUrl)]
        public string UrlAlbum { get; set; }

        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        //public ICollection<Artist> Artists { get; set; }

        //public ICollection<Track> Tracks { get; set; }
    }
}
