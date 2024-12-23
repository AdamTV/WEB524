using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class TrackUpdateClipViewModel
    {
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }
    }
}