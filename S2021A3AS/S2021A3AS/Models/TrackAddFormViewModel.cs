
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Models
{
    public class TrackAddFormViewModel : TrackBaseViewModel
    {
        [Display(Name="Album")]
        public SelectList AlbumList { get; set; }
        [Display(Name= "Media Type List")]
        public SelectList MediaTypeList { get; set; }
    }
}