using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5AS.Models
{
    public class TrackAddFormViewModel
    {
        [HiddenInput]
        public string Clerk { get; set; }

        [Display(Name="Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Display(Name="Track Primary Genre")]
        public SelectList GenreList { get; set; }

        public int Id { get; set; }

        [Display(Name = "Track Name")]
        public string Name { get; set; }

        public string AlbumName { get; set; }

        public int AlbumId { get; set; }
    }
}