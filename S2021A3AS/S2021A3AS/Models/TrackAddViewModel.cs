using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A3AS.Models
{
    public class TrackAddViewModel : TrackBaseViewModel
    {
        [Required]
        //[Range(0,1)]
        public int? AlbumId { get; set; }

        [Required]
        //[Range(0, 1)]
        public int MediaTypeId { get; set; }
    }
}