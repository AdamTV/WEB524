using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A6AS.Models
{
    public class TrackAddFormViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Track's Name")]
        public string Name { get; set; }
        [Display(Name = "Composer(s)")]
        public string Composers { get; set; }
        [Display(Name = "Track's primary Genre")]
        public SelectList GenreList { get; set; }

        // TODO 2 - In this "Form" class, the property type is string, and the data type is upload
        [Required]
        [Display(Name = "Sample Audio Clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }
        public string AlbumName { get; set; }

        public int AlbumId { get; set; }
    }
}