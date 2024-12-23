using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class TrackUpdateClipFormViewModel
    {
        [Required]
        [Display(Name = "Sample Audio Clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

        public string TrackName { get; set; }

        public int TrackId { get; set; }
    }
}