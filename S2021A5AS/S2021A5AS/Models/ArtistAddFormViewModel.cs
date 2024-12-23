using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5AS.Models
{
    public class ArtistAddFormViewModel
    {
        public ArtistAddFormViewModel()
        {
            BirthOrStartDate = DateTime.Today;
        }
        [Display(Name = "Birth Name (if applicable)")]
        public string BirthName { get; set; }
        [Display(Name = "Birth / Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }
        [Display(Name = "Artist's Primary Genre")]
        public SelectList GenreList { get; set; }
        public int Id { get; set; }
        [Display(Name = "Artist / Stage name")]
        public string Name { get; set; }
        [Display(Name = "URL to Artist Photo")]
        public string UrlArtist { get; set; }
    }
}