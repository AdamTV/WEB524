using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace S2021A6AS.Models
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

        [DataType(DataType.MultilineText)]
        [StringLength(5000)]
        public string Summary { get; set; }
    }
}