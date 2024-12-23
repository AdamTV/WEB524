using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A6AS.Models
{
    public class TrackAddViewModel
    {
        [HiddenInput]
        public string Clerk { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Display(Name = "Track Primary Genre")]
        public string Genre { get; set; }

        public int Id { get; set; }

        [Display(Name = "Track Name")]
        public string Name { get; set; }

        public string AlbumName { get; set; }

        public int AlbumId { get; set; }

        // TODO 3 - In this "Add" class, notice the type is HttpPostedFileBase
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }
    }
}