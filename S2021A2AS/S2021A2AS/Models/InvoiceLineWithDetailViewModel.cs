using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A2AS.Models
{
    public class InvoiceLineWithDetailViewModel : InvoiceLineBaseViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        [DataType(DataType.Text)]
        public string TrackName { get; set; }
        [StringLength(220)]
        [Display(Name = "Track Composer(s)")]
        [DataType(DataType.Text)]
        public string TrackComposer { get; set; }
        [DataType(DataType.Text)]
        public string TrackAlbumTitle { get; set; }
        [DataType(DataType.Text)]
        public string TrackAlbumArtistName { get; set; }
        [DataType(DataType.Text)]
        public string TrackMediaTypeName { get; set; }
    }
}