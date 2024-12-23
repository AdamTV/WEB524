using S2021A3AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A3AS.Models
{
    public class AlbumBaseViewModel
    {
        [Key]
        public int AlbumId { get; set; }

        [Required]
        [StringLength(160)]
        [DataType(DataType.Text)]
        public string Title { get; set; }
    }
}